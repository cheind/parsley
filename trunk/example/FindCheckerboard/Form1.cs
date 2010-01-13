using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


using Emgu.CV;
using Emgu.CV.Structure;

namespace FindCheckerboard {
  public partial class Form1 : Parsley.UI.Concrete.StreamViewer {
    private Parsley.Core.Camera _camera;
    private Parsley.Core.FrameGrabber _grabber;
    private Parsley.Core.CheckerBoard _cb;

    public Form1() {
      InitializeComponent();
      _camera = new Parsley.Core.Camera(0);
      _cb = new Parsley.Core.CheckerBoard(9, 6);
      _grabber = new Parsley.Core.FrameGrabber(_camera);
      _grabber.OnFrame += new Parsley.Core.FrameGrabber.OnFrameHandler(_grabber_OnFrame);
      this.FrameGrabber = _grabber;
      _grabber.Start();
    }

    protected override void OnFormClosing(FormClosingEventArgs e) {
      _grabber.RequestStop();
      _camera.Dispose();
    }

    void _grabber_OnFrame(Parsley.Core.FrameGrabber fg, Image<Bgr, byte> img) {
      Image<Gray, Byte> gray = img.Convert<Gray, Byte>();
      gray._EqualizeHist();

      _cb.FindPattern(gray);
      _cb.Draw(img, 4, 2);
    }
  }
}
