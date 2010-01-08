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

namespace FindCheckerboard {
  public partial class Form1 : Form {

    private Parsley.Core.Capture _capture;
    private Parsley.Core.CheckerBoard _cb;
    public Form1() {
      InitializeComponent();
      _capture = Parsley.Core.Capture.FromCamera(0);
      _cb = new Parsley.Core.CheckerBoard(9, 6);
      Application.Idle += new EventHandler(Application_Idle);
    }

    void Application_Idle(object sender, EventArgs e) {
      Image<Bgr, Byte> img = _capture.Frame();
      Image<Gray, Byte> gray = img.Convert<Gray, Byte>();
      gray._EqualizeHist();

      _cb.FindPattern(gray);
      _cb.Draw(img, 4, 2);

      _image_box.Image = img;
    }
  }
}
