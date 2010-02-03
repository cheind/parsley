using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MathNet.Numerics.LinearAlgebra;

namespace Parsley.Core {
  public class MedianPointAccumulator : PointPerPixelAccumulator {  
    /// <summary>
    /// Defines the entity stored per pixel
    /// </summary>
    class PerPixel {
      public List<double> x;
      public List<double> y;
      public List<double> z;

      /// <summary>
      /// Construct from capacity and first entry
      /// </summary>
      /// <param name="capacity"></param>
      /// <param name="first"></param>
      public PerPixel(int capacity, Vector first) {
        x = new List<double>(capacity);
        y = new List<double>(capacity);
        z = new List<double>(capacity);
        x.Add(first[0]);
        y.Add(first[1]);
        z.Add(first[2]);
      }

      public void Add(Vector v) {
        if (x.Count < x.Capacity) {
          x.Insert(~x.BinarySearch(v[0]), v[0]);
          y.Insert(~y.BinarySearch(v[1]), v[1]);
          z.Insert(~z.BinarySearch(v[2]), v[2]);
        }
      }

      public Vector Median() {
        Vector v = new Vector(3);
        int count = x.Count;
        v[0] = x[count / 2];
        v[1] = y[count / 2];
        v[2] = z[count / 2];
        return v;
      }
    }

    private int _max_entries;
    private PerPixel[] _entries;

    /// <summary>
    /// Create with region of interest and maximum number of median entries per pixel.
    /// </summary>
    /// <param name="roi">ROI</param>
    /// <param name="max_entries">Maximum number of median entries per pixel</param>
    public MedianPointAccumulator(System.Drawing.Rectangle roi, int max_entries)
      : base(roi) 
    {
      _max_entries = max_entries;
      _entries = new PerPixel[roi.Width * roi.Height];
    }

    public override void Accumulate(System.Drawing.Point pixel, Vector point, out bool first_point) {
      int id = this.ArrayIndexFromPixel(pixel);
      PerPixel pp = _entries[id];
      if (pp == null) {
        first_point = true;
        pp = new PerPixel(_max_entries, point);
        _entries[id] = pp;
      } else {
        first_point = false;
        pp.Add(point);
      }
    }

    
    /// <summary>
    /// Extract point from pixel. At least one point needs to be accumulated
    /// </summary>
    /// <param name="pixel">Pixel relative to ROI</param>
    /// <returns>Point coordinates</returns>
    public override Vector Extract(System.Drawing.Point pixel) {
      // Assumes at least one elemt
      return _entries[this.ArrayIndexFromPixel(pixel)].Median();
    }
  }
}
