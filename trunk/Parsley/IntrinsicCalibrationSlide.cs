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
    private Core.IntrinsicCalibration _ic;
    private Core.ExtrinsicCalibration _ec; // used for illustration of coordinate frame only.
    private bool _take_image_request;
    private BackgroundWorker _bw_calibrator;
    private Timer _timer_auto;

    /// <summary>
    /// Invoked whenever a calibration succeeds
    /// </summary>
    public event EventHandler<EventArgs> OnCalibrationSucceeded;


    public IntrinsicCalibrationSlide(Context c) : base(c) {
      InitializeComponent();
      _ic = new Parsley.Core.IntrinsicCalibration(c.CalibrationPattern.ObjectPoints, Context.Camera.FrameSize);

      _timer_auto = new Timer();
      _timer_auto.Interval = 3000;
      _timer_auto.Tick += new EventHandler(_timer_auto_Tick);

      _bw_calibrator = new BackgroundWorker();
      _bw_calibrator.DoWork += new DoWorkEventHandler(_bw_calibrator_DoWork);
      _bw_calibrator.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_bw_calibrator_RunWorkerCompleted);
    }


    protected override void OnSlidingIn() {
      base.OnSlidingIn();
      _timer_auto.Enabled = false;
      _cb_auto_take.Checked = false;
      _btn_calibrate.Enabled = false;
      _btn_take_image.Enabled = true;
      if (this.Context.FrameGrabber.Camera.HasIntrinsics) {
        _lbl_info.Text = "The camera already has a calibration. You can restart the calibration process by taking images. You need at least 3 images to proceed.";
      } else {
        _lbl_info.Text = "You can restart the calibration process by taking images. You need at least 3 images to proceed.";
      }
      
      _ic.ClearViews();
    }

    protected override void OnSlidingOut(CancelEventArgs args) {
      if (_bw_calibrator.IsBusy) {
        args.Cancel = true;
      } else {
        _timer_auto.Enabled = false;
        base.OnSlidingOut(args);
      }
    }

    void _timer_auto_Tick(object sender, EventArgs e) {
      _take_image_request = true;
    }

    void _bw_calibrator_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
      _ic.ClearViews();
      _btn_calibrate.Enabled = false;
      _btn_take_image.Enabled = true;
      _cb_auto_take.Enabled = true;
      _cb_auto_take.Checked = false;
      _lbl_info.Text = "Calibration succeeded!";
      EventHandler<EventArgs> d = OnCalibrationSucceeded;
      d(this, new EventArgs());
    }

    void _bw_calibrator_DoWork(object sender, DoWorkEventArgs e) {
      this.Context.FrameGrabber.Camera.Intrinsics = _ic.Calibrate();
      _ec = new Parsley.Core.ExtrinsicCalibration(Context.CalibrationPattern.ObjectPoints, Context.Camera.Intrinsics);
    }

    protected override void OnFrame(Parsley.Core.FrameGrabber fp, Emgu.CV.Image<Emgu.CV.Structure.Bgr, byte> img) {
      Core.CalibrationPattern pattern = this.Context.CalibrationPattern;
      Image<Gray, Byte> gray = img.Convert<Gray, Byte>();
      gray._EqualizeHist();
      pattern.FindPattern(gray);
      this.HandleTakeImageRequest();
      this.DrawCoordinateFrame(img);
      pattern.DrawPattern(img, pattern.ImagePoints, pattern.PatternFound);
    }

    void DrawCoordinateFrame(Emgu.CV.Image<Emgu.CV.Structure.Bgr, byte> img) {
      if (_ec != null && Context.CalibrationPattern.PatternFound && Context.Camera.HasIntrinsics) {
        Emgu.CV.ExtrinsicCameraParameters ecp = _ec.Calibrate(Context.CalibrationPattern.ImagePoints);
        Context.CalibrationPattern.DrawCoordinateFrame(img, ecp, Context.Camera.Intrinsics);
      }
    }

    void HandleTakeImageRequest() {
      if (_take_image_request) {
        if (Context.CalibrationPattern.PatternFound) {
          _ic.AddView(Context.CalibrationPattern.ImagePoints);
          this.Invoke((MethodInvoker)delegate {
            _lbl_info.Text = String.Format("You have successfully acquired {0} calibration image(s)", _ic.Views.Count);
            _btn_calibrate.Enabled = _ic.Views.Count > 2 && !_cb_auto_take.Checked;
          });
        }
      }
      _take_image_request = false;
    }

    private void _btn_take_image_Click(object sender, EventArgs e) {
      _take_image_request = true;
    }

    private void _btn_calibrate_Click(object sender, EventArgs e) {
      _take_image_request = false;
      _timer_auto.Enabled = false;
      _cb_auto_take.Enabled = false;
      _btn_take_image.Enabled = false;
      _btn_calibrate.Enabled = false;
      _bw_calibrator.RunWorkerAsync();
    }

    private void _cb_auto_take_CheckedChanged(object sender, EventArgs e) {
      _btn_take_image.Enabled = !_cb_auto_take.Checked;
      _btn_calibrate.Enabled = !_cb_auto_take.Checked && _ic.Views.Count > 2;
      if (_cb_auto_take.Checked) {
        _lbl_info.Text = "Auto-taking calibration images every three seconds.";
      }
      _timer_auto.Enabled = _cb_auto_take.Checked;
    }
  }
}
