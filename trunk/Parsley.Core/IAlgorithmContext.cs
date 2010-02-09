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
  }
}
