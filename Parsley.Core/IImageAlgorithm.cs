/*
 * Parsley http://parsley.googlecode.com
 * Copyright (c) 2010, Christoph Heindl. All rights reserved.
 * Code license:	New BSD License
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Emgu.CV.Structure;
using System.ComponentModel;

namespace Parsley.Core {

  /// <summary>
  /// Base class for algorithms on images.
  /// </summary>
  /// <remarks>
  /// </remarks>
  public interface IImageAlgorithm {

    /// <summary>
    /// Process Image
    /// </summary>
    /// <param name="image">Image to process</param>
    void ProcessImage(Emgu.CV.Image<Emgu.CV.Structure.Bgr, byte> image);
  }
}
