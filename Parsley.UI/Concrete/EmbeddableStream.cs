using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Parsley.UI.Concrete {
  public partial class EmbeddableStream : UserControl {
    private Core.FrameGrabber _grabber;

    public EmbeddableStream() {
      InitializeComponent();
    }

    public void GrabFrom(Core.FrameGrabber fg) {
      _grabber = fg;
      _grabber.OnFrame += new Parsley.Core.FrameGrabber.OnFrameHandler(_grabber_OnFrame);
    }

    void _grabber_OnFrame(Parsley.Core.FrameGrabber fg, Emgu.CV.Image<Emgu.CV.Structure.Bgr, byte> img) {
      MethodInvoker update = delegate {
        _picture_box.Image = img;
      };
      this.BeginInvoke(update); // Invoke Asnyc
    }
  }
}
