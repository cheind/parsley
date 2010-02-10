using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Parsley.Core {

  /// <summary>
  /// Input for laser-plane filter algorithms
  /// </summary>
  public interface ILaserPlaneFilterAlgorithmContext {

    /// <summary>
    /// Get the detected laser-plane
    /// </summary>
    Core.Plane LaserPlane {
      get;
    }
  }
}
