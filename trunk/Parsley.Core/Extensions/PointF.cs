/*
 * Parsley http://parsley.googlecode.com
 * Copyright (c) 2010, Christoph Heindl. All rights reserved.
 * Code license:	New BSD License
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Parsley.Core.Extensions {


  public static class PointFExtensions {

    /// <summary>
    /// Convert to nearest point by rounding.
    /// </summary>
    /// <param name="p">Point to be converted</param>
    /// <returns>Rounded point</returns>
    public static System.Drawing.Point ToNearestPoint(this System.Drawing.PointF p) {
      return new System.Drawing.Point((int)(Math.Round(p.X)),
                                      (int)(Math.Round(p.Y)));
    }
  }
}
