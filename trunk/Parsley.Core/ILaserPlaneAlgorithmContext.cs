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
    /// Get all valid laser-points
    /// </summary>
    System.Drawing.PointF[] ValidLaserPoints {
      get;
    }

    /// <summary>
    /// Get eye-rays throuh all valid laser points
    /// </summary>
    Ray[] EyeRays { get; }
  }
}
