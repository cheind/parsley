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
    private List<Core.Plane> _reference_planes;
    private List<Emgu.CV.ExtrinsicCameraParameters> _extrinsics;

    /// <summary>
    /// Default constructor of entities
    /// </summary>
    public World() {
      _camera = new Camera(0);
      _laser = new Laser();
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
    /// Used instead of DefaultValueAttribute
    /// </summary>
    public bool ShouldSerializeCamera() {
      return false;
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
    /// Used instead of DefaultValueAttribute
    /// </summary>
    public bool ShouldSerializeLaser() {
      return false;
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


  }
}
