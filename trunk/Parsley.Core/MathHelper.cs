using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Parsley.Core {
  public static class MathHelper {
    public static double Clamp(double value, double min, double max) {
      if (value < min) return min;
      else if (value > max) return max;
      else return value;
    }
  }
}
