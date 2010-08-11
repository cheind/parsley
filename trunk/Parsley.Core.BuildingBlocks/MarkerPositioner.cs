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
  public class MarkerPositioner : IPositioner, ISerializable{

    private Emgu.CV.ExtrinsicCameraParameters _ecp;
    private Parsley.Core.CalibrationPattern _pattern;
    private Matrix _extrinsicMatrix;
    private Matrix _final;
    private readonly ILog _logger;

    /// <summary>
    /// Default Constructor
    /// </summary>
    /// <param name="theCam"> Camera Object needed in order to obtain the current camera frame.</param>
    public MarkerPositioner() {
      _final = Matrix.Identity(4, 4);
      _ecp = null;
      _pattern = null;
      _extrinsicMatrix = Matrix.Identity(4, 4);
      _logger = LogManager.GetLogger(typeof(MarkerPositioner));
    }

    public MarkerPositioner(SerializationInfo info, StreamingContext context)
    {
      _ecp = (Emgu.CV.ExtrinsicCameraParameters)info.GetValue("ecp",  typeof(Emgu.CV.ExtrinsicCameraParameters));
      _final = Matrix.Create((double[][])info.GetValue("final", typeof(double[][])));
      _pattern = (Parsley.Core.CalibrationPattern)info.GetValue("pattern", typeof(Parsley.Core.CalibrationPattern));
      _extrinsicMatrix = Matrix.Create((double[][])info.GetValue("extrinsicMat", typeof(double[][])));
      _logger = LogManager.GetLogger(typeof(MarkerPositioner));
    }

    public void GetObjectData(SerializationInfo info, StreamingContext context) {
      info.AddValue("ecp", _ecp);
      info.AddValue("final", _final.GetArray());
      info.AddValue("pattern", _pattern);
      info.AddValue("extrinsicMat", _extrinsicMatrix.GetArray());
    }

    /// <summary>
    /// Transforms 3D-Points from the Camera Coordinate System into the Marker Coordinate System
    /// The transformation matrix "_final" is calculated in "UpdateTransformation"
    /// </summary>
    /// <param name="points"> List of Vectors, containing the 3d points which should be transformed</param>
    public void TransformPoints(List<Vector> points)
    {
      for (int i = 0; i < points.Count; ++i) {
        points[i] = _final.Multiply(points[i].ToHomogeneous(1).ToColumnMatrix()).GetColumnVector(0).ToNonHomogeneous();
      }
    }

    /// <summary>
    /// Extracts the extrinsic matrix from the cameras' extrinsic parameters
    /// The values of the extrinsic matrix are extracted into a 4x4 Matrix.
    /// </summary>
    private Matrix ExtractExctrinsicMatrix(ExtrinsicCameraParameters ecp)
    {
      Matrix extrinsicMatrix = Matrix.Identity(4, 4);
      if (ecp != null)
      {
        for (int i = 0; i < 3; i++)
        {
          for (int j = 0; j < 4; j++)
          {
            extrinsicMatrix[i, j] = ecp.ExtrinsicMatrix[i, j];
          }
        }
      }
      return extrinsicMatrix;
    }

    /// <summary>
    /// Calculates the transformation matrix, which is used to transform the 3d-object points, which were scanned with reference
    /// to the moved marker coordinate system, back to the initial marker system and henceforth back to the camera system.
    /// The camera object is needed in order to gain the current camera frame. Furthermore, the cameras' intrinsics are needed
    /// to perform an extrinsic calibration.
    /// 
    /// The transformation matrix is calculated as follows:
    /// * All scanned points are referenced to the camera coordinate system.
    /// * _extrinsicMatrix describes the transformation between initial detected marker system and camera system.
    /// * extrinsicM2 describes the transformation between moved marker system and camera system.
    /// * Since the position of the scanned object, relative to the marker does not change, all points in the camera system
    ///   can be represented in a common coordinate system by using the inverse transformation "inv(extrinsicM2)".
    /// * Because the points should be represented in the camera system, they need to be tranformed back into the camera system
    ///   using the transformation "_extrinsicMatrix".
    /// * Therefore the final transformation matrix can be calculated from _final = _extrinsicMatrix * inv(extrinsicM2)
    /// </summary>
    public bool UpdateTransformation(Camera the_cam)
    {
      Matrix extrinsicM2 = Matrix.Identity(4, 4);
      ExtrinsicCameraParameters ecp_moved = null;
      ExtrinsicCalibration ec_moved = null;
      bool ret_value = true;

      if (_ecp != null && _pattern != null)
      {
        Emgu.CV.Image<Gray, Byte> gray_img = the_cam.Frame().Convert<Gray, Byte>();
        System.Drawing.PointF[] currentImagePoints;

        // find the current maker corner points (image points) in the given pattern
        if (!_pattern.FindPattern(gray_img, out currentImagePoints))
        {
          // if pattern can't be found: use identity warp matrix
          _logger.Warn("UpdateTransformation: Pattern not found.");
          ret_value = false;
        }
        else
        {
          //find the extrinsic, which describe the transformation between current marker coordinate system and camera coordinate system.
          ec_moved = new ExtrinsicCalibration(_pattern.ObjectPoints, the_cam.Intrinsics);
          ecp_moved = ec_moved.Calibrate(currentImagePoints);
        }

        if (ecp_moved != null)
        {
          //if the extrinsics have been found correctly, extract the extrinsic matrix
          extrinsicM2 = ExtractExctrinsicMatrix(ecp_moved);
          _logger.Info("UpdateTransformation: Transformation found.");
        }
        else
        {
          _logger.Warn("UpdateTransformation: Extrinsics of moved marker system not found.");
          ret_value = false;
        }
        //now calculate the final transformation matrix
        _final = _extrinsicMatrix * extrinsicM2.Inverse();
      }
      else
      {
        ret_value = false;
        _logger.Warn("UpdateTransformation: No Pattern or no Positioner Extrinsics have been chosen.");
      }
      return ret_value;
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
        _extrinsicMatrix = ExtractExctrinsicMatrix(_ecp);
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
      }
    }
  }
}
