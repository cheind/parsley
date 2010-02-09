using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Parsley.Core.LaserLineAlgorithms {

  /// <summary>
  /// Weighted average laser line extraction
  /// </summary>
  [Serializable]
  [Addins.Addin]
  public class WeightedAverage : ILaserLineAlgorithm {
    private int _threshold;

    /// <summary>
    /// Initialize with no threshold
    /// </summary>
    public WeightedAverage() {
      _threshold = 220;
    }

    /// <summary>
    /// Initialize with threshold
    /// </summary>
    /// <param name="threshold">Minimum intensity threshold</param>
    public WeightedAverage(int threshold) {
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

    struct IncWeightedAverage {
      public double iwa;     // incremental weighted average
      public double weights; // sum of weights
      public bool stop;

      public void Update(double v, double w) {
        weights += w;
        iwa += (w/weights) * (v - iwa);
      }
    }

    public bool FindLaserLine(ILaserLineAlgorithmContext context, out System.Drawing.PointF[] laser_pos) {
      Emgu.CV.Image<Emgu.CV.Structure.Gray, byte> channel = context.ChannelImage;
      IncWeightedAverage[] iwas = new IncWeightedAverage[channel.Width];

      // Search per row
      byte[] d = channel.Bytes;
      int stride = d.Length / channel.Height;
      int h = channel.Height; 
      int w = channel.Width;

      // See http://www.cse.iitm.ac.in/~cs670/book/node57.html

      unchecked {
        for (int r = 0; r < h; ++r) {
          int offset = stride * r;
          for (int c = 0; c < w; ++c) {
            byte i = d[offset + c];
            if (i < _threshold) {
              iwas[c].stop |= iwas[c].weights > 0;
            } else {
              if (!iwas[c].stop)
                iwas[c].Update(r, i);
            }
          }
        }
      }

      // Update output: set -1 for invalid laser line poses
      laser_pos = new System.Drawing.PointF[w];
      for (int i = 0; i < w; ++i) {
        if (iwas[i].iwa > 0) {
          laser_pos[i] = new System.Drawing.PointF(i, (float)iwas[i].iwa);
        }
      }
      return true;
    }
  }
}
