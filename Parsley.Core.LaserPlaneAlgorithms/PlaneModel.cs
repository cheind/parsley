using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MathNet.Numerics.LinearAlgebra;

namespace Parsley.Core.LaserPlaneAlgorithms {

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
      return Math.Abs(_plane.SignedDistanceTo(x));
    }

    public void Fit(IEnumerable<MathNet.Numerics.LinearAlgebra.Vector> consensus_set) {
      Plane p;
      if (Plane.FitByPCA(consensus_set, out p)) {
        _plane = p;
      }
    }

    /// <summary>
    /// Get the plane
    /// </summary>
    public Plane Plane {
      get { return _plane; }
    } 
  }
}
