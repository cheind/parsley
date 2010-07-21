/*
 * Parsley http://parsley.googlecode.com
 * Copyright (c) 2010, Christoph Heindl. All rights reserved.
 * Code license:	New BSD License
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MathNet.Numerics.LinearAlgebra;
using System.Runtime.Serialization;
using Parsley.Core.Extensions;
using System.ComponentModel;

namespace Parsley.Core {

  /// <summary>
  /// Accumulates points by median
  /// </summary>
  [Serializable]
  public class MedianPointAccumulator : IPointPerPixelAccumulator, ISerializable {  
    private int _max_entries;
    [NonSerialized]
    private DensePixelGrid<PerPixel> _grid;

    /// <summary>
    /// Construct empty accumulator
    /// </summary>
    public MedianPointAccumulator() {
      _max_entries = 100;
      _grid = new DensePixelGrid<PerPixel>();
    }

    public MedianPointAccumulator(SerializationInfo info, StreamingContext context)
    {
      _max_entries = info.GetValue<int>("_max_entries");
      _grid = new DensePixelGrid<PerPixel>();
    }


    public void GetObjectData(SerializationInfo info, StreamingContext context) {
      info.AddValue("_max_entries", _max_entries);
    }

    
    public System.Drawing.Size Size {
      set {
        _grid.Size = value;
      }
    }

    public void Accumulate(System.Drawing.Point pixel, Ray r, double t) {
      PerPixel pp = _grid[pixel];
      if (pp == null) {
        pp = new PerPixel(_max_entries, r, t);
        _grid[pixel] = pp;
      } else {
        pp.Add(t);
      }
    }

    [Browsable(false)]
    public IEnumerable<Vector> Points {
      get {
        foreach (PerPixel pp in _grid.PixelData) {
          if (pp != null) {
            yield return pp.Median();
          }
        }
      }
    }

    /// <summary>
    /// Extract point from pixel. At least one point needs to be accumulated
    /// </summary>
    /// <param name="pixel">Pixel relative to ROI</param>
    /// <returns>Point coordinates</returns>
    public Vector Extract(System.Drawing.Point pixel) {
      return _grid[pixel].Median();
    }

    public void Reset() {
      _grid.Reset();
    }

    /// <summary>
    /// Defines the entity stored per pixel
    /// </summary>
    class PerPixel {
      private List<double> _ts;
      private Ray _r;

      /// <summary>
      /// Construct from capacity and first entry
      /// </summary>
      /// <param name="capacity"></param>
      /// <param name="first"></param>
      public PerPixel(int capacity, Ray r, double t) {
        _ts = new List<double>(capacity);
        _ts.Add(t);
        _r = r;
      }

      public void Add(double t) {
        if (_ts.Count < _ts.Capacity) {
          _ts.Insert(~_ts.BinarySearch(t), t);
        }
      }

      public Vector Median() {
        double t = _ts[_ts.Count / 2];
        return _r.At(t);
      }
    }


    #region ISerializable Member



    #endregion
  }
}
