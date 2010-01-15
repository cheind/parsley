using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Parsley {
  public partial class FrameGrabberSlide : UI.Slide {
    private Core.FrameGrabber _fg;

    public FrameGrabberSlide() {
      InitializeComponent();
    }

    public FrameGrabberSlide(Core.FrameGrabber fg) {
      _fg = fg;
      InitializeComponent();
    }

    public Core.FrameGrabber FrameGrabber {
      get { return _fg; }
      set { _fg = value; }
    }
  }
}
