using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Parsley.Core.BuildingBlocks {

  /// <summary>
  /// Represents the world as seen by Parsley.
  /// </summary>
  [Serializable]
  public class World {

    private BuildingBlocks.Camera _camera;
    private BuildingBlocks.Laser _laser;
    private Core.CalibrationPattern _intrinsic_pattern;
    private Core.CalibrationPattern _extrinsic_pattern;
    private List<Core.Plane> _reference_planes;
    private List<Emgu.CV.ExtrinsicCameraParameters> _extrinsics;

    /// <summary>
    /// Default constructor of entities
    /// </summary>
    public World() {
      _camera = new Camera(0);
      _laser = new Laser();
      _intrinsic_pattern = new Core.CheckerBoard(9, 6, 25.0f);
      _extrinsic_pattern = new Core.CheckerBoard(9, 6, 10.0f);
      _reference_planes = new List<Plane>();
      _extrinsics = new List<Emgu.CV.ExtrinsicCameraParameters>();
    }

    /// <summary>
    /// Get/set the camera
    /// </summary>
    [Description("Adjust camera settings")]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public Camera Camera {
      get { return _camera; }
      set { _camera = value; }
    }

    /// <summary>
    /// Get/set laser and laser extraction algorithms
    /// </summary>
    [Description("Adjust laser settings")]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public Laser Laser {
      get { return _laser; }
      set { _laser = value; }
    }

    /// <summary>
    /// Get the recorded reference planes
    /// </summary>
    [Browsable(false)]
    public List<Plane> ReferencePlanes {
      get { return _reference_planes; }
    }

    /// <summary>
    /// Get the recorded reference planes
    /// </summary>
    [Browsable(false)]
    public List<Emgu.CV.ExtrinsicCameraParameters> Extrinsics {
      get { return _extrinsics; }
    }

    /// <summary>
    /// Get/set the calibration pattern for the intrinsic camera calibration
    /// </summary>
    [Description("Choose the calibration pattern for the intrinsic camera calibration")]
    [TypeConverter(typeof(Core.ReflectionTypeConverter))]
    [RefreshProperties(RefreshProperties.All)]
    public Core.CalibrationPattern IntrinsicPattern {
      get { return _intrinsic_pattern; }
      set { _intrinsic_pattern = value; }
    }

    /// <summary>
    /// Get/set the calibration pattern for the extrinsic camera calibration
    /// </summary>
    [Description("Choose the calibration pattern for the extrinsic camera calibration")]
    [TypeConverter(typeof(Core.ReflectionTypeConverter))]
    [RefreshProperties(RefreshProperties.All)]
    public Core.CalibrationPattern ExtrinsicPattern {
      get { return _extrinsic_pattern; }
      set { _extrinsic_pattern = value; }
    }
  }
}
