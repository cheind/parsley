using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Parsley.Core {
  /// <summary>
  /// Stores user-data per pixel.
  /// </summary>
  /// <remarks>Uses a dense-storage to optimize lookup</remarks>
  public class DensePixelGrid<T> {
    private T[] _data;
    private System.Drawing.Size _size;

    /// <summary>
    /// Construct pixel grid from size
    /// </summary>
    /// <param name="s">Size of pixel grid</param>
    public DensePixelGrid(System.Drawing.Size s) {
      _data = new T[s.Width * s.Height];
      _size = s;
    }

    /// <summary>
    /// Construct empty
    /// </summary>
    public DensePixelGrid() {}

    /// <summary>
    /// Get/set size of pixel grid
    /// </summary>
    public System.Drawing.Size Size{
      get { return _size; }
      set {
        _size = value;
        _data = new T[_size.Width * _size.Height];
      }
    }

    /// <summary>
    /// Access pixel-data
    /// </summary>
    public T[] PixelData {
      get { return _data; }
    }

    /// <summary>
    /// Test was set previously.
    /// </summary>
    /// <remarks>Uses comparison with default(T).</remarks>
    /// <param name="pixel">Pixel</param>
    /// <returns>True if pixel-value was set previously</returns>
    public bool IsSet(System.Drawing.Point pixel) {
      return !EqualityComparer<T>.Default.Equals(
        _data[IndexHelper.ArrayIndexFromPixel(pixel, _size)], 
        default(T));
    }

    /// <summary>
    /// Access value at pixel
    /// </summary>
    /// <param name="pixel">Pixel</param>
    /// <returns>Value at pixel</returns>
    public T this[System.Drawing.Point pixel] {
      get {
        return _data[IndexHelper.ArrayIndexFromPixel(pixel, _size)];
      }
      set {
        _data[IndexHelper.ArrayIndexFromPixel(pixel, _size)] = value;
      }
    }
  }
}
