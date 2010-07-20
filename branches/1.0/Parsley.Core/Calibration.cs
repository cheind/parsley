/*
 * Parsley http://parsley.googlecode.com
 * Copyright (c) 2010, Christoph Heindl. All rights reserved.
 * Code license:	New BSD License
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MathNet.Numerics.LinearAlgebra;

namespace Parsley.Core {

  /// <summary>
  /// Base class for intrinsic/extrinsic calibration
  /// </summary>
  public class CalibrationBase {
    private Vector[] _object_points;

    /// <summary>
    /// Initialize with reference points on object in 3D
    /// </summary>
    /// <param name="object_points">object points</param>
    public CalibrationBase(Vector[] object_points) {
      _object_points = object_points;
    }

    /// <summary>
    /// Access the reference object points
    /// </summary>
    public Vector[] ObjectPoints {
      get { return _object_points; }
    }
  }
}
