using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Parsley.UI.Concrete {
  public partial class StreamViewer : Form {

    public StreamViewer() {
      InitializeComponent();
      this.MaintainAspectRatio();
    }

    public Parsley.Core.FrameGrabber FrameGrabber {
      get { return _display.FrameGrabber; }
      set { _display.FrameGrabber = value; }
    }

    public Emgu.CV.CvEnum.INTER Interpolation {
      get { return _display.Interpolation; }
      set { _display.Interpolation = value; }
    }

    public Emgu.CV.UI.ImageBox.FunctionalModeOption FunctionalMode {
      get { return _display.FunctionalMode; }
      set { _display.FunctionalMode = value; }
    }


    public void Pause() {
      _display.Pause();
    }

    public void Resume() {
      _display.Resume();
    }

    protected override void OnResizeBegin(EventArgs e) {
      _display.Pause();
      base.OnResizeBegin(e);
    }

    protected override void OnResizeEnd(EventArgs e) {
      this.MaintainAspectRatio();
      _display.Resume();
      base.OnResizeEnd(e);
    }

    private void MaintainAspectRatio() {
      if (this.FrameGrabber != null) {
        Parsley.Core.Camera cam = this.FrameGrabber.Camera;
        if (cam != null && !cam.Disposed) {
          double ar = cam.FrameAspectRatio;
          this.Height = (int)Math.Ceiling(this.Width / ar);
        }
      }
    }
  }
}
