using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Emgu.CV.Structure;
using System.ComponentModel;

namespace Parsley.Core {

  /// <summary>
  /// Base class for extraction of laser lines.
  /// </summary>
  /// <remarks>
  /// Laser line is extracted in each x-column of a single channel byte image.
  /// The result is a collection of floating point values that mark the location
  /// of the laser line per x-pixel. When locations with a value greater than
  /// zero are observed, the laser line was found for that x-pixel.
  /// </remarks>
  public abstract class LaserLineExtraction {
    private float[] _laser_positions;

    /// <summary>
    /// Get or set the laser pixels.
    /// </summary>
    [Browsable(false)]
    public float[] LaserPositions {
      get { return _laser_positions; }
      set { _laser_positions = value; }
    }

    /// <summary>
    /// Access the set of valid laser-points
    /// </summary>
    public IEnumerable<System.Drawing.PointF> ValidLaserPoints {
      get {
        for (int x = 0; x < _laser_positions.Length; ++x) {
          float y = _laser_positions[x];
          if (y>0.0f)
            yield return new System.Drawing.PointF(x,y);
        }
      }
    }

    /// <summary>
    /// Find laser line in channel image and make it accessible through local properties.
    /// </summary>
    /// <param name="channel">Image to search in.</param>
    /// <param name="laser_points">Laser points found.</param>
    public void FindLaserLine(Emgu.CV.Image<Gray, byte> channel) {
      this.FindLaserLine(channel, out _laser_positions);
    }

    /// <summary>
    /// Find laser line in channel image.
    /// </summary>
    /// <param name="channel">Image to search in.</param>
    public abstract void FindLaserLine(Emgu.CV.Image<Gray, byte> channel, out float[] laser_pos);
  }
}
