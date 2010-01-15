using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace CaptureFromCamera {
  public partial class Form1 : Form {
    Parsley.Core.Camera _camera;
    Parsley.Core.FrameGrabber _fg;
    

    public Form1() {
      InitializeComponent();
    }

    protected override void OnFormClosing(FormClosingEventArgs e) {
      if (_fg != null) {
        _fg.Dispose(); // First dispose the framegrabber.
        _camera.Dispose();
      }
      base.OnFormClosing(e);
    }

    private void _button_show_Click(object sender, EventArgs e) {
      if (_camera == null) {
        try {
          _camera = new Parsley.Core.Camera(0);
          _fg = new Parsley.Core.FrameGrabber(_camera);
          _fg.Start();
        } catch (ArgumentException) {
          Parsley.UI.ParsleyMessage.Show("Cannot find Camera", "Cannot connect to camera. Is there a camera pluged in?");
        }
      }
      if (_camera != null) {
        Parsley.UI.Concrete.StreamViewer sv = new Parsley.UI.Concrete.StreamViewer();
        sv.FrameGrabber = _fg;
        sv.FunctionalMode = Emgu.CV.UI.ImageBox.FunctionalModeOption.RightClickMenu;
        sv.Show();
      }
    }
  }
}
