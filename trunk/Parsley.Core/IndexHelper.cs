using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Parsley.Core {
  /// <summary>
  /// Help convert two-dimensional indices to one-dimensional array indices
  /// </summary>
  public static class IndexHelper {

    /// <summary>
    /// Convert pixel to one-dimensional array index
    /// </summary>
    /// <param name="pixel">Pixel to convert to index</param>
    /// <returns>Index</returns>
    public static int ArrayIndexFromPixel(System.Drawing.Point pixel, System.Drawing.Size s) {
      return pixel.Y * s.Width + pixel.X;
    }

    /// <summary>
    /// Convert array index to pixel
    /// </summary>
    /// <param name="i">Array index</param>
    /// <param name="s">Size of image described by array</param>
    /// <returns>Pixel</returns>
    public static System.Drawing.Point PixelFromArrayIndex(int i, System.Drawing.Size s) {
      return new System.Drawing.Point(i % s.Width, i / s.Width);
    }

    /// <summary>
    /// Make pixel coordinate relative to rectangle frame
    /// </summary>
    /// <param name="pixel">Pixel</param>
    /// <param name="r">Rectangle</param>
    /// <returns>Relative coordinates</returns>
    public static System.Drawing.Point MakeRelative(System.Drawing.Point pixel, System.Drawing.Rectangle r) {
      return new System.Drawing.Point(pixel.X - r.X, pixel.Y - r.Y);
    }
  }
}
