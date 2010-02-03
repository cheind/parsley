using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MathNet.Numerics.LinearAlgebra;

namespace Parsley.Core {

  /// <summary>
  /// Accumulates points in three-dimensions per pixel in a region of interest.
  /// </summary>
  public abstract class PointPerPixelAccumulator {
    System.Drawing.Rectangle _roi;
    uint[] _ids;

    /// <summary>
    /// Initialize with region of interest.
    /// </summary>
    /// <param name="roi">ROI</param>
    public PointPerPixelAccumulator(System.Drawing.Rectangle roi) {
      _roi = roi;
      _ids = new uint[roi.Width * roi.Height];
    }

    /// <summary>
    /// Access the ROI
    /// </summary>
    public System.Drawing.Rectangle ROI {
      get { return _roi; }
    }

    /// <summary>
    /// Accumulate a new point
    /// </summary>
    /// <param name="pixel">Pixel the point belongs. Relative to roi coordinate frame</param>
    /// <param name="point">Coordinates of point</param>
    /// <param name="first_point">True if given point was first point in pixel</param>
    public abstract void Accumulate(System.Drawing.Point pixel, Vector point, out bool first_point);

    /// <summary>
    /// Access the current accumulation state of the given pixel.
    /// </summary>
    /// <param name="pixel">Pixel to query. Relative to roi coordinate frame.</param>
    /// <returns>Vector</returns>
    public abstract Vector Extract(System.Drawing.Point pixel);

    /// <summary>
    /// Associate an id with the given pixel
    /// </summary>
    /// <param name="pixel">Pixel relative to roi coordinate frame</param>
    /// <param name="id">Id</param>
    public void SetId(System.Drawing.Point pixel, uint id) {
      _ids[ArrayIndexFromPixel(pixel)] = id;
    }

    /// <summary>
    /// Retrieve the associated pixel id
    /// </summary>
    /// <param name="pixel">Pixel relative to roi coordinate frame</param>
    /// <returns>Id</returns>
    public uint GetId(System.Drawing.Point pixel) {
      return _ids[ArrayIndexFromPixel(pixel)];
    }

    /// <summary>
    /// Convert pixel coordinates to ROI local coordinate system
    /// </summary>
    /// <param name="pixel"></param>
    /// <returns></returns>
    public System.Drawing.Point MakeRelativeToROI(System.Drawing.Point pixel) {
      return new System.Drawing.Point(pixel.X - _roi.X, pixel.Y - _roi.Y);
    }

    /// <summary>
    /// Convert pixel to one-dimensional array index
    /// </summary>
    /// <param name="pixel"></param>
    /// <returns></returns>
    protected int ArrayIndexFromPixel(System.Drawing.Point pixel) {
      return pixel.Y * _roi.Width + pixel.X;
    }

  }
}
