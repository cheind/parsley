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
    /// Construct checkboard from the number of inner corners per row/column
    /// and size of check-field.
    /// </summary>
    public CheckerBoard(int corner_row, int corner_column) {
      _inner_corners = new System.Drawing.Size(corner_row, corner_column);
      _pattern_found = false;
    }

    public MCvPoint3D32f[] ObjectCorners(float field_size) {
      MCvPoint3D32f[] corners = new MCvPoint3D32f[_inner_corners.Width * _inner_corners.Height];
      for (int y = 0; y < _inner_corners.Height; ++y) {
        for (int x = 0; x < _inner_corners.Width; x++) {
          int id = y * _inner_corners.Width + x;
          corners[id].x = x * field_size;
          corners[id].y = y * field_size;
          corners[id].z = 0.0f;
        }
      }
      return corners;
    }

    public System.Drawing.PointF[] ImageCorners {
      get { return _corners; }
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

    public bool PatternFound {
      get { return _pattern_found; }
    }

    /// <summary>
    /// Draw found pattern to image.
    /// </summary>
    /// <param name="img"></param>
    /// <param name="size"></param>
    /// <param name="thickness"></param>
    public void Draw(Emgu.CV.Image<Bgr, Byte> img, int size, int thickness) {
      System.Drawing.Color color = _pattern_found ? System.Drawing.Color.Green : System.Drawing.Color.Red;
      Bgr bgr = new Bgr(color);
      MCvFont f = new MCvFont(Emgu.CV.CvEnum.FONT.CV_FONT_HERSHEY_PLAIN, 0.8, 0.8); //Create the font
      int count = 1;
      foreach (System.Drawing.PointF corner in _corners) {
        img.Draw(new CircleF(corner, size), bgr, thickness);
        img.Draw(count.ToString(), ref f, new System.Drawing.Point((int)corner.X + 5,(int)corner.Y - 5), bgr);
        count++;
      }
    }
  }
}
