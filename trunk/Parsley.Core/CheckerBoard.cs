using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Emgu.CV.Structure;
using MathNet.Numerics.LinearAlgebra;

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

    /// <summary>
    /// Generate reference points
    /// </summary>
    /// <returns>Reference points in z-plane.</returns>
    Vector[] GenerateObjectCorners() {
      Vector[] corners = new Vector[_inner_corners.Width * _inner_corners.Height];
      for (int y = 0; y < _inner_corners.Height; ++y) {
        for (int x = 0; x < _inner_corners.Width; x++) {
          int id = y * _inner_corners.Width + x;
          corners[id] = new Vector(new double[]{x * _field_size, y * _field_size, 0});
        }
      }
      return corners;
    }

    /// <summary>
    /// Find checkerboard in image
    /// </summary>
    /// <param name="img">Image to search pattern for</param>
    /// <param name="image_points">Detected checkerboard image points</param>
    /// <returns>True if pattern was found, false otherwise</returns>
    public override bool FindPattern(Emgu.CV.Image<Gray, byte> img, out System.Drawing.PointF[] image_points)
    {
      bool found = Emgu.CV.CameraCalibration.FindChessboardCorners(
        img,
        _inner_corners,
        Emgu.CV.CvEnum.CALIB_CB_TYPE.ADAPTIVE_THRESH | 
        Emgu.CV.CvEnum.CALIB_CB_TYPE.FILTER_QUADS |
        Emgu.CV.CvEnum.CALIB_CB_TYPE.NORMALIZE_IMAGE,
        out image_points
      );

      if (found) {
        img.FindCornerSubPix(
          new System.Drawing.PointF[][] { image_points },
          new System.Drawing.Size(5, 5),
          new System.Drawing.Size(-1, -1),
          new MCvTermCriteria(0.001));
      }
      return found;
    }
  }
}
