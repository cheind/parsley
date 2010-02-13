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
  public class LaserLineFilterAlgorithmContext : LaserLineAlgorithmContext, ILaserLineFilterAlgorithmContext {
    private System.Drawing.PointF[] _laser_points;

    /// <summary>
    /// Default constructor
    /// </summary>
    public LaserLineFilterAlgorithmContext() { }

    /// <summary>
    /// Get all laser points
    /// </summary>
    public System.Drawing.PointF[] LaserPoints {
      get { return _laser_points; }
      set { _laser_points = value; }
    }
  }
}
