using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Parsley {
  public partial class FrameGrabberSlide : ContextSlide {
    
    public FrameGrabberSlide(Context c) : base(c) {
      InitializeComponent();
    }

    protected override void OnSlidingIn() {
      this.Context.FrameGrabber.OnFramePrepend += new Parsley.Core.FrameProducer.OnFrameHandler(this.OnFrame);
      this.Context.FrameGrabber.Start();
      base.OnSlidingIn();
    }

    protected override void OnSlidingOut(CancelEventArgs args) {
      this.Context.FrameGrabber.OnFramePrepend -= new Parsley.Core.FrameProducer.OnFrameHandler(this.OnFrame);
      base.OnSlidingOut(args);
    }

    protected virtual void OnFrame(Parsley.Core.FrameProducer fp, Emgu.CV.Image<Emgu.CV.Structure.Bgr, byte> img) { }
  }
}
