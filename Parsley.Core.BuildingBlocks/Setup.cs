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
    private World _world;
    private ScanWorkflow _wf;
    private Core.CalibrationPattern _intrinsic_pattern;
    private Core.CalibrationPattern _extrinsic_pattern;

    /// <summary>
    /// Construct default setup
    /// </summary>
    public Setup() {
      _world = new World();
      _wf = new ScanWorkflow();
      _intrinsic_pattern = new Core.CalibrationPatterns.CheckerBoard(9, 6, 25.0f);
      _extrinsic_pattern = new Core.CalibrationPatterns.CheckerBoard(9, 6, 10.0f);
    }

    /// <summary>
    /// Get/set world components
    /// </summary>
    [Description("Adjust settings of world components")]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public World World {
      get { return _world; }
      set { _world = value; }
    }

    /// <summary>
    /// Used instead of DefaultValueAttribute
    /// </summary>
    public bool ShouldSerializeWorld() {
      return false;
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
    /// Get/set the calibration pattern for the intrinsic camera calibration
    /// </summary>
    [Description("Choose the calibration pattern for the intrinsic camera calibration")]
    [Category("Calibration")]
    [TypeConverter(typeof(Core.Addins.ReflectionTypeConverter))]
    [RefreshProperties(RefreshProperties.All)]
    public Core.CalibrationPattern IntrinsicPattern {
      get { return _intrinsic_pattern; }
      set { _intrinsic_pattern = value; }
    }


    /// <summary>
    /// Used instead of DefaultValueAttribute
    /// </summary>
    public bool ShouldSerializeIntrinsicPattern() {
      return _intrinsic_pattern.GetType() != typeof(Core.CalibrationPatterns.CheckerBoard);
    }

    /// <summary>
    /// Get/set the calibration pattern for the extrinsic camera calibration
    /// </summary>
    [Description("Choose the calibration pattern for the extrinsic camera calibration")]
    [Category("Calibration")]
    [TypeConverter(typeof(Core.Addins.ReflectionTypeConverter))]
    [RefreshProperties(RefreshProperties.All)]
    public Core.CalibrationPattern ExtrinsicPattern {
      get { return _extrinsic_pattern; }
      set { _extrinsic_pattern = value; }
    }

    /// <summary>
    /// Used instead of DefaultValueAttribute
    /// </summary>
    public bool ShouldSerializeExtrinsicPattern() {
      return _intrinsic_pattern.GetType() != typeof(Core.CalibrationPatterns.CheckerBoard);
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
