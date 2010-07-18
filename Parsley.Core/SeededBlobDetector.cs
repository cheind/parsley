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
  /// Binary image blob
  /// </summary>
  public struct Blob {
    private System.Drawing.Point[] _pixels;


    public Blob(System.Drawing.Point[] pixels) {
      _pixels = pixels;
    }

    /// <summary>
    /// Access the pixels belonging to the blob
    /// </summary>
    public System.Drawing.Point[] Pixels {
      get { return _pixels.ToArray(); }
    }
  }

  /// <summary>
  /// Detect blobs in binary images.
  /// </summary>
  /// <remarks>
  /// This algorithm takes as input a binary image and detects blobs by
  /// recursive growing from seeds.
  /// </remarks>
  public class SeededBlobDetector {

    /// <summary>
    /// Default constructor
    /// </summary>
    public SeededBlobDetector() {}

    public Blob[] DetectBlobs(
      Emgu.CV.Image<Emgu.CV.Structure.Gray, byte> image,
      Func<System.Drawing.Point, System.Drawing.Point, bool> grow_predicate,
      System.Drawing.Point[] seeds) 
    {
      System.Drawing.Rectangle r = new System.Drawing.Rectangle(System.Drawing.Point.Empty, image.Size);
      // Disjoint-Set datastructure to record each pixel
      int[] djs = new int[r.Width * r.Height];
      for (int i = 0; i < djs.Length; ++i) {
        djs[i] = -1; // -1 is uninitialized
      }
      // Init seeds
      Stack<System.Drawing.Point> stack = new Stack<System.Drawing.Point>();
      foreach (System.Drawing.Point s in seeds) {
        if (r.Contains(s)) {
          int id = IndexHelper.ArrayIndexFromPixel(s, r.Size);
          djs[id] = id;
          stack.Push(s);
        }
      }

      while (stack.Count > 0) {
        System.Drawing.Point p = stack.Pop();
        int id_p = IndexHelper.ArrayIndexFromPixel(p, r.Size);
        int p_root = FindRoot(djs, id_p);
        // Fetch neighbors
        System.Drawing.Point[] neighbors = GetNeighbors(p, r.Size);
        foreach (System.Drawing.Point n in neighbors) {
          if (grow_predicate(p, n)) {
            int id_n = IndexHelper.ArrayIndexFromPixel(n, r.Size);
            if (djs[id_n] == -1) {
              djs[id_n] = p_root;
              stack.Push(n);
            } else {
              // Merge
              int n_root = FindRoot(djs, id_n);
              djs[n_root] = p_root;
            }
            
          }
        }
      }

      // Relabel all to flatten list
      for (int i = 0; i < djs.Length; ++i) {
        djs[i] = FindRoot(djs, djs[i]);
      }
      
      // Find the number of blobs
      IEnumerable<int> roots = djs.Where(value => { return value > -1 && FindRoot(djs, value) == value; });
      List<Blob> blobs = new List<Blob>();
      for (int i = 0; i < djs.Length; ++i) {
        if (djs[i] > 0 && FindRoot(djs, i) == i) {
          List<System.Drawing.Point> pixels = new List<System.Drawing.Point>();
          for (int j = 0; j < djs.Length; ++j) {
            if (djs[j] == i) {
              pixels.Add(IndexHelper.PixelFromArrayIndex(j, r.Size));
            }
          }
          blobs.Add(new Blob(pixels.ToArray()));
        }
      }
      
      return blobs.ToArray();
    }

    private int FindRoot(int[] djs, int id_p) {
      if (id_p == -1) {
        return -1;
      } else if (djs[id_p] == id_p) {
        return id_p;
      } else {
        return FindRoot(djs, djs[id_p]);
      }
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