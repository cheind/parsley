using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Parsley.Core {

  /// <summary>
  /// Context for laser line filtering algorithms
  /// </summary>
  public interface ILaserLineFilterAlgorithmContext : ILaserLineAlgorithmContext {
    /// <summary>
    /// Get the laser points
    /// </summary>
    System.Drawing.PointF[] LaserPoints { get; }
  }
}
