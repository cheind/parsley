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


    public void TransformPoints(List<Vector> points)
    {
      for (int i = 0; i < points.Count; ++i) {
        points[i] = _final.Multiply(points[i].ToHomogeneous(1).ToColumnMatrix()).GetColumnVector(0).ToNonHomogeneous();
      }
    }

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

    private List<Vector> TransformToMarkerCoordinates(System.Drawing.PointF[] pixels)
    {
      if (pixels != null)
      {
        List<Vector> points = new List<Vector>(pixels.Length);
        List<Ray> eye_rays = new List<Ray>(Ray.EyeRays(_mycam.Intrinsics, pixels));
        Plane positioner_plane = new Plane(_ecp);
        double t = 0;
        

        for(int x = 0; x < pixels.Length; x++)
        {
          if (Intersection.RayPlane(eye_rays[x], positioner_plane, out t))
          {
            points.Add(eye_rays[x].At(t));
          }
        }

        if (points.Count == 4)
        {
          for (int i = 0; i < points.Count; i++)
            points[i] = _extrinsicMatrix.Inverse().Multiply(points[i].ToHomogeneous(1).ToColumnMatrix()).GetColumnVector(0).ToNonHomogeneous();
        }
        else
          points = null;

        return points;
      }

      return null;
    }

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


    private void UpdateTransformation()
    {
      System.Drawing.PointF[] currentImagePoints;
      List<Vector> currentTransformed = null;
      if (_initialCornerPoints.Count == 4)
      {
        Emgu.CV.Image<Gray, Byte> gray_img = _mycam.Frame().Convert<Gray, Byte>();

        if (!_pattern.FindPattern(gray_img, out currentImagePoints))
        {
          _logger.Warn("UpdateTransformation: Pattern not found.");
          _warp_matrix = Matrix.Identity(4, 4);
        }
        else
        {
          currentTransformed = TransformToMarkerCoordinates(currentImagePoints);
          _warp_matrix = FindAffineTransformation(_initialCornerPoints, currentTransformed);
        }
        //now calculate the final transformation
        _final = _extrinsicMatrix * _warp_matrix * _extrinsicMatrix.Inverse();
      }
    }

    private Matrix FindAffineTransformation(List<Vector> src, List<Vector> dest)
    {
      Matrix m = Matrix.Identity(4,4);
      // calculate Translation
      if (src != null && dest != null)
      {
        Vector trans = (dest[0] - src[0]).ToHomogeneous(1);

        //calculate rotation
        Vector v_s10 = (src[1] - src[0]).Normalize();
        Vector v_d10 = (dest[1] - dest[0]).Normalize();

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

    private double CalculateAngle(Vector n_source, Vector n_dest)
    {
      double angle_s = 0, angle_d = 0, angle = 0;

      if (n_source != null && n_dest != null)
      {
        if (n_source[1] >= 0.0)
          angle_s = Math.Acos(n_source[0]);
        else
          angle_s = -Math.Acos(n_source[0]);

        if (angle_s < 0)
          angle_s = angle_s + 2 * Math.PI;

        if (n_dest[1] >= 0.0)
          angle_d = Math.Acos(n_dest[0]);
        else
          angle_d = -Math.Acos(n_dest[0]);

        if (angle_d < 0)
          angle_d = angle_d + 2 * Math.PI;

        angle = angle_d - angle_s;
        _angle_degrees = angle * (180 / Math.PI);
      }

      return angle;
    }

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
        ExtractExctrinsicMatrix();
      }
    }

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
