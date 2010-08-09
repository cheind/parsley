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
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Parsley.Core.BuildingBlocks {

  [Serializable]
  public class Setup {
    private BuildingBlocks.Camera _camera;
    private BuildingBlocks.Laser _laser;
    private BuildingBlocks.ReferenceBody _reference_body;
    //private BuildingBlocks.RotaryPositioner _rotary_positioner;
    private IPositioner _positioner;
    private ScanWorkflow _wf;

    /// <summary>
    /// Construct default setup
    /// </summary>
    public Setup() {
      _wf = new ScanWorkflow();
      _camera = new Camera(0);
      _laser = new Laser();
      _reference_body = new ReferenceBody();
      _positioner = new MarkerPositioner();
    }

    /// <summary>
    /// Get/set scan workflow
    /// </summary>
    [Description("Adjust scan workflow settings")]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public ScanWorkflow ScanWorkflow {
      get { return _wf; }
      set { _wf = value; }
    }


    /// <summary>
    /// Used instead of DefaultValueAttribute
    /// </summary>
    public bool ShouldSerializeScanWorkflow() {
      return false;
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
    /// Reference body
    /// </summary>
    [Description("Setup the reference body.")]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public BuildingBlocks.ReferenceBody ReferenceBody {
      get { return _reference_body; }
      set { _reference_body = value; }
    }


    /// <summary>
    /// Used instead of DefaultValueAttribute
    /// </summary>
    public bool ShouldSerializeReferenceBody() {
      return false;
    }

    /// <summary>
    /// Positioner
    /// </summary>
    [Description("Setup the positioner if available.")]
    [TypeConverter(typeof(Core.Addins.ReflectionTypeConverter))]
    [RefreshProperties(RefreshProperties.All)]
    public IPositioner Positioner {
      get { return _positioner; }
      set { _positioner = value; }
    }


    /// <summary>
    /// Used instead of DefaultValueAttribute
    /// </summary>
    public bool ShouldSerializePositioner() {
      return false;
    }


    /// <summary>
    /// Load setup from file
    /// </summary>
    /// <param name="path">Path to configuration</param>
    /// <returns>Setup</returns>
    public static Setup LoadBinary(string path) {
      using (Stream s = File.Open(path, FileMode.Open)) {
        if (s != null) {
          IFormatter formatter = new BinaryFormatter();
          Setup setup = formatter.Deserialize(s) as Setup;
          s.Close();
          if (setup == null) {
            throw new TypeLoadException("Could not load setup from file");
          }
          return setup;
        } else {
          throw new FileLoadException("Could not open file");
        }
      }
    }

    /// <summary>
    /// Save setup to file
    /// </summary>
    /// <param name="path"></param>
    /// <param name="s"></param>
    public static void SaveBinary(string path, Setup setup) {
      using (Stream s = File.OpenWrite(path)) {
        if (s != null) {
          IFormatter formatter = new BinaryFormatter();
          formatter.Serialize(s, setup);
          s.Close();
        }
      }
    }
  }
}
