using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CaptureFromCamera {
  public partial class Form1 : Form {
    Parsley.Core.Camera _camera;
    Parsley.Core.FrameGrabber _fg;
    

    public Form1() {
      InitializeComponent();
      _camera = new Parsley.Core.Camera(0);
      _fg = new Parsley.Core.FrameGrabber(_camera);
      _display.GrabFrom(_fg);
      _fg.Start();
    }

    protected override void OnFormClosing(FormClosingEventArgs e) {
      _fg.Stop();
      base.OnFormClosing(e);
    }
  }
}
