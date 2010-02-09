using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Parsley.Core {
  public class LaserLineAlgorithmContext : AlgorithmContext, ILaserLineAlgorithmContext {
    private Emgu.CV.Image<Emgu.CV.Structure.Gray, byte> _channel_image;

    public LaserLineAlgorithmContext() { }
    
    public Emgu.CV.Image<Emgu.CV.Structure.Gray, byte> ChannelImage {
      get { return _channel_image; }
      set { _channel_image = value; }
    }

    
  }
}
