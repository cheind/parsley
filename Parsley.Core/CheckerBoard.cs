using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Emgu.CV.Structure;

namespace Parsley.Core {

  /// <summary>
  /// Represents a checker board used for calibration.
  /// </summary>
  /// <remarks>
  /// A checkerboard as implemented by this class is a rectangular pattern of 
  /// square fields which alternate in color (black/white). It's best to use
  /// a non-square pattern to avoid symmetries in detection. 
  /// 
  /// CheckerBoard is parametrized by the number of inner corner points per
  /// checkerboard row and column. Additionally a field size is specified that
  /// is used to generate object reference points in 3d.
  /// 
  /// </remarks>
  public class CheckerBoard : CalibrationPattern {
    private System.Drawing.Size _inner_corners;
    private float _field_size;

    public CheckerBoard(int inner_corner_row, int inner_corner_col, float field_size) {
      _inner_corners = new System.Drawing.Size(inner_corner_row, inner_corner_col);
      _field_size = field_size;
      this.ObjectPoints = GenerateObjectCorners();
    }

    MCvPoint3D32f[] GenerateObjectCorners() {
      MCvPoint3D32f[] corners = new MCvPoint3D32f[_inner_corners.Width * _inner_corners.Height];
      for (int y = 0; y < _inner_corners.Height; ++y) {
        for (int x = 0; x < _inner_corners.Width; x++) {
          int id = y * _inner_corners.Width + x;
          corners[id].x = x * _field_size;
          corners[id].y = y * _field_size;
          corners[id].z = 0.0f;
        }
      }
      return corners;
    }

    public override bool FindPattern(Emgu.CV.IImage img, out System.Drawing.PointF[] image_points) {
      Emgu.CV.Image<Gray, Byte> gray = img as Emgu.CV.Image<Gray, Byte>;
      if (gray == null) {
        throw new ArgumentException("Given image is not of type Image<Gray, Byte>");
      }
      return Emgu.CV.CameraCalibration.FindChessboardCorners(
        gray,
        _inner_corners,
        Emgu.CV.CvEnum.CALIB_CB_TYPE.ADAPTIVE_THRESH,
        out image_points
      );
    }
  }
}
