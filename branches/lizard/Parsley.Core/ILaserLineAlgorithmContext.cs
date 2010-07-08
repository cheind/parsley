/*
 * Parsley http://parsley.googlecode.com
 * Copyright (c) 2010, Christoph Heindl. All rights reserved.
 * Code license:	New BSD License
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Parsley.Core {

  /// <summary>
  /// Defines the main color channel of the laser.
  /// Enumeration matches Emgu BGR color format.
  /// </summary>
  public enum ColorChannel {
    Blue = 0,
    Green = 1,
    Red = 2
  }

  /// <summary>
  /// Defines the context for laser-line finder algorithms
  /// </summary>
  public interface ILaserLineAlgorithmContext : IAlgorithmContext {
    /// <summary>
    /// Access the laser color
    /// </summary>
    ColorChannel LaserColor { get; }
  }
}
