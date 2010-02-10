using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

using MathNet.Numerics.LinearAlgebra;

namespace Parsley.Core {

  /// <summary>
  /// Interface for algorithms extracting the laser-plane from laser-points.
  /// </summary>
  public abstract class ILaserPlaneAlgorithm {

    /// <summary>
    /// Find laser-plane through detected laser-points.
    /// </summary>
    /// <remarks>Method is only invoked when at least 3 laser_points have been detected.</remarks>
    /// <param name="context">Input context</param>
    /// <returns>True on success, false otherwise</returns>
    public abstract bool FindLaserPlane(ILaserPlaneAlgorithmContext context, out Plane plane);

  }
}
