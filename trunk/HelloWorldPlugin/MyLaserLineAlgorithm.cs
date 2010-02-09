using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HelloWorldPlugin {

  [Serializable]
  [Parsley.Core.Addins.Addin]
  public class MyLaserLineAlgorithm : Parsley.Core.ILaserLineAlgorithm {
    public MyLaserLineAlgorithm() { }

    public void FindLaserLine(Parsley.Core.ILaserLineAlgorithmContext context, out System.Drawing.PointF[] laser_pos) {
      laser_pos = new System.Drawing.PointF[0];
    }
  }
}
