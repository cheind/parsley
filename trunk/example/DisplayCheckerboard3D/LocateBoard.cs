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
  public partial class LocateBoard : Form {

    private Parsley.Core.ExtrinsicCalibration _ex;
    private Parsley.Draw3D.Viewer _viewer;
    private Parsley.Core.Capture _capture;
    private Parsley.Core.CheckerBoard _cb;
    private BackgroundWorker _bw_3d;
    private BackgroundWorker _bw_cam;

    public LocateBoard(Parsley.Core.IntrinsicCalibration calib, Emgu.CV.IntrinsicCameraParameters intrinsics) {
      InitializeComponent();

      _ex = new Parsley.Core.ExtrinsicCalibration(calib.ObjectPoints, intrinsics);

      _viewer = new Parsley.Draw3D.Viewer(_render_target);
      _capture = Parsley.Core.Capture.FromCamera(0);
      _cb = new Parsley.Core.CheckerBoard(9, 6);

      _bw_3d = new BackgroundWorker();
      _bw_3d.WorkerSupportsCancellation = true;
      _bw_3d.DoWork += new DoWorkEventHandler(_bw_3d_DoWork);

      _bw_cam = new BackgroundWorker();
      _bw_cam.WorkerSupportsCancellation = true;
      _bw_cam.WorkerReportsProgress = true;
      _bw_cam.ProgressChanged += new ProgressChangedEventHandler(_bw_cam_ProgressChanged);
      _bw_cam.DoWork += new DoWorkEventHandler(_bw_cam_DoWork);

      _bw_3d.RunWorkerAsync();
      _bw_cam.RunWorkerAsync();
    }

    void _bw_cam_ProgressChanged(object sender, ProgressChangedEventArgs e) {
      _label_distance.Text = e.UserState as string;
    }

    void _bw_cam_DoWork(object sender, DoWorkEventArgs e) {
      BackgroundWorker bw = sender as BackgroundWorker;

      while (!bw.CancellationPending) {
        Image<Bgr, Byte> img = _capture.Frame();
        Image<Gray, Byte> gray = img.Convert<Gray, Byte>();
        gray._EqualizeHist();

        _cb.FindPattern(gray);
        if (_cb.PatternFound) {
          Emgu.CV.ExtrinsicCameraParameters ecp = _ex.Calibrate(_cb.ImageCorners);
          bw.ReportProgress(0, String.Format("Distance to Plate: {0}", ecp.TranslationVector.Norm));
        }
        _cb.Draw(img, 4, 2);
        _picture_box.Image = img.Resize(_picture_box.Width, _picture_box.Height, Emgu.CV.CvEnum.INTER.CV_INTER_NN);        
      }
      if (bw.CancellationPending) {
        e.Cancel = true;
      }
    }

    void _bw_3d_DoWork(object sender, DoWorkEventArgs e) {
      BackgroundWorker bw = sender as BackgroundWorker;
      while (!bw.CancellationPending) {
        System.Threading.Monitor.Enter(_viewer);
        _viewer.Frame();
        System.Threading.Monitor.Exit(_viewer);
      }
      if (bw.CancellationPending) {
        e.Cancel = true;
      }
    }

    protected override void OnFormClosing(FormClosingEventArgs e) {
      if (_bw_3d.IsBusy) {
        _bw_3d.CancelAsync();
        e.Cancel = true;
      }
      if (_bw_cam.IsBusy) {
        _bw_cam.CancelAsync();
        e.Cancel = true;
      }

      if (!e.Cancel) {
        _capture.Dispose();
        _viewer.Dispose();
      } else {
        Parsley.UI.ParsleyMessage.Show(this, "Cannot Close Window", "Your closing request was received. Try again in a moment.");
      }
      base.OnFormClosing(e);
    }
  }
}
