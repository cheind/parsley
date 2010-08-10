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
using Parsley.Core.Extensions;

namespace Parsley.Core.LaserPlaneAlgorithms {

  [Serializable]
  [Parsley.Core.Addins.Addin]
  public class PlaneRansac : Core.ILaserPlaneAlgorithm {
    private bool _only_out_of_roi;
    private double _min_consensus_precent;
    private double _plane_accurracy;
    private int _max_iterations;
    private IPlaneRansacConstraint _constraint;

    public PlaneRansac(double min_consensus, double plane_accurracy, int max_iter, IPlaneRansacConstraint c) {
      MinimumConsensus = min_consensus;
      Accurracy = plane_accurracy;
      Iterations = max_iter;
      Constraint = c;
    }

    /// <summary>
    /// Construct algorithm with default arguments
    /// </summary>
    public PlaneRansac() : this(0.5, 0.2, 30, new RejectParallelPlanes()) {
    }

    /// <summary>
    /// Get/Set the minimum number consensus points in percent.
    /// </summary>
    public double MinimumConsensus {
      get { return _min_consensus_precent; }
      set { _min_consensus_precent = MathHelper.Clamp(value, 0, 1); }
    }

    /// <summary>
    /// True if points only outside of ROI are used for plane estimation.
    /// </summary>
    public bool OnlyOutOfROI {
      get { return _only_out_of_roi; }
      set { _only_out_of_roi = value; }
    }

    /// <summary>
    /// Defines required accurracy (distance to plane) of inliers in world units.
    /// </summary>
    public double Accurracy {
      get { return _plane_accurracy; }
      set { _plane_accurracy = value; }
    }

    /// <summary>
    /// Maximum number of iterations to perform to find a plane.
    /// </summary>
    public int Iterations {
      get { return _max_iterations; }
      set { _max_iterations = Math.Max(1, value); }
    }

    /// <summary>
    /// Get/set additional constraints on the plane found by RANSAC.
    /// </summary>
    [TypeConverter(typeof(Addins.ReflectionTypeConverter))]
    [RefreshProperties(RefreshProperties.All)]
    public IPlaneRansacConstraint Constraint {
      get { return _constraint; }
      set { _constraint = value; }
    }

    /// <summary>
    /// Find laser-plane through RANSAC
    /// </summary>
    /// <param name="context">Context</param>
    /// <param name="plane">Found plane</param>
    /// <returns>Success</returns>
    public bool FindLaserPlane(Bundle bundle) {
      BundleBookmarks b = new BundleBookmarks(bundle);

      List<System.Drawing.PointF> laser_pixels = b.LaserPixel;
      List<Ray> eye_rays = b.EyeRays;
      System.Drawing.Rectangle roi = b.ROI;

      List<Ray> rays;
      if (_only_out_of_roi) {
        rays = new List<Ray>();
        for (int i = 0; i < laser_pixels.Count; ++i) {
          if (!roi.Contains(laser_pixels[i])) {
            rays.Add(eye_rays[i]);
          }
        }
      } else {
        rays = eye_rays;
      }

      if (rays.Count == 0) {
        return false;
      }

      Vector[] isect;
      double[] ts;
      int[] plane_ids;

      IList<Plane> reference_planes = b.ReferencePlanes;
      Core.Intersection.FindEyeRayPlaneIntersections(
        rays.ToArray(), 
        reference_planes.ToArray(), 
        out ts, out isect, out plane_ids);

      Ransac<PlaneModel> ransac = new Ransac<PlaneModel>(isect);
      int min_consensus = (int)Math.Max(rays.Count * _min_consensus_precent, b.Image.Width * 0.05);
      _constraint.Bundle = bundle;
      Ransac<PlaneModel>.Hypothesis h = ransac.Run(_max_iterations, _plane_accurracy, min_consensus, _constraint);

      if (h != null) {
        b.LaserPlane = h.Model.Plane;
        return true;
      } else {
        return false;
      }
    }
  }
}
