using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;


namespace Playground {

  [Parsley.Core.Addins.Addin]
  public class FilterHoleBoundaries : Parsley.Core.ILaserLineFilterAlgorithm {
    public FilterHoleBoundaries() { }
    public bool FilterLaserLine(Parsley.Core.ILaserLineFilterAlgorithmContext context, out System.Drawing.PointF[] filtered_positions) {
      filtered_positions = context.LaserPoints;
      if (context.LaserPoints.Length < 2)
        return false;

      PointF last = context.LaserPoints[0];
      for (int i = 1; i < context.LaserPoints.Length; ++i) {
        PointF cur = context.LaserPoints[i];
        if (cur.IsEmpty) {
          // Current is invalid
          context.LaserPoints[i - 1] = PointF.Empty;
        } else if (last.IsEmpty) {
          context.LaserPoints[i] = PointF.Empty;
        }
        last = cur;
      }
      return true;
    }
  }
}
