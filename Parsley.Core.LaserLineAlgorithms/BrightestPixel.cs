using System;
using System.Collections.Generic;
using System.Text;

using Emgu.CV.Structure;
using System.ComponentModel;

namespace Parsley.Core.LaserLineAlgorithms {
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
  [Serializable]
  [Addins.Addin]
  public class BrightestPixel : ILaserLineAlgorithm {
    private int _threshold;

    /// <summary>
    /// Initialize with no threshold
    /// </summary>
    public BrightestPixel() {
      _threshold = 220;
    }

    /// <summary>
    /// Initialize with threshold
    /// </summary>
    /// <param name="threshold">Minimum intensity threshold</param>
    public BrightestPixel(int threshold) {
      _threshold = threshold;
    }

    /// <summary>
    /// Get and set the intensity threshold.
    /// </summary>
    [Description("Set minimum intensity value for valid laser point")]
    public int IntensityThreshold {
      get { return _threshold; }
      set { _threshold = value; }
    }

    public bool FindLaserLine(ILaserLineAlgorithmContext context, out System.Drawing.PointF[] laser_pos) {
      Emgu.CV.Image<Gray, byte> channel = context.ChannelImage;
      int[] max_intensities = new int[channel.Width];
      float[] range = new float[channel.Width];
      // Note that default float is zero.

      // Search per row
      byte[] d = channel.Bytes;
      int stride = d.Length / channel.Height;
      int h = channel.Height; // This one here is a huge, HUGE timesaver!
      int w = channel.Width; // This one here is a huge, HUGE timesaver!
      
      unchecked {
        for (int r = 0; r < h; ++r) {
          int offset = stride * r;
          for (int c = 0; c < w; ++c) {
            byte i = d[offset + c];
            if (i > max_intensities[c]) {
              max_intensities[c] = i;
              range[c] = r;
            }
          }
        }
        // Update output: set -1 for invalid laser line poses
        laser_pos = new System.Drawing.PointF[channel.Width];
        for (int i = 0; i < w; ++i) {
          if (max_intensities[i] >= _threshold) {
            laser_pos[i] = new System.Drawing.PointF(i, range[i]);
          }
        }
      }

      return true;
    }
  }
}
