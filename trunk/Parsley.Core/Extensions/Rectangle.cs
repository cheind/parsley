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
  public static class RectangleExtensions {

    /// <summary>
    /// True if point is contained in rectangle, false otherwise.
    /// </summary>
    /// <param name="r">Rectangle</param>
    /// <param name="p">Point</param>
    /// <returns>True if point is contained in rectangle, false otherwise</returns>
    public static bool Contains(this System.Drawing.Rectangle r, System.Drawing.PointF p ) {
      return
        p.X >= r.X && p.X <= (r.X + r.Width) &&
        p.Y >= r.Y && p.Y <= (r.Y + r.Height);
    }
  }
}
