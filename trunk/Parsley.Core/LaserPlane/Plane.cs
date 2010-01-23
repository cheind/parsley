using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MathNet.Numerics.LinearAlgebra;

namespace Parsley.Core.LaserPlane {

  /// <summary>
  /// A plane in three dimensions represented in 
  /// its parametric form a*x+b*y+c*z = d
  /// </summary>
  public class Plane {
    private Vector _normal;
    private double _d;

    /// <summary>
    /// Construct plane from point and unit normal
    /// </summary>
    /// <param name="point">point on plane</param>
    /// <param name="normal">unit normal of plane</param>
    public Plane(Vector point, Vector normal) {
      _normal = normal.Normalize(); // Make sure it is normalized!
      _d = -Vector.ScalarProduct(point, normal);
    }

    /// <summary>
    /// Construct plane from three non colinear points
    /// </summary>
    /// <param name="a">First point</param>
    /// <param name="b">Second point</param>
    /// <param name="c">Third point</param>
    /// <exception cref="ArgumentException">When points are colinear</exception>
    public Plane(Vector a, Vector b, Vector c) {
      Vector n = Vector.CrossProduct(b - a, c - a);
      double norm = n.Norm();
      if (norm > 0) {
        _normal = n / norm;
        _d = -Vector.ScalarProduct(a, _normal);
      } else {
        throw new ArgumentException("Cannot create plane from co-linear points");
      }
    }

    /// <summary>
    /// Access D
    /// </summary>
    public double D {
      get { return _d; }
    }

    /// <summary>
    /// Access normal
    /// </summary>
    public Vector Normal {
      get { return _normal; }
    }



    /// <summary>
    /// Calculate the distance from a point to this plane
    /// </summary>
    /// <param name="x">Query point</param>
    /// <returns>Signed distance of x to plane, which is positive
    /// when x lies on the same side of the plane as the plane's normal,
    /// negative otherwise.</returns>
    public double DistanceTo(Vector x) {
      return Vector.ScalarProduct(x, _normal) + _d;
    }

    /// <summary>
    /// Fit plane through points by linear orthogonal regression.
    /// </summary>
    /// <param name="points">Points to fit by plane</param>
    /// <returns>Best-fit plane in terms of orthogonal least square regression.</returns>
    public static Plane FitThrough(IEnumerable<Vector> points) {
      // Perform orth lin regression by PCA which requires the estimation
      // of the covariance matrix of the samples
      // http://en.wikipedia.org/wiki/Estimation_of_covariance_matrices

      int count = 0;
      Vector mean = new Vector(3, 0);
      foreach (Vector p in points) {
        mean += p; count++;
      }
      if (count < 3)
        throw new ArgumentException("Three points are needed at least to fit a plane");
      mean /= count;

      Matrix cov = new Matrix(3, 3, 0);
      foreach (Vector p in points) {
        Vector v = p - mean;
        cov += v.ToColumnMatrix() * v.ToRowMatrix();
      }
      cov.Multiply(count - 1);

      // Need to test for input/output
      
      EigenvalueDecomposition decomp = cov.EigenvalueDecomposition;
      return new Plane(mean, decomp.EigenVectors.GetColumnVector(0));
    }

  }
}
