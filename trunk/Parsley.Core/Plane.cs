using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MathNet.Numerics.LinearAlgebra;
using Parsley.Core.Extensions;

namespace Parsley.Core {

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
    /// Construct plane from parametric parameters
    /// </summary>
    /// <param name="normal">Normal of plane</param>
    /// <param name="d">Distance from origin</param>
    public Plane(Vector normal, double d) {
      _normal = normal;
      _d = d;
    }

    /// <summary>
    /// Construct plane from extrinsic coordinate system
    /// </summary>
    /// <remarks>Z-vector is used as normal and translation offset is used to calculate D.</remarks>
    /// <param name="ecp"></param>
    public Plane(Emgu.CV.ExtrinsicCameraParameters ecp) {
      Matrix m = ecp.ExtrinsicMatrix.ToParsley();
      _normal =  m.GetColumnVector(2).Normalize();
      _d = -Vector.ScalarProduct(m.GetColumnVector(3), _normal);
    }

    /// <summary>
    /// Construct plane from three points
    /// </summary>
    /// <param name="a">First point</param>
    /// <param name="b">Second point</param>
    /// <param name="c">Third point</param>
    /// <param name="plane">Created plane</param>
    /// <returns>True points are not colinear, false otherwise</returns>
    public static bool FromPoints(Vector a, Vector b, Vector c, out Plane plane) {
      bool not_colinear = false;
      Vector n = Vector.CrossProduct(b - a, c - a);
      double norm = n.Norm();
      if (norm > 0) {
        n.ScaleInplace(1.0 / norm);
        double d = -Vector.ScalarProduct(a, n);
        plane = new Plane(n, d);
        not_colinear = true;
      } else {
        plane = null;
      }
      return not_colinear;
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
    /// <remarks>http://www.lsr.ei.tum.de/fileadmin/publications/K._Klasing/KlasingAlthoff-ComparisonOfSurfaceNormalEstimationMethodsForRangeSensingApplications_ICRA09.pdf</remarks>
    /// <param name="points">Points to fit by plane</param>
    /// <returns>Best-fit plane in terms of orthogonal least square regression.</returns>
    public static bool FitByPCA(IEnumerable<Vector> points, out Plane plane) {
      // Perform orth lin regression by PCA which requires the estimation
      // of the covariance matrix of the samples
      // http://en.wikipedia.org/wiki/Estimation_of_covariance_matrices
      plane = null;
      int count = 0;
      Vector mean = new Vector(3, 0.0);
      foreach (Vector p in points) {
        mean.AddInplace(p); count++;
      }
      if (count < 3)
        return false;
      mean /= (double)count;

      Matrix cov = new Matrix(3, 3, 0.0);
      foreach (Vector p in points) {
        Vector v = p - mean;
        //code below is equivalent to
        //cov += v.ToColumnMatrix() * v.ToRowMatrix();
        //optimized
        double v00 = v[0] * v[0];
        double v01 = v[0] * v[1];
        double v02 = v[0] * v[2];
        double v11 = v[1] * v[1];
        double v12 = v[1] * v[2];
        double v22 = v[2] * v[2];

        cov[0, 0] += v00;
        cov[0, 1] += v01;
        cov[0, 2] += v02;
        cov[1, 0] += v01;
        cov[1, 1] += v11;
        cov[1, 2] += v12;
        cov[2, 0] += v02;
        cov[2, 1] += v12;
        cov[2, 2] += v22;
      }
      // Skip normalization of matrix
      try {
        EigenvalueDecomposition decomp = cov.EigenvalueDecomposition;
        plane = new Plane(mean, decomp.EigenVectors.GetColumnVector(0));
        return true;
      } catch (IndexOutOfRangeException) {
        return false;
      }
      
    }

    /// <summary>
    /// Fit by averaging using newell's method
    /// "Real-Time Collision Detection" page 494
    /// </summary>
    /// <remarks>Faster by a factor of 5 compared to PCA method above</remarks>
    /// <param name="points">Points to fit plane through</param>
    /// <returns>Fitted Plane</returns>
    public static bool FitByAveraging(IEnumerable<Vector> points, out Plane plane) {
      // Assume at least three points
      plane = null;

      IEnumerator<Vector> a = points.GetEnumerator();
      IEnumerator<Vector> b = points.GetEnumerator();

      Vector centroid = new Vector(3, 0.0);
      Vector normal = new Vector(3, 0.0);
      
      b.MoveNext();
      int count = 0;
      while (a.MoveNext()) {
        if (!b.MoveNext()) {
          b = points.GetEnumerator();
          // b.Reset(); Reset is not supported when using yield
          b.MoveNext();
        }
        Vector i = a.Current;
        Vector j = b.Current;
        
        normal[0] += (i[1] - j[1]) * (i[2] + j[2]); // Project on yz
        normal[1] += (i[2] - j[2]) * (i[0] + j[0]); // Project on xz
        normal[2] += (i[0] - j[0]) * (i[1] + j[1]); // Project on xy
        centroid += j;
        count++;
      }
      if (count < 3)
        return false;

      centroid /= (double)count;
      plane = new Plane(centroid, normal.Normalize());
      return true;
    }

  }
}
