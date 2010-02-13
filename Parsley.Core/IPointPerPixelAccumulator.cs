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

namespace Parsley.Core {

  /// <summary>
  /// Interface for algorithms that accumulate points per pixels
  /// </summary>
  public interface IPointPerPixelAccumulator {

    /// <summary>
    /// Set new size
    /// </summary>
    /// <remarks>Discard old values</remarks>
    System.Drawing.Size Size { set; }


    /// <summary>
    /// Accumulate next point
    /// </summary>
    /// <param name="pixel">Pixel</param>
    /// <param name="r"></param>
    /// <param name="t"></param>
    void Accumulate(System.Drawing.Point pixel, Ray r, double t);


    /// <summary>
    /// Access the current accumulation state of the given pixel.
    /// </summary>
    /// <param name="pixel">Pixel to query</param>
    /// <returns>Vector</returns>
    Vector Extract(System.Drawing.Point pixel);

    /// <summary>
    /// Reset accumator entries
    /// </summary>
    void Reset();


    /// <summary>
    /// Access all points
    /// </summary>
    IEnumerable<Vector> Points {
      get;
    }
  }
}
