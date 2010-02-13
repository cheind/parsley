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
    /// <summary>
    /// Defines the main color channel of the laser.
    /// Enumeration matches Emgu BGR color format.
    /// </summary>
    public enum ColorChannel {
      Blue = 0,
      Green = 1,
      Red = 2
    }

    private Laser.ColorChannel _color;

    /// <summary>
    /// Instance a default red-light laser
    /// </summary>
    public Laser() {
      _color = ColorChannel.Red;
    }

    /// <summary>
    /// Get/set the color channel the laser is to find in.
    /// </summary>
    public ColorChannel Color {
      get { return _color; }
      set { _color = value; }
    }
  }
}
