using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Parsley.Core {

  /// <summary>
  /// Weighted average laser line extraction
  /// </summary>
  public class WeightedAverageLLE : LaserLineExtraction {
    private int _threshold;

    /// <summary>
    /// Initialize with no threshold
    /// </summary>
    public WeightedAverageLLE() {
      _threshold = 0;
    }

    /// <summary>
    /// Initialize with threshold
    /// </summary>
    /// <param name="threshold">Minimum intensity threshold</param>
    public WeightedAverageLLE(int threshold) {
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

      public void Update(double v, double w) {
        weights += w;
        iwa += (w/weights) * (v - iwa);
      }
    }

    public override void FindLaserLine(Emgu.CV.Image<Emgu.CV.Structure.Gray, byte> channel, out float[] laser_pos) {
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
            if (i > _threshold)
              iwas[c].Update(r, i);
          }
        }
      }

      // Update output: set -1 for invalid laser line poses
      laser_pos = new float[w];
      for (int i = 0; i < w; ++i) {
        if (iwas[i].iwa > 0) {
          laser_pos[i] = (float)iwas[i].iwa;
        } else {
          laser_pos[i] = -1.0f;
        }
      }
    }
  }
}
