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
    Core.ILaserPlaneAlgorithmContext Context {
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
    private Core.ILaserPlaneAlgorithmContext _context;

    public RejectParallelPlanes() { }

    ILaserPlaneAlgorithmContext IPlaneRansacConstraint.Context {
      set { _context = value; }
    }

    public bool Test(IRansacModel model) {
      PlaneModel r = model as PlaneModel;
      bool no_parallel_found = true;
      foreach (Plane p in _context.ReferencePlanes) {
        double d = Vector.ScalarProduct(r.Plane.Normal, p.Normal);
        if ((1.0 - Math.Abs(d)) < 1e-2) {
          no_parallel_found = false;
          break;
        }
      }
      return no_parallel_found;
    }

    #region IPlaneRansacConstraint Members



    #endregion
  }
}
