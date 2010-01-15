using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Parsley {
  public partial class FrameGrabberSlide : UI.ParsleySlide {
    private Core.FrameGrabber _fg;

    public FrameGrabberSlide() {
      InitializeComponent();
    }

    public FrameGrabberSlide(Core.FrameGrabber fg) {
      _fg = fg;
      InitializeComponent();
    }

    /// <summary>
    /// Access the framegrabber
    /// </summary>
    public Core.FrameGrabber FrameGrabber {
      get { return _fg; }
      set { _fg = value; }
    }

    public override void OnSlideShowing() {
      this.FrameGrabber.OnFramePrepend += new Parsley.Core.FrameProducer.OnFrameHandler(this.OnFrame);
      this.FrameGrabber.Start();
      base.OnSlideShowing();
    }

    public override void OnSlideHiding(CancelEventArgs args) {
      this.FrameGrabber.OnFramePrepend -= new Parsley.Core.FrameProducer.OnFrameHandler(this.OnFrame);
      base.OnSlideHiding(args);
    }

    protected virtual void OnFrame(Parsley.Core.FrameProducer fp, Emgu.CV.Image<Emgu.CV.Structure.Bgr, byte> img) { }
  }
}
