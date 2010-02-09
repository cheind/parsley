using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Parsley.Core {
  public class AlgorithmContext : IAlgorithmContext {
    Emgu.CV.Image<Emgu.CV.Structure.Bgr, byte> _image;
    Emgu.CV.IntrinsicCameraParameters _intrinsics;

    public AlgorithmContext() { }

    public Emgu.CV.Image<Emgu.CV.Structure.Bgr, byte> Image {
      get { return _image; }
      set { _image = value; }
    }

    public Emgu.CV.IntrinsicCameraParameters Intrinsics {
      get { return _intrinsics; }
      set { _intrinsics = value; }
    }
  }
}
