using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Parsley.Core {

  /// <summary>
  /// Input for algorithms extracting laser planes from points
  /// </summary>
  public interface ILaserPlaneAlgorithmContext : ILaserLineFilterAlgorithmContext {

    /// <summary>
    /// Get eye-rays throuh all valid laser points
    /// </summary>
    Ray[] EyeRays { get; }

    /// <summary>
    /// The set of calibrated reference planes
    /// </summary>
    Plane[] ReferencePlanes { get; }
  }
}
