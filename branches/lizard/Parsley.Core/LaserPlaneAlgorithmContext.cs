/*
 * Parsley http://parsley.googlecode.com
 * Copyright (c) 2010, Christoph Heindl. All rights reserved.
 * Code license:	New BSD License
 */

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
    private System.Drawing.PointF[] _valid_laser_points;

    public Ray[] EyeRays {
      get { return _eye_rays; }
      set { _eye_rays = value; }
    }

    public System.Drawing.PointF[] ValidLaserPoints {
      get { return _valid_laser_points; }
      set { _valid_laser_points = value; }
    }
  }
}
