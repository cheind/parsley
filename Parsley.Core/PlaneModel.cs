using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MathNet.Numerics.LinearAlgebra;

namespace Parsley.Core {

  /// <summary>
  /// Plane model to be used in conjunction with Ransac
  /// </summary>
  public class PlaneModel : IRansacModel {
    private Plane _plane;

    public int RequiredSamples {
      get { return 3; }
    }

    public bool Build(IEnumerable<MathNet.Numerics.LinearAlgebra.Vector> initial) {
      IEnumerator<Vector> e = initial.GetEnumerator(); e.MoveNext();
      Vector a = e.Current; e.MoveNext();
      Vector b = e.Current; e.MoveNext();
      Vector c = e.Current;

      return Plane.FromPoints(a, b, c, out _plane);
    }

    public double DistanceTo(MathNet.Numerics.LinearAlgebra.Vector x) {
      return Math.Abs(_plane.DistanceTo(x));
    }

    public void Fit(IEnumerable<MathNet.Numerics.LinearAlgebra.Vector> consensus_set) {
      _plane = Plane.FitByAveraging(consensus_set);
    }

    /// <summary>
    /// Get the plane
    /// </summary>
    public Plane Plane {
      get { return _plane; }
    } 
  }

  public class NotParallelPlaneConstraint : IRansacModelConstraint {
    private Plane[] _planes;

    public NotParallelPlaneConstraint(Plane[] planes) {
      _planes = planes;
    }


    public bool Test(IRansacModel model) {
      PlaneModel r = model as PlaneModel;
      bool no_parallel_found = true;
      foreach (Plane p in _planes) {
        double d = Vector.ScalarProduct(r.Plane.Normal, p.Normal);
        if ((1.0 - Math.Abs(d)) < 1e-5) {
          no_parallel_found = false;
          break;
        }
      }
      return no_parallel_found;
    }
  }
}
