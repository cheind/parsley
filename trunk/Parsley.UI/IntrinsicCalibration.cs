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

namespace Parsley.UI {
  [Obsolete("Use the slide based approach from Parsley App instead.")]
  public partial class IntrinsicCalibration : Form {
    Core.Capture _camera;
    Core.CheckerBoard _cb;
    Core.IntrinsicCalibration _calib;
    Emgu.CV.IntrinsicCameraParameters _intrinsics;

    BackgroundWorker _bw;
    int _nr_frames;

    public IntrinsicCalibration(Core.Capture camera, int nr_frames) {
      InitializeComponent();
      _nr_frames = nr_frames;
      _camera = camera;
      _cb = new Parsley.Core.CheckerBoard(9, 6, 25.0f);
      _calib = new Parsley.Core.IntrinsicCalibration(_cb.ObjectPoints, _camera.Properties.ImageSize);
      _bw = new BackgroundWorker();
      _bw.DoWork += new DoWorkEventHandler(_bw_DoWork);
      _bw.WorkerReportsProgress = true;
      _bw.WorkerSupportsCancellation = true;
      _bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_bw_RunWorkerCompleted);
      _bw.ProgressChanged += new ProgressChangedEventHandler(_bw_ProgressChanged);
      _progress_bar.Minimum = 0;
      _progress_bar.Maximum = _nr_frames;
    }

    public Emgu.CV.IntrinsicCameraParameters Intrinsics {
      get { return _intrinsics; }
    }

    public Core.IntrinsicCalibration Calibration {
      get { return _calib; }
    }

    protected override void OnFormClosing(FormClosingEventArgs e) {
      if (_bw.IsBusy) {
        e.Cancel = true;
        ParsleyMessage.Show("Cannot Close Window", "Cannot close window while calibrating. Try again when calibration has finished.");
      }
      base.OnFormClosing(e);
    }

    void _bw_ProgressChanged(object sender, ProgressChangedEventArgs e) {
      _progress_bar.Value = e.ProgressPercentage;
      _progress_label.Text = String.Format("{0}/{1} Calibration Images Captured!", e.ProgressPercentage, _nr_frames);
    }

    void _bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
      if (e.Cancelled) {
        _calib.ClearViews();
        _progress_label.Text = "Calibration Cancelled";
        _button_start.Text = "Restart Calibration";
      } else {
        _progress_label.Text = "Calibration Completed";
        _button_start.Text = "Done!";
      }
      _progress_bar.Value = _progress_bar.Minimum;
      
    }

    void _bw_DoWork(object sender, DoWorkEventArgs e) {
      BackgroundWorker bw = sender as BackgroundWorker;
      DateTime last = DateTime.Now;
      Image<Bgr, Byte> img = null;
      while (!bw.CancellationPending && (_calib.Views.Count < _nr_frames)) {
        img = _camera.Frame();
        Image<Gray, Byte> gray = img.Convert<Gray, Byte>();
        gray._EqualizeHist();

        _cb.FindPattern(gray);
        DateTime now = DateTime.Now;
        if ((now - last).Seconds > 3) {
          last = now;
          if (_cb.PatternFound) {
            _calib.AddView(_cb.ImagePoints);
            bw.ReportProgress(_calib.Views.Count);
          }
        }
        _cb.DrawPattern(img, _cb.ImagePoints, _cb.PatternFound);
        _picture_box.Image = img.Resize(_picture_box.Width, _picture_box.Height, Emgu.CV.CvEnum.INTER.CV_INTER_NN);
      }
      if (bw.CancellationPending) {
        e.Cancel = true;
      } else {
        _intrinsics = _calib.Calibrate();
        Parsley.Core.ExtrinsicCalibration ex = new Parsley.Core.ExtrinsicCalibration(_calib.ObjectPoints, _intrinsics);
        Emgu.CV.ExtrinsicCameraParameters ecp = ex.Calibrate(_cb.ImagePoints);

        System.Drawing.PointF[] coords = Emgu.CV.CameraCalibration.ProjectPoints(
            new MCvPoint3D32f[] { 
              new MCvPoint3D32f(0, 0, 0),
              new MCvPoint3D32f(50, 0, 0),
              new MCvPoint3D32f(0, 50, 0)
            },
            ecp, _intrinsics);

        img.Draw(new LineSegment2DF(coords[0], coords[1]), new Bgr(System.Drawing.Color.Red), 3);
        img.Draw(new LineSegment2DF(coords[0], coords[2]), new Bgr(System.Drawing.Color.Green), 3);
        _picture_box.Image = img.Resize(_picture_box.Width, _picture_box.Height, Emgu.CV.CvEnum.INTER.CV_INTER_NN);
      }
    }

    private void _button_start_Click(object sender, EventArgs e) {
      if (!_bw.IsBusy) {
        if (_intrinsics != null) {
          this.Close();
        } else {
          _calib.ClearViews();
          _bw.RunWorkerAsync();
          _button_start.Text = "Cancel Calibration";
        }
      } else {
        _bw.CancelAsync();
        _button_start.Text = "Cancelling...";
      }
    }
  }
}
