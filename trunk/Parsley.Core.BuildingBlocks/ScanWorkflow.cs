using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

using Emgu.CV.Structure;
using MathNet.Numerics.LinearAlgebra;
using System.Runtime.Serialization;
using System.Drawing;

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

    public ScanWorkflow() {
      _line_algorithm = new LaserLineAlgorithms.WeightedAverage(220);
      _line_filter = new LaserLineAlgorithms.NoFilter();
      _plane_algorithm = new LaserPlaneAlgorithms.PlaneRansac();
      _plane_filter = new LaserPlaneAlgorithms.FilterByCameraPlaneAngle();
      _point_accum = new MedianPointAccumulator();
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
    public bool Process(Setup s, Emgu.CV.Image<Bgr, byte> image, out Vector[] points, out System.Drawing.Point[] pixels) {
      points = null;
      pixels = null;

      Core.LaserPlaneFilterAlgorithmContext context = new LaserPlaneFilterAlgorithmContext();
      context.Intrinsics = s.Camera.Intrinsics;
      context.ROI = _roi;
      context.ReferencePlanes = s.ReferencePlanes.ToArray();


      // 1. Update context
      context.Image = image;
      context.ChannelImage = image[(int)s.Laser.Color];
      
      // 2. Extract laser line
      System.Drawing.PointF[] laser_points;
      if (!_line_algorithm.FindLaserLine(context, out laser_points)) return false;
      context.LaserPoints = laser_points;

      // 3. Filter laser points
      System.Drawing.PointF[] filtered_laser_points;
      if (!_line_filter.FilterLaserLine(context, out filtered_laser_points)) return false;
      context.ValidLaserPoints = filtered_laser_points.Where(p => p != System.Drawing.PointF.Empty).ToArray();
      if (context.ValidLaserPoints.Length < 3) return false;

      // 4. Detect laser plane
      context.EyeRays = Core.Ray.EyeRays(context.Intrinsics, context.ValidLaserPoints);

      Core.Plane laser_plane;
      if (!_plane_algorithm.FindLaserPlane(context, out laser_plane)) return false;
      context.LaserPlane = laser_plane;

      // 5. Filter laser plane
      Core.Plane filtered_laser_plane;
      if (!_plane_filter.FilterLaserPlane(context, out filtered_laser_plane)) {
        Console.WriteLine("filtered" + filtered_laser_plane.Normal);
        return false;
      }

      // 6. Extract relevant points in ROI
      List<System.Drawing.Point> my_pixels = new List<System.Drawing.Point>();
      List<Vector> my_points = new List<Vector>();

      for (int i = 0; i < context.ValidLaserPoints.Length; ++i) {
        // Round to nearest pixel
        System.Drawing.Point p = new System.Drawing.Point(
          (int)context.ValidLaserPoints[i].X,
          (int)context.ValidLaserPoints[i].Y);

        double t;
        if (_roi.Contains(p)) {
          Core.Ray r = context.EyeRays[i];
          if (Core.Intersection.RayPlane(r, filtered_laser_plane, out t)) {
            System.Drawing.Point in_roi = Core.IndexHelper.MakeRelative(p, _roi);
            my_pixels.Add(p);
            _point_accum.Accumulate(in_roi, r, t);
            my_points.Add(_point_accum.Extract(in_roi));
          }
        }
      }

      points = my_points.ToArray();
      pixels = my_pixels.ToArray();
      return points.Length > 0;
    }
  }
}
