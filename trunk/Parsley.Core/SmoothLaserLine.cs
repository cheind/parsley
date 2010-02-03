using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Parsley.Core {

  /// <summary>
  /// Smoothes detected laser-line points
  /// </summary>
  public abstract class SmoothLaserLine {

    /// <summary>
    /// Smooth points
    /// </summary>
    /// <param name="points">Points to be smoothed, will be modified</param>
    public abstract void Smooth(float[] points);
  }
}
