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
  /// Interace that contains the relevant information for various algorithms
  /// </summary>
  public interface IAlgorithmContext {
    /// <summary>
    /// Current camera frame
    /// </summary>
    Emgu.CV.Image<Emgu.CV.Structure.Bgr, byte> Image { get; }

    /// <summary>
    /// Intrinsic camera calibration
    /// </summary>
    Emgu.CV.IntrinsicCameraParameters Intrinsics { get; }

    /// <summary>
    /// The set of calibrated reference planes
    /// </summary>
    Plane[] ReferencePlanes { get; }


    /// <summary>
    /// Region of scanning interest
    /// </summary>
    System.Drawing.Rectangle ROI { get; }
  }
}
