using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

using Emgu.CV.Structure;
using MathNet.Numerics.LinearAlgebra;

namespace Parsley.Core.BuildingBlocks {
  
  /// <summary>
  /// Defines how scanning is performed
  /// </summary>
  [Serializable]
  public class ScanWorkflow {
    private World _world;
    private System.Drawing.Rectangle _roi;
    private Core.ILaserLineAlgorithm _line_algorithm;
    private Core.ILaserPlaneAlgorithm _plane_algorithm;
    private Core.IPointPerPixelAccumulator _point_accum;
    private Core.DensePixelGrid<VisualizationInfo> _vis_info_grid;
    private Emgu.CV.Image<Bgr, byte> _reference_image;

    public ScanWorkflow() {
      _line_algorithm = new LaserLineAlgorithms.WeightedAverage(220);
      _plane_algorithm = new LaserPlaneAlgorithms.PlaneRansac();
      _point_accum = new MedianPointAccumulator();
      _vis_info_grid = new DensePixelGrid<VisualizationInfo>();
    }

    /// <summary>
    /// Get/Set the world reference
    /// </summary>
    [Browsable(false)]
    public World World {
      set { _world = value; }
      get { return _world; }
    }

    /// <summary>
    /// Access the scanning region of interest
    /// </summary>
    public System.Drawing.Rectangle ROI {
      set { 
        _roi = value;
        _point_accum.Size = _roi.Size;
        _vis_info_grid.Size = _roi.Size;
      }
    }

    /// <summary>
    /// Set the reference image used for texturing points.
    /// </summary>
    public Emgu.CV.Image<Bgr, byte> ReferenceImage {
      set { _reference_image = value; }
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
      Core.LaserPlaneAlgorithmContext c = new LaserPlaneAlgorithmContext();
      c.Intrinsics = World.Camera.Intrinsics;
      c.Image = image;
      c.ChannelImage = image[(int)World.Laser.Color];

      // 1. Extract laser line
      System.Drawing.PointF[] laser_points;
      if (!_line_algorithm.FindLaserLine(c, out laser_points)) {
        return false;
      } else {
        c.LaserPoints = laser_points;
      }

      // Only valids?

      // 2. Filter laser line ?



    }

    class VisualizationInfo {
      private Vector _color;
      private uint _id;

      public VisualizationInfo(Vector color, uint id) {
        _color = color;
        _id = id;
      }

      public Vector Color {
        get { return _color; }
      }

      public uint Id {
        get { return _id; }
      }

    };


  }
}
