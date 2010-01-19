using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Emgu.CV.Structure;
using MathNet.Numerics.LinearAlgebra;

namespace Parsley.Core {

  /// <summary>
  /// Perform intrinsic camera calibration
  /// </summary>
  public class IntrinsicCalibration : CalibrationBase {
    private List<System.Drawing.PointF[]> _image_points;
    private MCvPoint3D32f[] _converted_object_points;
    private System.Drawing.Size _img_size;

    /// <summary>
    /// Construct from reference object points
    /// </summary>
    /// <param name="object_points"></param>
    public IntrinsicCalibration(Vector[] object_points, System.Drawing.Size image_size) 
      : base(object_points)
    {
      _img_size = image_size;
      _image_points = new List<System.Drawing.PointF[]>();
      // Since object points remain constant, we can apply their conversion right here
      _converted_object_points = Array.ConvertAll<Vector, MCvPoint3D32f>(
        this.ObjectPoints,
        new Converter<Vector, MCvPoint3D32f>(Extensions.ConvertFromParsley.ToEmguF)
      );
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
        throw new InvalidOperationException("No image points present.");
      }

      List<MCvPoint3D32f[]> points = new List<MCvPoint3D32f[]>();
      for (int i = 0; i < _image_points.Count; ++i) {
        points.Add(this._converted_object_points);
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
