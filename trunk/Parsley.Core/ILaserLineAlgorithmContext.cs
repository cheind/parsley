using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Parsley.Core {

  /// <summary>
  /// Defines the context for laser-line finder algorithms
  /// </summary>
  public interface ILaserLineAlgorithmContext : IAlgorithmContext {
    /// <summary>
    /// Channel image of the current image of the current camera image
    /// </summary>
    Emgu.CV.Image<Emgu.CV.Structure.Gray, byte> ChannelImage { get; }
  }
}
