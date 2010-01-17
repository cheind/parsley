using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Emgu.CV.Structure;
using MathNet.Numerics.LinearAlgebra;

namespace Parsley.Core {

  /// <summary>
  /// Perform extrinsic calibration
  /// </summary>
  public class ExtrinsicCalibration : CalibrationBase {
    private Emgu.CV.IntrinsicCameraParameters _intrinsics;
    private MCvPoint3D32f[] _converted_object_points;

    public ExtrinsicCalibration(Vector[] object_points, Emgu.CV.IntrinsicCameraParameters intrinsics)
      : base(object_points) 
    {
      _intrinsics = intrinsics;
      // Since object points remain constant, we can apply their conversion right here
      _converted_object_points = Array.ConvertAll<Vector, MCvPoint3D32f>(
        this.ObjectPoints, 
        new Converter<Vector, MCvPoint3D32f>(Extensions.ConvertToEmgu.ToEmguF)
      );
    }

    public Emgu.CV.IntrinsicCameraParameters Intrinsics {
      get { return _intrinsics; }
    }

    /// <summary>
    /// Find extrinsic calibration
    /// </summary>
    /// <param name="image_points">Detected pattern points in image</param>
    /// <returns>Extrinsic calibration</returns>
    public Emgu.CV.ExtrinsicCameraParameters Calibrate(System.Drawing.PointF[] image_points) {
      return Emgu.CV.CameraCalibration.FindExtrinsicCameraParams2(
       _converted_object_points,
       image_points,
       _intrinsics);
    }
  }
}
