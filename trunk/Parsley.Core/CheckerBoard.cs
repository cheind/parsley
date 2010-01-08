using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Emgu.CV.Structure;

namespace Parsley.Core {

  /// <summary>
  /// Represents a checkerboard used for calibration
  /// </summary>
  public class CheckerBoard {
    private System.Drawing.Size _inner_corners;
    private System.Drawing.PointF[] _corners;
    private bool _pattern_found;

    /// <summary>
    /// Construct checkboard from the number of inner corners per row and column.
    /// </summary>
    public CheckerBoard(int corner_row, int corner_column) {
      _inner_corners = new System.Drawing.Size(corner_row, corner_column);
      _pattern_found = false;
    }

    /// <summary>
    /// Locate the checkerboard pattern in the provided image.
    /// </summary>
    /// <param name="img"></param>
    /// <returns>True if pattern was found</returns>
    public void FindPattern(Emgu.CV.Image<Gray, Byte> img) {
      _pattern_found = Emgu.CV.CameraCalibration.FindChessboardCorners(
        img, 
        _inner_corners, 
        Emgu.CV.CvEnum.CALIB_CB_TYPE.ADAPTIVE_THRESH,
        out _corners);
    }

    public void Draw(Emgu.CV.Image<Bgr, Byte> img, int size, int thickness) {
      System.Drawing.Color color = _pattern_found ? System.Drawing.Color.Green : System.Drawing.Color.Red; 
      Bgr bgr = new Bgr(color);
      int count = 1;
      foreach (System.Drawing.PointF corner in _corners) {
        img.Draw(new CircleF(corner, size), bgr, thickness);
        count++;
      }
    }
  }
}
