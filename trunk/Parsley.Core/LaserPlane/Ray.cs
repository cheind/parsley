using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MathNet.Numerics.LinearAlgebra;

namespace Parsley.Core.LaserPlane {
  
  /// <summary>
  /// Defines a ray through the origin
  /// </summary>
  public class Ray {
    private Vector _direction;

    /// <summary>
    /// Initialize ray from direction
    /// </summary>
    /// <param name="origin">Origin of ray</param>
    /// <param name="direction">Direction of ray as unit-length vector</param>
    public Ray(Vector direction) {
      _direction = direction;
    }

    /// <summary>
    /// Get direction of ray
    /// </summary>
    public Vector Direction {
      get { return _direction; }
    }

    /// <summary>
    /// Evaluate the parameteric ray equation x = o + t*direction, where o is
    /// the zero-point.
    /// </summary>
    /// <returns>Position on ray</returns>
    public Vector At(double t) {
      return _direction * t;
    }
  }
}
