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
    private Core.LaserLineExtraction _algorithm;

    /// <summary>
    /// Instance a default red-light laser
    /// </summary>
    public Laser() {
      _color = ColorChannel.Red;
      _algorithm = new WeightedAverageLLE(30);
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
    [TypeConverter(typeof(Core.ReflectionTypeConverter))]
    [RefreshProperties(RefreshProperties.All)]
    public LaserLineExtraction LaserLineAlgorithm {
      get { return _algorithm; }
      set { _algorithm = value; }
    }

    /// <summary>
    /// Access the set of valid laser-points from the last extraction
    /// </summary>
    [Browsable(false)]
    public IEnumerable<System.Drawing.PointF> ValidLaserPoints {
      get {
        float[] positions = _algorithm.LaserPositions;
        if (positions != null) {
          for (int x = 0; x < positions.Length; ++x) {
            float y = positions[x];
            if (y > 0.0f)
              yield return new System.Drawing.PointF(x, y);
          }
        }
      }
    }

    /// <summary>
    /// Find laser line in channel image and make it accessible through local properties.
    /// </summary>
    /// <param name="channel">Image to search in.</param>
    /// <param name="laser_points">Laser points found.</param>
    public void FindLaserLine(Emgu.CV.Image<Emgu.CV.Structure.Bgr, byte> img) {
      using (Emgu.CV.Image<Emgu.CV.Structure.Gray, byte> channel = img[(int)_color]) {
        _algorithm.FindLaserLine(channel);
      }
    }
  }
}
