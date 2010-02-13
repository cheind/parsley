/*
 * Parsley http://parsley.googlecode.com
 * Copyright (c) 2010, Christoph Heindl. All rights reserved.
 * Code license:	New BSD License
 */

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

    private FrameGrabberSlide() : base(null) {
      InitializeComponent();
    }

    protected override void OnSlidingIn() {
      this.Context.FrameGrabber.OnFramePrepend += new Parsley.Core.BuildingBlocks.FrameGrabber.OnFrameHandler(this.OnFrame);
      this.Context.FrameGrabber.Start();
      base.OnSlidingIn();
    }

    protected override void OnSlidingOut(CancelEventArgs args) {
      this.Context.FrameGrabber.OnFramePrepend -= new Parsley.Core.BuildingBlocks.FrameGrabber.OnFrameHandler(this.OnFrame);
      base.OnSlidingOut(args);
    }

    protected virtual void OnFrame(Parsley.Core.BuildingBlocks.FrameGrabber fp, Emgu.CV.Image<Emgu.CV.Structure.Bgr, byte> img) { }
  }
}
