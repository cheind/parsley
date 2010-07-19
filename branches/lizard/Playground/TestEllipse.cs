using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

using Emgu.CV.Structure;
using MathNet.Numerics.LinearAlgebra;

namespace Playground {

  [Parsley.Core.Addins.Addin]
  public class TestEllipse : Parsley.Core.IImageAlgorithm {

    public TestEllipse() { }
    
    public void ProcessImage(Emgu.CV.Image<Emgu.CV.Structure.Bgr, byte> image) {
      MCvBox2D mybox = new MCvBox2D(new System.Drawing.PointF(100, 100), new System.Drawing.Size(50, 30), 110);
      MCvBox2D mybox2 = new MCvBox2D(new System.Drawing.PointF(100, 100), new System.Drawing.Size(50, 30), 0);

      image.Draw(new Ellipse(mybox), new Bgr(0,0,255), 2);
      image.Draw(new Ellipse(mybox2), new Bgr(0, 255, 0), 2);
    }
  }
}
