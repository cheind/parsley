using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Parsley.Core {

  public class LaserPlaneFilterAlgorithmContext : LaserPlaneAlgorithmContext, ILaserPlaneFilterAlgorithmContext {
    Core.Plane _laser_plane;  
    
    public Plane LaserPlane {
      get { return _laser_plane; }
      set { _laser_plane = value; }
    }
  }
}
