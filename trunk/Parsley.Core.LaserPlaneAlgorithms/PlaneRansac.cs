using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MathNet.Numerics.LinearAlgebra;
using System.ComponentModel;

namespace Parsley.Core.LaserPlaneAlgorithms {

  [Serializable]
  [Parsley.Core.Addins.Addin]
  public class PlaneRansac : Core.ILaserPlaneAlgorithm {
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
    /// Find laser-plane using RANSAC
    /// </summary>
    /// <param name="icp"></param>
    /// <param name="laser_points"></param>
    /// <param name="reference_planes"></param>
    /// <param name="plane"></param>
    /// <returns></returns>
    public bool FindLaserPlane(ILaserPlaneAlgorithmContext context, out Plane plane) {
      Vector[] isect = new Vector[context.EyeRays.Length];
      double[] ts = new double[context.EyeRays.Length];

      Core.Intersection.FindEyeRayPlaneIntersections(context.EyeRays, context.ReferencePlanes, ref ts, ref isect);
      
      Ransac<PlaneModel> ransac = new Ransac<PlaneModel>(isect);
      int min_consensus = (int)Math.Max(context.EyeRays.Length * _min_consensus_precent, context.Image.Width * 0.1);
      _constraint.Context = context;
      Ransac<PlaneModel>.Hypothesis h = ransac.Run(_max_iterations, _plane_accurracy, min_consensus, _constraint);

      if (h != null) {
        plane = h.Model.Plane;
        return true;
      } else {
        plane = null;
        return false;
      }
    }
  }
}
