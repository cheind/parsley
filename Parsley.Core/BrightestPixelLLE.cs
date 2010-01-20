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
  /// Since image memory is allocated in row-major order, this algorithm process
  /// one row at a time.
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

    public override void FindLaserLine(Emgu.CV.Image<Gray, byte> channel, out float[] laser_pos) {
           
      int[] max_intensities = new int[channel.Width];
      laser_pos = new float[channel.Width];
      // Note that default float is zero.

      // Search per row
      byte[,,] data = channel.Data;
      for (int r = 0; r < channel.Height; ++r) {
        for (int c = 0; c < channel.Width; ++c) {
          int i = (int)data[r, c, 0];
          if (i > max_intensities[c]) {
            max_intensities[c] = i;
            laser_pos[c] = r;
          }
        }
      }

      // Update output: set -1 for invalid laser line poses
      for (int i = 0; i < channel.Width; ++i) {
        if (max_intensities[i] < _threshold) {
          laser_pos[i] = -1.0f;
        }
      }
    }
  }
}
