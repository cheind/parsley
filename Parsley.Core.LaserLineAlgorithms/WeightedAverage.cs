/*
 * Parsley http://parsley.googlecode.com
 * Copyright (c) 2010, Christoph Heindl. All rights reserved.
 * Code license:	New BSD License
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Emgu.CV.Structure;

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
        // See http://www-uxsup.csx.cam.ac.uk/~fanf2/hermes/doc/antiforgery/stats.pdf
        weights += w;
        iwa += (w / weights) * (v - iwa);
      }
    }

    public bool FindLaserLine(Bundle bundle) {
      BundleBookmarks b = new BundleBookmarks(bundle);
      Emgu.CV.Image<Bgr, byte> image = b.Image;
      int laser_color = (int)b.LaserColor;

      using (Emgu.CV.Image<Gray, byte> channel = image[laser_color]) {
        List<System.Drawing.PointF> pixels = ExtractPoints(channel);
        b.LaserPixel = pixels;
        return pixels.Count > 0;
      }
    }


    private List<System.Drawing.PointF> ExtractPoints(Emgu.CV.Image<Gray, byte> channel) {
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

      List<System.Drawing.PointF> pixels = new List<System.Drawing.PointF>();
      for (int i = 0; i < w; ++i) {
        if (iwas[i].iwa > 0) {
          pixels.Add(new System.Drawing.PointF(i, (float)iwas[i].iwa));
        }
      }

      return pixels;
    }
  }
}
