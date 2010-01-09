using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Emgu.CV.Structure;

namespace Parsley.Core {

  /// <summary>
  /// Perform extrinsic calibration
  /// </summary>
  public class ExtrinsicCalibration : CalibrationBase {
    private Emgu.CV.IntrinsicCameraParameters _intrinsics;

    public ExtrinsicCalibration(MCvPoint3D32f[] object_points, Emgu.CV.IntrinsicCameraParameters intrinsics)
      : base(object_points) {
      _intrinsics = intrinsics;
    }

    public Emgu.CV.ExtrinsicCameraParameters Calibrate(System.Drawing.PointF[] image_points) {
      return Emgu.CV.CameraCalibration.FindExtrinsicCameraParams2(
       this.ObjectPoints,
       image_points,
       _intrinsics);
    }
  }
}
