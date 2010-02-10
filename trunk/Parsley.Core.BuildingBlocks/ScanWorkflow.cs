using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

using Emgu.CV.Structure;
using MathNet.Numerics.LinearAlgebra;
using System.Runtime.Serialization;

namespace Parsley.Core.BuildingBlocks {
  
  /// <summary>
  /// Defines how scanning is performed
  /// </summary>
  [Serializable]
  public class ScanWorkflow : IDeserializationCallback {
    [NonSerialized]
    private World _world;
    private System.Drawing.Rectangle _roi;
    private Core.ILaserLineAlgorithm _line_algorithm;
    private Core.ILaserPlaneAlgorithm _plane_algorithm;
    private Core.ILaserLineFilterAlgorithm _line_filter;
    private Core.IPointPerPixelAccumulator _point_accum;
    private Core.ILaserPlaneFilterAlgorithm _plane_filter;
    [NonSerialized]
    private Core.LaserPlaneFilterAlgorithmContext _context;

    public ScanWorkflow() {
      _line_algorithm = new LaserLineAlgorithms.WeightedAverage(220);
      _line_filter = new LaserLineAlgorithms.NoFilter();
      _plane_algorithm = new LaserPlaneAlgorithms.PlaneRansac();
      _plane_filter = new LaserPlaneAlgorithms.FilterByCameraPlaneAngle();
      _point_accum = new MedianPointAccumulator();
      _context = new LaserPlaneFilterAlgorithmContext();
    }

    public void OnDeserialization(object sender) {
      _context = new LaserPlaneFilterAlgorithmContext();
      _point_accum.Size = _roi.Size;
    }

    /// <summary>
    /// Get/Set the world reference
    /// </summary>
    [Browsable(false)]
    public World World {
      set { 
        _world = value;
        _context.Intrinsics = World.Camera.Intrinsics;
        _context.ReferencePlanes = World.ReferencePlanes.ToArray();
      }
      get { return _world; }
    }

    /// <summary>
    /// Access the scanning region of interest
    /// </summary>
    public System.Drawing.Rectangle ROI {
      get { return _roi; }
      set { 
        _roi = value;
        _point_accum.Size = _roi.Size;
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
    /// Get/set the algorithm that performs laser line extraction
    /// </summary>
    [TypeConverter(typeof(Core.Addins.ReflectionTypeConverter))]
    [RefreshProperties(RefreshProperties.All)]
    public ILaserLineFilterAlgorithm LaserLineFilterAlgorithm {
      get { return _line_filter; }
      set { _line_filter = value; }
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
    /// Get/set the algorithm accumulate points
    /// </summary>
    [TypeConverter(typeof(Core.Addins.ReflectionTypeConverter))]
    [RefreshProperties(RefreshProperties.All)]
    public ILaserPlaneFilterAlgorithm LaserPlaneFilterAlgorithm {
      get { return _plane_filter; }
      set { _plane_filter = value; }
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
    /// Process image
    /// </summary>
    /// <param name="image">Image</param>
    /// <param name="points">Found points</param>
    /// <param name="pixels">Corresponding pixels for each point</param>
    /// <returns>True if successful, false otherwise</returns>
    public bool Process(Emgu.CV.Image<Bgr, byte> image, out Vector[] points, out System.Drawing.Point[] pixels) {
      points = null;
      pixels = null;


      // 1. Update context
      _context.Image = image;
      _context.ChannelImage = image[(int)World.Laser.Color];
      
      // 2. Extract laser line
      System.Drawing.PointF[] laser_points;
      if (!_line_algorithm.FindLaserLine(_context, out laser_points)) return false;
      _context.LaserPoints = laser_points;

      // 3. Filter laser points
      System.Drawing.PointF[] filtered_laser_points;
      if (!_line_filter.FilterLaserLine(_context, out filtered_laser_points)) return false;
      _context.ValidLaserPoints = filtered_laser_points.Where(p => p != System.Drawing.PointF.Empty).ToArray();
      if (_context.ValidLaserPoints.Length < 3) return false;

      // 4. Detect laser plane
      _context.EyeRays = Core.Ray.EyeRays(_context.Intrinsics, _context.ValidLaserPoints);

      Core.Plane laser_plane;
      if (!_plane_algorithm.FindLaserPlane(_context, out laser_plane)) return false;
      _context.LaserPlane = laser_plane;

      // 5. Filter laser plane
      Core.Plane filtered_laser_plane;
      if (!_plane_filter.FilterLaserPlane(_context, out filtered_laser_plane)) return false;

      // 6. Extract relevant points in ROI
      List<System.Drawing.Point> my_pixels = new List<System.Drawing.Point>();
      List<Vector> my_points = new List<Vector>();
      
      for (int i = 0; i < _context.ValidLaserPoints.Length; ++i) {
        // Round to nearest pixel
        System.Drawing.Point p = new System.Drawing.Point(
          (int)_context.ValidLaserPoints[i].X,
          (int)_context.ValidLaserPoints[i].Y);

        double t;
        if (_roi.Contains(p)) {
          Core.Ray r = _context.EyeRays[i];
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
