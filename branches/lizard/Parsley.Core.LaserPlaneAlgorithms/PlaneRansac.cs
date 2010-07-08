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
    public bool FindLaserPlane(ILaserPlaneAlgorithmContext context, out Plane plane) {
      Ray[] rays;
      if (_only_out_of_roi) {
        List<Core.Ray> outside_rays = new List<Ray>();
        for (int i = 0; i < context.ValidLaserPoints.Length; ++i) {
          if (!context.ROI.Contains(context.ValidLaserPoints[i])) {
            outside_rays.Add(context.EyeRays[i]);
          }
        }
        rays = outside_rays.ToArray();
      } else {
        rays = context.EyeRays;
      }

      if (rays.Length == 0) {
        plane = null;
        return false;
      }

      Vector[] isect;
      double[] ts;
      int[] plane_ids;

      Core.Intersection.FindEyeRayPlaneIntersections(rays, context.ReferencePlanes, out ts, out isect, out plane_ids);

      Ransac<PlaneModel> ransac = new Ransac<PlaneModel>(isect);
      int min_consensus = (int)Math.Max(rays.Length * _min_consensus_precent, context.Image.Width * 0.05);
      _constraint.Context = context;
      Ransac<PlaneModel>.Hypothesis h = ransac.Run(_max_iterations, _plane_accurracy, min_consensus, _constraint);

      // Make sure that at least consensus set was formed from at least two different planes


      if (h != null/* && h.ConsensusIds.Select(element => plane_ids[element]).Distinct().Count() > 1*/) {
        plane = h.Model.Plane;
        return true;
      } else {
        plane = null;
        return false;
      }
    }
  }
}
