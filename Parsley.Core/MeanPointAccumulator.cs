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
  /// Accumulates points by mean value
  /// </summary>
  [Serializable]
  [Parsley.Core.Addins.Addin]
  public class MeanPointAccumulator : IPointPerPixelAccumulator
  {  
    /// <summary>
    /// Memory used to save data for every captured pixel
    /// with the structure PerPixel.
    /// The size of the DensePixelGrid object is given by
    /// the ROI size
    /// </summary>
    [NonSerialized]
    private DensePixelGrid<PerPixel> _grid = null;

    /// <summary>
    /// Construct empty accumulator
    /// </summary>
    public MeanPointAccumulator() {}
    
    /// <summary>
    /// Set the size of the DensePixelGrid object.
    /// </summary>
    public System.Drawing.Size Size {
      set {
        if (_grid == null)
          _grid = new DensePixelGrid<PerPixel>();
        _grid.Size = value;
      }
    }

    /// <summary>
    /// Stores the captured pixel in the grid.
    /// If the value of "pixel" corresponds to point which is
    /// already stored in the list, the mean t will be updated.
    /// </summary>
    /// <param name="pixel"></param>
    /// <param name="r"></param>
    /// <param name="t"></param>
    public void Accumulate(System.Drawing.Point pixel, Ray r, double t) {
      PerPixel pp = _grid[pixel];
      if (pp == null) {
        pp = new PerPixel(r, t);
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
            yield return pp.Mean();
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
      return _grid[pixel].Mean();
    }

    public void Reset() {
      _grid.Reset();
    }

    /// <summary>
    /// Defines the entity stored per pixel:
    /// _tmean: mean value of the pixels t's
    /// _t_count: number of t-values given for the pixel
    /// _r: ray of the pixel (needed to calculate the mean point)
    /// </summary>
    class PerPixel {
      private double _tmean;
      private long _t_count;
      private Ray _r;

      /// <summary>
      /// Construct PerPixel from first entry:
      /// Since this the first t-value is passed to the constructor,
      /// t_mean is set to t.
      /// </summary>
      /// <param name="first"></param>
      public PerPixel(Ray r, double t) {
        _tmean = t;
        _r = r;
        _t_count = 1;
      }

      /// <summary>
      /// Calculates the new mean value of t, when
      /// another value t is found for the corresponding pixel.
      /// </summary>
      /// <param name="t"></param>
      public void Add(double t) {
        _t_count = _t_count + 1;
        _tmean = _tmean + (t - _tmean) / _t_count;
      }

      /// <summary>
      /// Passes the mean 3d point to the calling function.
      /// (evaluation of the ray-equation)
      /// </summary>
      /// <returns></returns>
      public Vector Mean() {
        return _r.At(_tmean);
      }
    }
  }
}
