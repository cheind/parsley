using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Emgu.CV.Structure;

namespace Parsley.Core {
  /// <summary>
  /// Laser line extraction based on the brightest pixel
  /// </summary>
  /// <remarks>
  /// Algorithms scans reports the brightest, maximum intensity, y-value per x-value 
  /// if it exceeds a user controlable threshold. If no y-value exceeds the threshold,
  /// the algorithm reports -1.0f for this x-value. This algorithm is implemented using 
  /// a single pass.
  /// 
  /// The image memory layout should have a column-major layout in order to reduce
  /// chaching misses. OpenCV stores images as one-dimensional array, in row-major
  /// form. Todo: Consider flipping the image and providing a Flipped property. When
  /// using the flipped property the resulting laser points will be adapted to
  /// conform with the original image.
  /// </remarks>
  public class BrightestPixelLLE : LaserLineExtraction {
    private int _threshold;

    /// <summary>
    /// Initialize with no threshold
    /// </summary>
    public BrightestPixelLLE() {
      _threshold = 0;
    }

    /// <summary>
    /// Initialize with threshold
    /// </summary>
    /// <param name="threshold">Minimum intensity threshold</param>
    public BrightestPixelLLE(int threshold) {
      _threshold = threshold;
    }

    /// <summary>
    /// Get and set the intensity threshold.
    /// </summary>
    public int IntensityThreshold {
      get { return _threshold; }
      set { _threshold = value; }
    }

    public override void FindLaserLine(Emgu.CV.Image<Gray, byte> channel, out System.Drawing.PointF[] laser_points) {
      laser_points = new System.Drawing.PointF[channel.Width];
      // Search rows
      int max_i = 0;
      int max_pos = 0;
      for (int c = 0; c < channel.Width; ++c) {
        this.FindMax(channel, c, ref max_pos, ref max_i);
        laser_points[c].X = c;
        laser_points[c].Y = (max_i >= _threshold) ? max_pos : -1;
      }
    }

    void FindMax(Emgu.CV.Image<Gray, byte> channel, int column, ref int max_pos, ref int max_i) {
      max_i = 0;
      max_pos = -1;
      for (int k = 0; k < channel.Height; ++k) {
        int i = (int)channel[k, column].Intensity;
        if (i > max_i) {
          max_i = i;
          max_pos = k;
        }
      }
    }

  }
}
