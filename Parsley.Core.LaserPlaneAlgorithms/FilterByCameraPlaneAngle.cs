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
using System.ComponentModel;

namespace Parsley.Core.LaserPlaneAlgorithms {
  /// <summary>
  /// Filter laser-plane if angle between laser-plane and camera view direction is too small.
  /// </summary>
  [Serializable]
  [Core.Addins.Addin]
  public class FilterByCameraPlaneAngle : Core.ILaserPlaneFilterAlgorithm {
    private double _angle_proj_cos;
    private double _angle_deg;
    private Vector _camera_view_direction;

    public FilterByCameraPlaneAngle() {
      this.MinimumAngle = 30;
      _camera_view_direction = new Vector(new double[] { 0, 0, 1 });
    }

    /// <summary>
    /// Minimum angle between laser-plane in degrees
    /// </summary>
    [Description("Minimum angle between camera and laser-plane in degrees")]
    public double MinimumAngle {
      get { return _angle_deg; }
      set {
        _angle_deg =  MathHelper.Clamp(value, 0.0, 90.0);
        _angle_proj_cos = Math.Cos(((90.0 - _angle_deg) / 180.0) * Math.PI);
      }
    }


    /// <summary>
    /// Filter laser-plane if angle between laser-plane and camera view direction is too small.
    /// </summary>
    /// <param name="context">Context</param>
    /// <param name="filtered_plane">Laser-plane as given in context</param>
    /// <returns>True if angle between camera and laser is greater-equal to minimum angle specified, false otherwise</returns>
    public bool FilterLaserPlane(ILaserPlaneFilterAlgorithmContext context, out Plane filtered_plane) {
      filtered_plane = context.LaserPlane;
      return Math.Abs(Vector.ScalarProduct(_camera_view_direction, filtered_plane.Normal)) >= _angle_proj_cos;
    }

  }
}
