using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Parsley.Core {

  public interface ILaserLineFilterAlgorithm {

    /// <summary>
    /// Filter laser-line and return a list of filtered positions
    /// </summary>
    /// <remarks>Filter algorithms do not delete previous laser positions, but mark unwanted positions as invalid (PointF.Empty)</remarks>
    /// <param name="context">Algorithm Context</param>
    /// <param name="filtered_positions">Filtered output</param>
    bool FilterLaserLine(ILaserLineFilterAlgorithmContext context, out System.Drawing.PointF[] filtered_positions);
  }
}
