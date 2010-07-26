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
using System.ComponentModel;

namespace Parsley.Core.LaserPlaneAlgorithms {

  /// <summary>
  /// Flags suitable constraints for plane based ransac approach
  /// </summary>
  public interface IPlaneRansacConstraint : IRansacModelConstraint {
    [Browsable(false)]
    Bundle Bundle {
      set;
    }
  };

  /// <summary>
  /// Reject planes that are parallel to reference planes.
  /// </summary>
  [Addins.Addin]
  [Serializable]
  public class RejectParallelPlanes : IPlaneRansacConstraint {
    [NonSerialized]
    private BundleBookmarks _b;

    public RejectParallelPlanes() { }

    public Bundle Bundle {
      set { _b = new BundleBookmarks(value); }
    }

    public bool Test(IRansacModel model) {
      
      PlaneModel r = model as PlaneModel;
      bool no_parallel_found = true;
      foreach (Plane p in _b.ReferencePlanes) {
        double d = Vector.ScalarProduct(r.Plane.Normal, p.Normal);
        if ((1.0 - Math.Abs(d)) < 1e-2) {
          no_parallel_found = false;
          break;
        }
      }
      return no_parallel_found;
    }
  }
}
