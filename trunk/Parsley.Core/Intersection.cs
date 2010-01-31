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

  }
}
