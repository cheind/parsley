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

namespace DisplayCheckerboard3D {
  public partial class Form1 : Form {
    private Parsley.Core.IntrinsicCalibration _calib;
    private Emgu.CV.IntrinsicCameraParameters _intrinsics;
    public Form1() {
      InitializeComponent();
    }
    
    private void _button_calibrate_Click(object sender, EventArgs e) {
      Parsley.Core.Capture c = Parsley.Core.Capture.FromCamera(0);
      Parsley.UI.IntrinsicCalibration ic = new Parsley.UI.IntrinsicCalibration(c, 3);
      ic.ShowDialog();
      c.Dispose();
      _calib = ic.Calibration;
      _intrinsics = ic.Intrinsics;
      _button_3d.Enabled = _intrinsics != null;
    }

    private void _button_3d_Click(object sender, EventArgs e) {
      LocateBoard lb = new LocateBoard(_calib, _intrinsics);
      lb.ShowDialog();
    }
  }
}
