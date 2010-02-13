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
  public class Intersection {

    /// <summary>
    /// Ray/Plane intersection
    /// </summary>
    /// <param name="r">Ray</param>
    /// <param name="p">Plane</param>
    /// <param name="t">Parametric t</param>
    /// <returns></returns>
    public static bool RayPlane(Ray r, Plane p, out double t) {
      // http://www.siggraph.org/education/materials/HyperGraph/raytrace/rayplane_intersection.htm
      double denom = Vector.ScalarProduct(p.Normal, r.Direction);
      if (Math.Abs(denom) > 1e-10) {
        // Since the ray passes through the origin, the intersection calculation can
        // be greatly simplified.
        t = -p.D / denom;
        return true;
      } else {
        t = Double.MaxValue;
        return false;
      }
    }

    /// <summary>
    /// Find best intersection of eye-rays and reference planes.
    /// </summary>
    /// <remarks>Best is defined as the the plane with the shortest distance</remarks>
    /// <param name="eye_rays">Eye-rays</param>
    /// <param name="planes">Calibrated reference planes</param>
    /// <param name="isect_t">Parametric ray intersection distance</param>
    /// <param name="isect_p">Intersection points (optional).</param>
    public static void FindEyeRayPlaneIntersections(Ray[] eye_rays, Plane[] planes, out double[] isect_t, out Vector[] isect_p, out int[] isect_plane_ids) {
      isect_t = new double[eye_rays.Length];
      isect_p = new Vector[eye_rays.Length];
      isect_plane_ids = new int[eye_rays.Length];

      for (int i = 0; i < eye_rays.Length; ++i) {
        Ray r = eye_rays[i];
        double t = Double.MaxValue;
        int id = -1;
        for (int p = 0; p < planes.Length; ++p) {
          double this_t;
          Intersection.RayPlane(r, planes[p], out this_t);
          if (this_t < t) {
            t = this_t;
            id = p;
          } 
        }
        isect_t[i] = t;
        isect_p[i] = r.At(t);
        isect_plane_ids[i] = id;
      }
    }

  }
}
