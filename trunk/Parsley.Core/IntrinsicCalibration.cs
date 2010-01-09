using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Emgu.CV.Structure;

namespace Parsley.Core {

  /// <summary>
  /// Perform intrinsic camera calibration
  /// </summary>
  public class IntrinsicCalibration : CalibrationBase {
    private List<System.Drawing.PointF[]> _image_points;
    private System.Drawing.Size _img_size;

    /// <summary>
    /// Construct from reference object points
    /// </summary>
    /// <param name="object_points"></param>
    public IntrinsicCalibration(MCvPoint3D32f[] object_points, System.Drawing.Size image_size) 
      : base(object_points)
    {
      _img_size = image_size;
      _image_points = new List<System.Drawing.PointF[]>();
    }

    /// <summary>
    /// Add another view of image points.
    /// </summary>
    /// <param name="image_points"></param>
    public void AddView(System.Drawing.PointF[] image_points) {
      if (image_points.Length != ObjectPoints.Length) {
        throw new ArgumentException("Number of image points and object points do not match");
      }
      _image_points.Add(image_points);
    }

    /// <summary>
    /// Access the views taken
    /// </summary>
    public List<System.Drawing.PointF[]> Views {
      get { return _image_points; }
    }

    /// <summary>
    /// Clear previously added image points from views
    /// </summary>
    public void ClearViews() {
      _image_points.Clear();
    }

    public Emgu.CV.IntrinsicCameraParameters Calibrate() {
      if (_image_points.Count == 0) {
        throw new Exception("Number of image points and object points do not match");
      }
      List<MCvPoint3D32f[]> points = new List<MCvPoint3D32f[]>();
      for (int i = 0; i < _image_points.Count; ++i) {
        points.Add(this.ObjectPoints);
      }
      Emgu.CV.ExtrinsicCameraParameters[] ecp;

      Emgu.CV.IntrinsicCameraParameters icp = new Emgu.CV.IntrinsicCameraParameters();

      Emgu.CV.CameraCalibration.CalibrateCamera(
        points.ToArray(),
        _image_points.ToArray(),
        _img_size,
        icp,
        Emgu.CV.CvEnum.CALIB_TYPE.DEFAULT,
        out ecp
      );

      return icp;
    }
  }
}
