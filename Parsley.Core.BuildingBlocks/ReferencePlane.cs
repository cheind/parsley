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
using Parsley.Core.Extensions;

namespace Parsley.Core.BuildingBlocks {
  
  public class ReferencePlane {
    private Plane _plane;
    private Emgu.CV.ExtrinsicCameraParameters _ecp;
    private Vector[] _deviation_points;
    private double[] _deviations;

    /// <summary>
    /// Construct reference plane from extrinsic calibration
    /// </summary>
    /// <param name="ecp">Extrinsic calibration</param>
    public ReferencePlane(Emgu.CV.ExtrinsicCameraParameters ecp, Vector[] deviation_points, double[] deviations) {
      _ecp = ecp;
      _plane = new Plane(ecp);
      _deviation_points = deviation_points;
      _deviations = deviations;
    }

    /// <summary>
    /// Get the plane equation
    /// </summary>
    public Plane Plane {
      get { return _plane; }
    }

    public Emgu.CV.ExtrinsicCameraParameters Extrinsic {
      get { return _ecp; }
    }

    public Vector[] DeviationPoints {
      get { return _deviation_points; }
    }

    public double[] Deviations {
      get { return _deviations; }

    }
  }
}
