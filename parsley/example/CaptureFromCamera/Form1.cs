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
    Parsley.Core.Capture _capture;

    public Form1() {
      InitializeComponent();
      _capture = Parsley.Core.Capture.FromCamera(0);
      Application.Idle += new EventHandler(Application_Idle);
    }

    void Application_Idle(object sender, EventArgs e) {
      _picturebox.Image = _capture.Frame();
    }
  }
}
