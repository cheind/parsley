using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Emgu.CV.Structure;

namespace Parsley.Core {

  /// <summary>
  /// Perform camera calibration
  /// </summary>
  public class Calibration {
    private MCvPoint3D32f[] _object_points;
    private List<System.Drawing.PointF[]> _image_points;
    Emgu.CV.IntrinsicCameraParameters _icp;

    public Calibration(MCvPoint3D32f[] object_points) {
      if (object_points.Length == 0) {
        throw new ArgumentException("Cannot work with no object points");
      }
      _image_points = new List<System.Drawing.PointF[]>();
      _icp = null;
      _object_points = object_points;
    }

    public bool Calibrated {
      get { return _icp != null; }
    }

    public void AddImagePoints(System.Drawing.PointF[] image_points) {
      if (image_points.Length != _object_points.Length) {
        throw new ArgumentException("Number of image points and object points do not match");
      }
      _image_points.Add(image_points);
    }

   
    public void FindIntrinsics(System.Drawing.Size image_size) {
      if (_image_points.Count == 0) {
        throw new Exception("Number of image points and object points do not match");
      }
      List<MCvPoint3D32f[]> points = new List<MCvPoint3D32f[]>();
      for (int i = 0; i < _image_points.Count; ++i) {
        points.Add(_object_points);
      }
      Emgu.CV.ExtrinsicCameraParameters[] ecp;

      _icp = new Emgu.CV.IntrinsicCameraParameters();
      Emgu.CV.CameraCalibration.CalibrateCamera(
        points.ToArray(),
        _image_points.ToArray(),
        image_size,
        _icp,
        Emgu.CV.CvEnum.CALIB_TYPE.DEFAULT,
        out ecp
      );
    }

    public Emgu.CV.ExtrinsicCameraParameters FindExtrinsics(System.Drawing.PointF[] image_points) {
      return Emgu.CV.CameraCalibration.FindExtrinsicCameraParams2(
        _object_points,
        image_points,
        _icp);
    }

    /// <summary>
    /// Access intrinsic parameters.
    /// </summary>
    public Emgu.CV.IntrinsicCameraParameters Intrinsics {
      get { return _icp; }
    }

    public List<System.Drawing.PointF[]> ImagePoints {
      get { return _image_points; }
    }
  }
}
