using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;
using Emgu.CV.Structure;

namespace Parsley.Core.Tests {

  [TestFixture]
  public class BrightestPixelLLE {

    [Test]
    public void ImageA() {
      System.Drawing.Bitmap b = new System.Drawing.Bitmap(@"../Parsley.Core.Tests/etc/brightest_lle/a.bmp");
      Emgu.CV.Image<Gray, byte> img = new Emgu.CV.Image<Gray, byte>(b);
      
      Parsley.Core.LaserLineAlgorithms.BrightestPixel lle = new Parsley.Core.LaserLineAlgorithms.BrightestPixel();
      lle.IntensityThreshold = 1;

      float[] laser_pos;
      lle.FindLaserLine(img, out laser_pos);
      Assert.AreEqual(10, laser_pos.Length);

      Assert.AreEqual(0, laser_pos[0]);
      Assert.AreEqual(-1, laser_pos[1]);
      Assert.AreEqual(-1, laser_pos[2]);
      Assert.AreEqual(2, laser_pos[3]);
      Assert.AreEqual(6, laser_pos[4]);
      Assert.AreEqual(6, laser_pos[5]);
      Assert.AreEqual(9, laser_pos[6]);
      Assert.AreEqual(9, laser_pos[7]);
      Assert.AreEqual(9, laser_pos[8]);
      Assert.AreEqual(9, laser_pos[9]);
    }
  }
}
