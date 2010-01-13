using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Emgu.CV.Structure;

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
    private System.Drawing.PointF[] _laser_points;

    /// <summary>
    /// Get or set the laser pixels.
    /// </summary>
    public System.Drawing.PointF[] LaserPoints {
      get { return _laser_points; }
      set { _laser_points = value; }
    }

    /// <summary>
    /// Find laser line in channel image and make it accessible through local properties.
    /// </summary>
    /// <param name="channel">Image to search in.</param>
    /// <param name="laser_points">Laser points found.</param>
    public void FindLaserLine(Emgu.CV.Image<Gray, byte> channel) {
      this.FindLaserLine(channel, out _laser_points);
    }

    /// <summary>
    /// Find laser line in channel image.
    /// </summary>
    /// <param name="channel">Image to search in.</param>
    public abstract void FindLaserLine(Emgu.CV.Image<Gray, byte> channel, out System.Drawing.PointF[] laser_points);
  }
}
