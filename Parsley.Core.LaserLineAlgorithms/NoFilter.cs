using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Parsley.Core.LaserLineAlgorithms {

  /// <summary>
  /// Performs no laser-line filtering
  /// </summary>
  [Serializable]
  [Core.Addins.Addin]
  public class NoFilter : Core.ILaserLineFilterAlgorithm {

    public NoFilter() {}

    public bool FilterLaserLine(ILaserLineFilterAlgorithmContext context, out System.Drawing.PointF[] filtered_positions) {
      filtered_positions = context.LaserPoints;
      return true;
    }

  }
}
