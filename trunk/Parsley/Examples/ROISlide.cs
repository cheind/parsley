using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Parsley.Examples {
  public partial class ROISlide : FrameGrabberSlide {

    public ROISlide(Context c) : base(c) { }

    public ROISlide(): base(null) {
      InitializeComponent();
    }

    override protected void OnFrame(Parsley.Core.FrameGrabber fp, Emgu.CV.Image<Emgu.CV.Structure.Bgr, byte> img) {
      if (Context.ROI != Rectangle.Empty) {
        img.Draw(Context.ROI, new Emgu.CV.Structure.Bgr(Color.Green), 1);
      }
    }
  }
}
