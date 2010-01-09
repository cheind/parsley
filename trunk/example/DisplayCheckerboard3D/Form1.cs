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

    private Parsley.Core.Capture _capture;
    private Parsley.Core.CheckerBoard _cb;
    private Parsley.Core.Calibration _calib;
    private DateTime _last_calib_image;


    public Form1() {
      InitializeComponent();
      _capture = Parsley.Core.Capture.FromCamera(0);
      _cb = new Parsley.Core.CheckerBoard(9, 6);
      _calib = new Parsley.Core.Calibration(_cb.ObjectCorners(25.0f));
      _last_calib_image = DateTime.Now;
      _status_label.Text = "Application will take a calibration image every 2 seconds until calibrated.";

      Application.Idle += new EventHandler(Application_Idle);
    }

    void Application_Idle(object sender, EventArgs e) {
      Image<Bgr, Byte> img = _capture.Frame();
      Image<Gray, Byte> gray = img.Convert<Gray, Byte>();
      gray._EqualizeHist();

      _cb.FindPattern(gray);
      if (_cb.PatternFound) {
        if (!_calib.Calibrated) {
          AddCalibrationImage(gray);
        } else {
          Emgu.CV.ExtrinsicCameraParameters ecp = _calib.FindExtrinsics(_cb.ImageCorners);
          _status_label.Text = String.Format("Distance to origin: #{0}", ecp.TranslationVector.Data[2,0]);
        }
      }

      _cb.Draw(img, 4, 2);
      _picture_box.Image = img.Resize(_picture_box.Width, _picture_box.Height, Emgu.CV.CvEnum.INTER.CV_INTER_NN);
    }

    void AddCalibrationImage(Image<Gray, Byte> img) {
      const int calib_images = 3;
      if (_cb.PatternFound && (DateTime.Now - _last_calib_image).Seconds > 2) {
        _last_calib_image = DateTime.Now;
        _calib.AddImagePoints(_cb.ImageCorners);
        if (_calib.ImagePoints.Count >= calib_images) {
          _calib.FindIntrinsics(img.Size);
          _status_label.Text = "Calibrated!";
        } else {
          _status_label.Text = String.Format("{0}/{1} required images acquired.", _calib.ImagePoints.Count, calib_images);
        }
      }

    }
  }
}
