using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Parsley.Core {

  /// <summary>
  /// Stores laser-plane algorithm inputs
  /// </summary>
  public class LaserPlaneAlgorithmContext : LaserLineFilterAlgorithmContext, ILaserPlaneAlgorithmContext {
    private Ray[] _eye_rays;
    private Plane[] _reference_planes;

    public Ray[] EyeRays {
      get { return _eye_rays; }
      set { _eye_rays = value; }
    }

    public Plane[] ReferencePlanes {
      get { return _reference_planes; }
      set { _reference_planes = value; }
    }
  }
}
