using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LaserLineDetection {
  public partial class Form1 : Parsley.UI.Concrete.StreamViewer {
    private Parsley.Core.Camera _camera;
    private Parsley.Core.FrameGrabber _grabber;
    private Emgu.CV.Image<Emgu.CV.Structure.Gray, Byte> _reference;
    private Parsley.Core.BrightestPixelLLE _lle;
    DateTime _start;

    public Form1() {
      _camera = new Parsley.Core.Camera(0);
      _grabber = new Parsley.Core.FrameGrabber(_camera);
      _grabber.OnFrame += new Parsley.Core.FrameGrabber.OnFrameHandler(_grabber_OnFrame);
      _lle = new Parsley.Core.BrightestPixelLLE();
      _lle.IntensityThreshold = 20;
      this.FrameGrabber = _grabber;
      _start = DateTime.Now;
      _grabber.Start();
    }

    protected override void OnFormClosing(FormClosingEventArgs e) {
      _grabber.RequestStop();
      _camera.Dispose();
      base.OnFormClosing(e);
    }

    Emgu.CV.IImage _grabber_OnFrame(Parsley.Core.FrameGrabber fg, Emgu.CV.IImage img) {
      Emgu.CV.Image<Emgu.CV.Structure.Bgr, Byte> casted = img as Emgu.CV.Image<Emgu.CV.Structure.Bgr, Byte>;
      //Emgu.CV.Image<Emgu.CV.Structure.Gray, Byte>[] channel = casted.Split();
      Emgu.CV.Image<Emgu.CV.Structure.Gray, Byte> gray = casted.Convert<Emgu.CV.Structure.Gray, Byte>();
      if (_reference == null) {
        if ((DateTime.Now - _start).Seconds > 5) {
          //_reference = channel[1];
          _reference = gray;
        }
      } else {
        Emgu.CV.Image<Emgu.CV.Structure.Gray, Byte> result = new Emgu.CV.Image<Emgu.CV.Structure.Gray, byte>(img.Size);
        //_lle.FindLaserLine(channel[1].Sub(_reference));
        _lle.FindLaserLine(gray);
        foreach (PointF p in _lle.LaserPoints) {
          if (p.Y >= 0.0f) {
            result[(int)p.Y,(int)p.X] = new Emgu.CV.Structure.Gray(100);
          }
        }
        img = result;
      }
      return img;
    }


  }
}
