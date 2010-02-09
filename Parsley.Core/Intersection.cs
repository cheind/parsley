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
    public static void FindEyeRayPlaneIntersections(Ray[] eye_rays, IEnumerable<Plane> planes, ref double[] isect_t, ref Vector[] isect_p) {
      isect_t = new double[eye_rays.Length];
      for (int i = 0; i < eye_rays.Length; ++i) {
        Ray r = eye_rays[i];
        double t = Double.MaxValue;
        foreach (Plane p in planes) {
          double this_t;
          Intersection.RayPlane(r, p, out this_t);
          if (this_t < t) t = this_t;
        }
        isect_t[i] = t;
        if (isect_p != null) isect_p[i] = r.At(t);
      }
    }

  }
}
