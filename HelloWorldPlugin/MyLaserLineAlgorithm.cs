using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HelloWorldPlugin {

  [Serializable]
  [Parsley.Core.Addins.Addin]
  public class MyLaserLineAlgorithm : Parsley.Core.LaserLineExtraction {
    public MyLaserLineAlgorithm() { }

    public override void FindLaserLine(Emgu.CV.Image<Emgu.CV.Structure.Gray, byte> channel, out float[] laser_pos) {
      laser_pos = new float[0];
    }
  }
}
