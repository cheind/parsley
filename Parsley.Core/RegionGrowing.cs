/*
 * Parsley http://parsley.googlecode.com
 * Copyright (c) 2010, Christoph Heindl. All rights reserved.
 * Code license:	New BSD License
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Parsley.Core {

  /// <summary>
  /// Segmented region
  /// </summary>
  public struct Region {
    private IEnumerable<System.Drawing.Point> _pixels;


    public Region(IEnumerable<System.Drawing.Point> pixels) {
      _pixels = pixels;
    }

    /// <summary>
    /// Access the pixels belonging to the blob
    /// </summary>
    public IEnumerable<System.Drawing.Point> Pixels {
      get { return _pixels; }
    }
  }

  /// <summary>
  /// Detect blobs in images by recursive region growing.
  /// </summary>
  public class RegionGrowing {

    /// <summary>
    /// Default constructor
    /// </summary>
    public RegionGrowing() {}

    /// <summary>
    /// Find regions in image.
    /// </summary>
    /// <param name="image">Gray-scaled image</param>
    /// <param name="grow_predicate">Grow predicate</param>
    /// <param name="seeds">Seeds to grow from</param>
    /// <returns>Segmented regions</returns>
    public Region[] FindRegions(
      Emgu.CV.Image<Emgu.CV.Structure.Gray, byte> image,
      Func<System.Drawing.Point, System.Drawing.Point, bool> grow_predicate,
      System.Drawing.Point[] seeds) 
    {
      System.Drawing.Rectangle r = new System.Drawing.Rectangle(System.Drawing.Point.Empty, image.Size);
      // Disjoint-Set datastructure to record each pixel
      DisjointSet ds = new DisjointSet(new DisjointSet.DenseContainer());
      ds.Initialize(r.Width * r.Height);
      // Init seeds
      Stack<System.Drawing.Point> stack = new Stack<System.Drawing.Point>();
      foreach (System.Drawing.Point s in seeds) {
        if (r.Contains(s) && grow_predicate(new System.Drawing.Point(-1, -1), s)) {
          int id = IndexHelper.ArrayIndexFromPixel(s, r.Size);
          ds.MakeSet(id);
          stack.Push(s);
        }
      }

      while (stack.Count > 0) {
        System.Drawing.Point p = stack.Pop();
        int id_p = IndexHelper.ArrayIndexFromPixel(p, r.Size);
        int p_root = ds.FindRoot(id_p);
        // Fetch neighbors
        System.Drawing.Point[] neighbors = GetNeighbors(p, r.Size);
        foreach (System.Drawing.Point n in neighbors) {
          if (grow_predicate(p, n)) {
            int id_n = IndexHelper.ArrayIndexFromPixel(n, r.Size);
            if (ds.FindRoot(id_n) == -1) {
              ds.Set(id_n, p_root);
              stack.Push(n);
            } else {
              // Merge
              ds.Merge(id_n, p_root);
            }
          }
        }
      }
      
      List<Region> blobs = new List<Region>();
      foreach (List<int> e in ds.FindSetElements(ds.Roots)) {
        List<System.Drawing.Point> pixels = e.ConvertAll<System.Drawing.Point>(
          value => { return IndexHelper.PixelFromArrayIndex(value, r.Size); }
        );
        blobs.Add(new Region(pixels));
      }
      return blobs.ToArray();
    }

    private System.Drawing.Point[] GetNeighbors(System.Drawing.Point p, System.Drawing.Size size) {
      List<System.Drawing.Point> n = new List<System.Drawing.Point>();
      // top
      if (p.Y > 0)
        n.Add(new System.Drawing.Point(p.X, p.Y - 1));
      // bottom
      if (p.Y < size.Height - 1)
        n.Add(new System.Drawing.Point(p.X, p.Y + 1));
      // left
      if (p.X > 0)
        n.Add(new System.Drawing.Point(p.X - 1, p.Y));
      // right
      if (p.X < size.Width - 1)
        n.Add(new System.Drawing.Point(p.X + 1, p.Y));
      return n.ToArray();
    }


  }
}