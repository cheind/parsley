/*
 * Parsley http://parsley.googlecode.com
 * Copyright (c) 2010, Christoph Heindl. All rights reserved.
 * Code license:	New BSD License
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Drawing;

using Parsley.Core.Extensions;
using Emgu.CV.Structure;
using MathNet.Numerics.LinearAlgebra;

namespace Parsley.Core.BuildingBlocks {
  
  /// <summary>
  /// Defines how scanning is performed
  /// </summary>
  [Serializable]
  public class ScanWorkflow : IDeserializationCallback {
    private System.Drawing.Rectangle _roi;
    private Core.ILaserLineAlgorithm _line_algorithm;
    private Core.ILaserPlaneAlgorithm _plane_algorithm;
    private Core.ILaserLineFilterAlgorithm _line_filter;
    private Core.IPointPerPixelAccumulator _point_accum;
    private Core.ILaserPlaneFilterAlgorithm _plane_filter;
    private double _minimumDistanceToReferencePlane;

    public ScanWorkflow() {
      _line_algorithm = new LaserLineAlgorithms.WeightedAverage(220);
      _line_filter = new LaserLineAlgorithms.NoFilter();
      _plane_algorithm = new LaserPlaneAlgorithms.PlaneRansac();
      _plane_filter = new LaserPlaneAlgorithms.FilterByCameraPlaneAngle();
      _point_accum = new MedianPointAccumulator();
      _minimumDistanceToReferencePlane = -10;
    }

    public void OnDeserialization(object sender) {
      _point_accum.Size = _roi.Size;
    }

    public Rectangle ROI{
      get { return _roi; }
      set { 
        _roi = value;
        _point_accum.Size = value.Size;
      }
    }

    /// <summary>
    /// Get/set the algorithm that performs laser line extraction
    /// </summary>
    [TypeConverter(typeof(Core.Addins.ReflectionTypeConverter))]
    [RefreshProperties(RefreshProperties.All)]
    public ILaserLineAlgorithm LaserLineAlgorithm {
      get { return _line_algorithm; }
      set { _line_algorithm = value; }
    }

    /// <summary>
    /// Used instead of DefaultValueAttribute
    /// </summary>
    public bool ShouldSerializeLaserLineAlgorithm() {
      return _line_algorithm.GetType() != typeof(LaserLineAlgorithms.WeightedAverage);
    }

    /// <summary>
    /// Get/set the algorithm that performs laser line extraction
    /// </summary>
    [TypeConverter(typeof(Core.Addins.ReflectionTypeConverter))]
    [RefreshProperties(RefreshProperties.All)]
    public ILaserLineFilterAlgorithm LaserLineFilterAlgorithm {
      get { return _line_filter; }
      set { _line_filter = value; }
    }

    /// <summary>
    /// Used instead of DefaultValueAttribute
    /// </summary>
    public bool ShouldSerializeLaserLineFilterAlgorithm() {
      return _line_filter.GetType() != typeof(LaserLineAlgorithms.NoFilter);
    }

    /// <summary>
    /// Get/set the algorithm that performs laser plane extraction
    /// </summary>
    [TypeConverter(typeof(Core.Addins.ReflectionTypeConverter))]
    [RefreshProperties(RefreshProperties.All)]
    public ILaserPlaneAlgorithm LaserPlaneAlgorithm {
      get { return _plane_algorithm; }
      set { _plane_algorithm = value; }
    }


    /// <summary>
    /// Used instead of DefaultValueAttribute
    /// </summary>
    public bool ShouldSerializeLaserPlaneAlgorithm() {
      return _plane_algorithm.GetType() != typeof(LaserPlaneAlgorithms.PlaneRansac);
    }

    /// <summary>
    /// Get/set the algorithm accumulate points
    /// </summary>
    [TypeConverter(typeof(Core.Addins.ReflectionTypeConverter))]
    [RefreshProperties(RefreshProperties.All)]
    public ILaserPlaneFilterAlgorithm LaserPlaneFilterAlgorithm {
      get { return _plane_filter; }
      set { _plane_filter = value; }
    }

    /// <summary>
    /// Used instead of DefaultValueAttribute
    /// </summary>
    public bool ShouldSerializeLaserPlaneFilterAlgorithm() {
      return _plane_filter.GetType() != typeof(LaserPlaneAlgorithms.FilterByCameraPlaneAngle);
    }

    /// <summary>
    /// Get/set the algorithm accumulate points
    /// </summary>
    [TypeConverter(typeof(Core.Addins.ReflectionTypeConverter))]
    [RefreshProperties(RefreshProperties.All)]
    public IPointPerPixelAccumulator PointAccumulator {
      get { return _point_accum; }
      set { _point_accum = value; }
    }

    /// <summary>
    /// Used instead of DefaultValueAttribute
    /// </summary>
    public bool ShouldSerializePointAccumulator() {
      return _point_accum.GetType() != typeof(MedianPointAccumulator);
    }

    /// <summary>
    /// Set minimum distance from detected points to the reference planes 
    /// </summary>
    [Description("Set minimum distance from detected points to the reference planes")]
    public double MinimumDistanceToReferencePlane
    {
      get { return _minimumDistanceToReferencePlane; }
      set { _minimumDistanceToReferencePlane = value; }
    }

    /// <summary>
    /// Reset workflow
    /// </summary>
    public void Reset() {
      _point_accum.Reset();
    }

    /// <summary>
    /// Process image
    /// </summary>
    /// <param name="s">Setup</param>
    /// <param name="image">Image</param>
    /// <param name="points">Found points</param>
    /// <param name="pixels">Corresponding pixels for each point</param>
    /// <returns>True if successful, false otherwise</returns>
    public bool Process(
      Setup s, Emgu.CV.Image<Bgr, byte> image, 
      out List<Vector> points, out List<System.Drawing.Point> pixels)
    {
      pixels = null;
      points = null;
      // 1. Update values needed by algorithms

      Bundle b = new Bundle();
      BundleBookmarks bb = new BundleBookmarks(b);

      bb.ROI = _roi;
      bb.ReferencePlanes = s.ReferenceBody.Planes;
      
      if (s.Positioner.PositionerPose != null)
        bb.ReferencePlanes.Add(new Plane(s.Positioner.PositionerPose));
      
      bb.Image = image;
      bb.LaserColor = s.Laser.Color;
            
      // 2. Extract laser line
      if (!_line_algorithm.FindLaserLine(bb.Bundle)) return false;
      // 3. Filter laser points
      if (!_line_filter.FilterLaserLine(bb.Bundle)) return false;
      if (bb.LaserPixel.Count < 3) return false;

      // 4. Detect laser plane
      List<Ray> eye_rays = new List<Ray>(Core.Ray.EyeRays(s.Camera.Intrinsics, bb.LaserPixel.ToArray()));
      bb.EyeRays = eye_rays;
      if (!_plane_algorithm.FindLaserPlane(bb.Bundle)) return false;

      // 5. Filter laser plane
      if (!_plane_filter.FilterLaserPlane(bb.Bundle)) return false;

      // 6. Extract relevant points in ROI
      pixels = new List<System.Drawing.Point>();
      points = new List<Vector>();

      List<System.Drawing.PointF> laser_pixel = bb.LaserPixel;
      Plane laser_plane = bb.LaserPlane;
      List<Plane> reference_planes = bb.ReferencePlanes;
      for (int i = 0; i < laser_pixel.Count; ++i) {
        // Round to nearest pixel
        System.Drawing.Point p = laser_pixel[i].ToNearestPoint();

        double t;
        if (_roi.Contains(p)) {
          Core.Ray r = eye_rays[i];
          if (Core.Intersection.RayPlane(r, laser_plane, out t)) {

            if (this.PointInsideOfReferenceArea(reference_planes, r, t))
            {
              System.Drawing.Point in_roi = Core.IndexHelper.MakeRelative(p, _roi);
              pixels.Add(p);
              _point_accum.Accumulate(in_roi, r, t);
              points.Add(_point_accum.Extract(in_roi));
            }
          }
        }
      }

      s.Positioner.TransformPoints(points);
      return points.Count > 0;
    }

    private bool PointInsideOfReferenceArea(List<Plane> referencePlanes, Core.Ray r, double t)
    {
      Vector my3DPoint = r.At(t);

      for (int i = 0; i < referencePlanes.Count; i++)
      {
        if (referencePlanes[i].SignedDistanceTo(my3DPoint) >= _minimumDistanceToReferencePlane)
          return false;
      }

      return true;
    }
  }
}
