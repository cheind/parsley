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
      try {
        _camera = new Parsley.Core.Camera(0);
        _cb = new Parsley.Core.CheckerBoard(9, 6, 25.0f);
        _grabber = new Parsley.Core.FrameGrabber(_camera);
        // OnFrame requests are processed in the order they are registered.
        _grabber.OnFrame += new Parsley.Core.FrameGrabber.OnFrameHandler(_grabber_OnFrame);
        this.FrameGrabber = _grabber;
        _grabber.Start();
      } catch (ArgumentException) {
        Parsley.UI.ParsleyMessage.Show("No Camera", "No camera connected");
        this.Close();
      }
    }

    protected override void OnFormClosing(FormClosingEventArgs e) {
      if (_grabber != null) {
        _grabber.OnFrame -= new Parsley.Core.FrameGrabber.OnFrameHandler(_grabber_OnFrame);
        _grabber.RequestStop();
        _camera.Dispose();
      }
    }

    Emgu.CV.IImage _grabber_OnFrame(Parsley.Core.FrameGrabber fg, Emgu.CV.IImage img) {
      Emgu.CV.Image<Emgu.CV.Structure.Bgr, Byte> casted = img as Emgu.CV.Image<Emgu.CV.Structure.Bgr, Byte>;
      Image<Gray, Byte> gray = casted.Convert<Gray, Byte>();
      gray._EqualizeHist();

      _cb.FindPattern(gray);
      _cb.DrawPattern(casted, _cb.ImagePoints, _cb.PatternFound);
      return img;
    }
  }
}
