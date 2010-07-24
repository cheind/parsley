/*
 * Parsley http://parsley.googlecode.com
 * Copyright (c) 2010, Christoph Heindl. All rights reserved.
 * Code license:	New BSD License
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

using MathNet.Numerics.LinearAlgebra;

namespace Parsley.Core {

  /// <summary>
  /// Interface for algorithms extracting the laser-plane from laser-points.
  /// </summary>
  public interface ILaserPlaneAlgorithm {

    /// <summary>
    /// Find laser-plane through detected laser-points.
    /// </summary>
    /// <remarks>Method is only invoked when at least 3 laser_points have been detected.</remarks>
    /// <param name="values">Knowledge about the current frame encoded as string/object pairs</param>
    /// <returns>True on success, false otherwise</returns>
    bool FindLaserPlane(Dictionary<string, object> values);

  }
}
