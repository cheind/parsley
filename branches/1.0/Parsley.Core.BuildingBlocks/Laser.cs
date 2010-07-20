/*
 * Parsley http://parsley.googlecode.com
 * Copyright (c) 2010, Christoph Heindl. All rights reserved.
 * Code license:	New BSD License
 */

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

    private Core.ColorChannel _color;

    /// <summary>
    /// Instance a default red-light laser
    /// </summary>
    public Laser() {
      _color = Core.ColorChannel.Red;
    }

    /// <summary>
    /// Get/set the color channel the laser is to find in.
    /// </summary>
    public Core.ColorChannel Color {
      get { return _color; }
      set { _color = value; }
    }
  }
}
