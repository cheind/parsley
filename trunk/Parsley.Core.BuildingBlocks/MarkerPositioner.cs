using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using MathNet.Numerics.LinearAlgebra;
using System.ComponentModel;
using Parsley.Core.Extensions;
using Emgu.CV.Structure;
using Emgu.CV;
using Emgu.CV.CvEnum;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;
using log4net;

namespace Parsley.Core.BuildingBlocks
{
  [Serializable]
  [Parsley.Core.Addins.Addin]
  public class MarkerPositioner : Parsley.Core.IPositioner, ISerializable{

    private Emgu.CV.ExtrinsicCameraParameters _ecp;
    private Parsley.Core.CalibrationPattern _pattern;
    private List<Vector> _initialCornerPoints;
    private Matrix _extrinsicMatrix;
    private Matrix _final;
    private Matrix _warp_matrix;
    private Camera _mycam;
    private double _angle_degrees;
    private bool _first;
    private readonly ILog _logger;

    /// <summary>
    /// Default Constructor
    /// </summary>
    /// <param name="theCam"> Camera Object needed in order to obtain the current camera frame.</param>
    public MarkerPositioner(Camera theCam) {
      _final = Matrix.Identity(4, 4);
      _ecp = null;
      _pattern = null;
      _extrinsicMatrix = Matrix.Identity(4, 4);
      _initialCornerPoints = new List<Vector>();
      _mycam = theCam;
      _warp_matrix = Matrix.Identity(4,4);
      _angle_degrees = 0;
      _first = true;
      _logger = LogManager.GetLogger(typeof(MarkerPositioner));
    }

    public MarkerPositioner(SerializationInfo info, StreamingContext context)
    {
      _ecp = (Emgu.CV.ExtrinsicCameraParameters)info.GetValue("ecp",  typeof(Emgu.CV.ExtrinsicCameraParameters));
      _final = Matrix.Create((double[][])info.GetValue("final", typeof(double[][])));
      _angle_degrees = (double)info.GetValue("angle", typeof(double));
      _mycam = (Parsley.Core.BuildingBlocks.Camera)info.GetValue("cam", typeof(Parsley.Core.BuildingBlocks.Camera));
      _warp_matrix = Matrix.Create((double[][])info.GetValue("warp", typeof(double[][])));
      _pattern = (Parsley.Core.CalibrationPattern)info.GetValue("pattern", typeof(Parsley.Core.CalibrationPattern));
      _extrinsicMatrix = Matrix.Create((double[][])info.GetValue("extrinsicMat", typeof(double[][])));
      _initialCornerPoints = (List<Vector>)info.GetValue("cornerpoints", typeof(List<Vector>));

      _first = true;
      _logger = LogManager.GetLogger(typeof(MarkerPositioner));
    }

    public void GetObjectData(SerializationInfo info, StreamingContext context) {
      info.AddValue("ecp", _ecp);
      info.AddValue("final", _final.GetArray());
      info.AddValue("angle", _angle_degrees);
      info.AddValue("cam", _mycam);
      info.AddValue("warp", _warp_matrix.GetArray());
      info.AddValue("pattern", _pattern);
      info.AddValue("extrinsicMat", _extrinsicMatrix.GetArray());
      info.AddValue("cornerpoints", _initialCornerPoints);
    }

    /// <summary>
    /// Transforms 3D-Points from the Camera Coordinate System into the Marker Coordinate System
    /// </summary>
    /// <param name="points"> List of Vectors, containing the 3d points which should be transformed</param>
    public void TransformPoints(List<Vector> points)
    {
      for (int i = 0; i < points.Count; ++i) {
        points[i] = _final.Multiply(points[i].ToHomogeneous(1).ToColumnMatrix()).GetColumnVector(0).ToNonHomogeneous();
      }
    }

    /// <summary>
    /// Finds image points of the given pattern.
    /// If the function CalibrationPattern::FindPattern can't find the pattern,
    /// a warning will be reported.
    /// </summary>
    private void FindImagePoints()
    {
      if (_pattern != null && _pattern.ImagePoints == null)
      {
        Emgu.CV.Image<Gray, Byte> gray_img = _mycam.Frame().Convert<Gray, Byte>();
        _pattern.FindPattern(gray_img);
        if (_pattern.ImagePoints != null && _first == true)
          _first = false;
        else
          _logger.Warn("FinImagePoints: Pattern not found.");
      }
    }

    /// <summary>
    /// Transforms 2D-Points from the Camera Coordinate System into 3D-Points of the Marker Coordinate System.
    /// Firstly, Eye-Rays are created through the given 2d corner points of the pattern.
    /// The intersections between these rays and the positioners' plane, represent the 3d points in the camera coordinate system.
    /// 3d points are extracted using the function Ray::At(t). These points finally need to be transformed into the marker coordinate system
    /// using the inverse extrinsic matrix.
    /// </summary>
    /// <param name="pixels"> Array, containing the 2d points which should be transformed. </param>
    /// <returns></returns>
    private List<Vector> TransformToMarkerCoordinates(System.Drawing.PointF[] pixels)
    {
      // only if the the array contains elements
      if (pixels != null)
      {
        List<Vector> points = new List<Vector>(pixels.Length);
        //find eye-rays through pixels
        List<Ray> eye_rays = new List<Ray>(Ray.EyeRays(_mycam.Intrinsics, pixels));
        Plane positioner_plane = new Plane(_ecp);
        double t = 0;
        
        // find intersections positioner plane <-> eye rays ==> points.
        for(int x = 0; x < pixels.Length; x++)
        {
          if (Intersection.RayPlane(eye_rays[x], positioner_plane, out t))
          {
            points.Add(eye_rays[x].At(t));
          }
        }

        // 4 corner points found?
        if (points.Count == 4)
        {
          // transform these points into the marker coordinate system
          for (int i = 0; i < points.Count; i++)
            points[i] = _extrinsicMatrix.Inverse().Multiply(points[i].ToHomogeneous(1).ToColumnMatrix()).GetColumnVector(0).ToNonHomogeneous();
        }
        else
          points = null;

        return points;
      }

      return null;
    }

    /// <summary>
    /// Extracts the extrinsic matrix from the cameras' extrinsic parameters 
    /// </summary>
    private void ExtractExctrinsicMatrix()
    {
      if (_ecp != null)
      {
        for (int i = 0; i < 3; i++)
        {
          for (int j = 0; j < 4; j++)
          {
            _extrinsicMatrix[i, j] = _ecp.ExtrinsicMatrix[i, j];
          }
        }
      }
    }

    /// <summary>
    /// Calculates the transformation matrix, which is used to transform the 3d-object points, which were scanned with reference
    /// to the moved marker coordinate system, back to the initial marker system and henceforth back to the camera system.
    /// </summary>
    private void UpdateTransformation()
    {
      System.Drawing.PointF[] currentImagePoints;
      List<Vector> currentTransformed = null;
      if (_initialCornerPoints.Count == 4)
      {
        Emgu.CV.Image<Gray, Byte> gray_img = _mycam.Frame().Convert<Gray, Byte>();

        // find the current maker corner points (image points)
        if (!_pattern.FindPattern(gray_img, out currentImagePoints))
        {
          // if pattern can't be found: use identity warp matrix
          _logger.Warn("UpdateTransformation: Pattern not found.");
          _warp_matrix = Matrix.Identity(4, 4);
        }
        else
        {
          // transform the current-cornerpoints into the marker coordinate system
          currentTransformed = TransformToMarkerCoordinates(currentImagePoints);
          // find the affine transformation between initial marker coordinate system and the shifted system
          _warp_matrix = FindAffineTransformation(_initialCornerPoints, currentTransformed);
        }
        //now calculate the final transformation matrix
        _final = _extrinsicMatrix * _warp_matrix * _extrinsicMatrix.Inverse();
      }
    }

    /// <summary>
    /// Finds affine transformation between initial and shifted marker coordinate system.
    /// Translation: Difference Vector between the origins of the two marker c-systems.
    /// Rotation: Calculated angle between normalized x-direction vector.
    /// zo-----> x  represents corners: 0 ----- 1
    ///  |                              |
    ///  |                              |
    ///  |                              |
    ///  y                              3
    ///  Matrix m and translation vector, describe the transformation from the shifted marker system
    ///  to the initial marker system. The inverse matrix therefore needs to be used.
    /// </summary>
    /// <param name="src"> List containing the corner 3d-vectors of the ínitial marker system</param>
    /// <param name="dest">List containing the corner 3d-vectors of the shifted marker system</param>
    /// <returns></returns>
    private Matrix FindAffineTransformation(List<Vector> src, List<Vector> dest)
    {
      Matrix m = Matrix.Identity(4,4);
      // calculate Translation if lists contain elements: otherwhise use identity matrix
      if (src != null && dest != null)
      {
        //calculate translation
        Vector trans = (dest[0] - src[0]).ToHomogeneous(1);

        //calculate rotation
        Vector v_s10 = (src[1] - src[0]).Normalize();
        Vector v_d10 = (dest[1] - dest[0]).Normalize();

        //see function CalculateAngle for more details
        double angle = CalculateAngle(v_s10, v_d10);

        //set up matrix
        m.SetColumnVector(trans, 3);

        m[0, 0] = Math.Cos(angle);
        m[0, 1] = -Math.Sin(angle);
        m[1, 0] = Math.Sin(angle);
        m[1, 1] = Math.Cos(angle);

        //Transformation von marker(start) to marker(rotate) coordinate system
        m = m.Inverse();

        _logger.InfoFormat("MarkerPositioner: Affine Transformation found. Rotation: {0} degrees.", (int)_angle_degrees);
      }
      else
        _logger.Warn("MarkerPositioner: Affine Transformation not found. - Using Identity Matrix!");

      return m;
    }


    /// <summary>
    /// Calculates rotational angle between two unit vectors.
    /// Every input vector is considered in the complex plane. [x|y] => x + jy
    /// The angle with respect to the real-coordinate system axis is calculated for both vectors.
    /// Taking the difference of both angle results, leads to the rotational angle.
    /// Observe, that a distinction of cases (using cos^-1) is needed in order to obtain the angles properly.
    /// The result therefore depends on, in which quadrant of the complex plane the unit vector is located.
    /// </summary>
    /// <param name="n_source"> Unit vector source system</param>
    /// <param name="n_dest"> Unit vector destination system</param>
    /// <returns></returns>
    private double CalculateAngle(Vector n_source, Vector n_dest)
    {
      double angle_s = 0, angle_d = 0, angle = 0;

      if (n_source != null && n_dest != null)
      {
        //distinction of cases: quadrant, only consider the y component
        if (n_source[1] >= 0.0)
          // angles value is defined by the dot product (two unity vectors ==> length = 1)
          // cos(alpha) = [x|y] * [1|0] / (|[x|y]| |[1|0]|)
          // therefore only the x-component and length 1 needs to be considered ==> source[0] / length
          angle_s = Math.Acos(n_source[0]);
        else
          angle_s = -Math.Acos(n_source[0]);

        // add 2pi if the vector is located in quadrant 3 or 4, in order to obtain a range of 0.. 359 degrees
        if (angle_s < 0)
          angle_s = angle_s + 2 * Math.PI;

        // same procedure for vector 2
        if (n_dest[1] >= 0.0)
          angle_d = Math.Acos(n_dest[0]);
        else
          angle_d = -Math.Acos(n_dest[0]);

        if (angle_d < 0)
          angle_d = angle_d + 2 * Math.PI;

        // use the difference as rotational angle
        angle = angle_d - angle_s;
        _angle_degrees = angle * (180 / Math.PI);
      }

      return angle;
    }

    /// <summary>
    /// Set, Get the extrinsic parameters using a file dialog.
    /// If set: extract the new extrinsic matrix
    /// </summary>
    [Editor(typeof(ExtrinsicTypeEditor),
            typeof(System.Drawing.Design.UITypeEditor))]
    public Emgu.CV.ExtrinsicCameraParameters PositionerPose
    {
      get
      {
        return _ecp;
      }
      set
      {
        _ecp = value;
        _first = true;
        ExtractExctrinsicMatrix();
      }
    }

    /// <summary>
    /// Sets / Gets the used pattern using a file dialog.
    /// If set: find the new image corner points and transform them into the marker system (initial)
    /// </summary>
    [Editor(typeof(PatternTypeEditor),
            typeof(System.Drawing.Design.UITypeEditor))]
    public Parsley.Core.CalibrationPattern PositionerPattern
    {
      get
      {
        return _pattern;
      }
      set
      {
        _pattern = value;
        _first = true;
        FindImagePoints();
        _initialCornerPoints = TransformToMarkerCoordinates(_pattern.ImagePoints);
      }
    }

    /// <summary>
    /// Set / Get the current rotational angle.
    /// Observe: Setting the angle triggers an update of the transformation.
    /// </summary>
    public double Angle
    {
      get { return _angle_degrees; }
      set
      {
        _angle_degrees = value;
        if (_first == true)
        {
          FindImagePoints();
          _initialCornerPoints = TransformToMarkerCoordinates(_pattern.ImagePoints);
        }
        UpdateTransformation();
      }
    }
  }
}
