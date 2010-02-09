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
    IEnumerable<Plane> ReferencePlanes {
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
    private IEnumerable<Plane> _planes;

    public RejectParallelPlanes() { }

    public RejectParallelPlanes(IEnumerable<Plane> planes) {
      _planes = planes;
    }

    public IEnumerable<Plane> ReferencePlanes {
      set { _planes = value; }
    }

    public bool Test(IRansacModel model) {
      PlaneModel r = model as PlaneModel;
      bool no_parallel_found = true;
      foreach (Plane p in _planes) {
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
