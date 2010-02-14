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
  public class LaserLineAlgorithmContext : AlgorithmContext, ILaserLineAlgorithmContext {
    private ColorChannel _cc;
    
    public LaserLineAlgorithmContext() { }

    /// <summary>
    /// Access the laser color
    /// </summary>
    public ColorChannel LaserColor {
      get { return _cc; }
      set { _cc = value; }
    }

    
  }
}
