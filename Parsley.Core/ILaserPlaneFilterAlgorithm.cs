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
  /// Interface for algorithms that filter laser-planes
  /// </summary>
  public interface ILaserPlaneFilterAlgorithm {

    /// <summary>
    /// Filter laser-plane
    /// </summary>
    /// <param name="context"> Dectected laser-plane</param>
    /// <param name="filtered_plane">Possibly modified laser-plane</param>
    /// <returns>True if filter succeeded, false otherwise</returns>
    bool FilterLaserPlane(ILaserPlaneFilterAlgorithmContext context, out Plane filtered_plane);

  }
}
