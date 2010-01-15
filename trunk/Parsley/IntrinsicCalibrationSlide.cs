using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Emgu.CV;
using Emgu.CV.Structure;

namespace Parsley {
  public partial class IntrinsicCalibrationSlide : FrameGrabberSlide {
    private Core.CalibrationPattern _pattern;
    private Core.IntrinsicCalibration _ic;
    private bool _take_image_request;


    public IntrinsicCalibrationSlide(Core.FrameGrabber fg, Core.CalibrationPattern pattern) : base(fg) {
      InitializeComponent();
      _pattern = pattern;
      _ic = new Parsley.Core.IntrinsicCalibration(pattern.ObjectPoints, fg.Camera.FrameSize);
    }

    /// <summary>
    /// Get and set the calibration pattern
    /// </summary>
    public Core.CalibrationPattern CalibrationPattern {
      get { return _pattern; }
      set { _pattern = value; }
    }

    protected override void OnFrame(Parsley.Core.FrameProducer fp, Emgu.CV.Image<Emgu.CV.Structure.Bgr, byte> img) {
      Image<Gray, Byte> gray = img.Convert<Gray, Byte>();
      gray._EqualizeHist();
      _pattern.FindPattern(gray);
      this.HandleTakeImageRequest();
      _pattern.DrawPattern(img, _pattern.ImagePoints, _pattern.PatternFound);
    }

    void HandleTakeImageRequest() {
      if (_take_image_request) {
        if (_pattern.PatternFound) {
          _ic.AddView(_pattern.ImagePoints);
          this.Invoke((MethodInvoker)delegate {
            _lbl_info.Text = String.Format("You have successfully acquired #{0} calibration images", _ic.Views.Count);
          });
        }
        _take_image_request = false;
      }
    }

    private void _btn_take_image_Click(object sender, EventArgs e) {
      _take_image_request = true;
    }
  }
}
