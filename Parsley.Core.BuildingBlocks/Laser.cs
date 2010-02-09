using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Parsley.Core.BuildingBlocks {

  /// <summary>
  /// Laser data and algorithms
  /// </summary>
  [Serializable]
  public class Laser {
    /// <summary>
    /// Defines the main color channel of the laser.
    /// Enumeration matches Emgu BGR color format.
    /// </summary>
    public enum ColorChannel {
      Blue = 0,
      Green = 1,
      Red = 2
    }

    private Laser.ColorChannel _color;
    private Core.ILaserLineAlgorithm _line_algorithm;
    private Core.ILaserPlaneAlgorithm _plane_algorithm;

    /// <summary>
    /// Instance a default red-light laser
    /// </summary>
    public Laser() {
      _color = ColorChannel.Red;
      _line_algorithm = new LaserLineAlgorithms.WeightedAverage(220);
      _plane_algorithm = new LaserPlaneAlgorithms.PlaneRansac();
    }

    /// <summary>
    /// Get/set the color channel the laser is to find in.
    /// </summary>
    public ColorChannel Color {
      get { return _color; }
      set { _color = value; }
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
  }
}
