using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

using MathNet.Numerics.LinearAlgebra;

namespace Parsley.Core {

  /// <summary>
  /// Base class for extracting laser-planes.
  /// </summary>
  [Serializable]
  public abstract class LaserPlaneExtraction {

    /// <summary>
    /// Find best intersection of eye-rays and reference planes.
    /// </summary>
    /// <remarks>Best is defined as the the plane with the shortest distance</remarks>
    /// <param name="eye_rays">Eye-rays</param>
    /// <param name="planes">Calibrated reference planes</param>
    /// <param name="isect_t">Parametric ray intersection distance</param>
    /// <param name="isect_p">Intersection points (optional).</param>
    protected static void FindEyeRayPlaneIntersections(Ray[] eye_rays, IEnumerable<Plane> planes, ref double[] isect_t, ref Vector[] isect_p) {
      isect_t = new double[eye_rays.Length];
      for (int i = 0; i < eye_rays.Length; ++i) {
        Ray r = eye_rays[i];
        double t = Double.MaxValue;
        foreach (Plane p in planes) {
          double this_t;
          Core.Intersection.RayPlane(r, p, out this_t);
          if (this_t < t) t = this_t;
        }
        isect_t[i] = t;
        if (isect_p != null) isect_p[i] = r.At(t);
      }
    }

    /// <summary>
    /// Find laser-plane through detected laser-points.
    /// </summary>
    /// <remarks>Method is only invoked when at least 3 laser_points have been detected.</remarks>
    /// <param name="eye_rays">Eye-rays for each laser-points</param>
    /// <param name="reference_planes">Calibrated reference planes</param>
    /// <returns>True on success, false otherwise</returns>
    public abstract bool FindLaserPlane(Ray[] eye_rays, IEnumerable<Plane> reference_planes, out Plane plane);

  }
}
