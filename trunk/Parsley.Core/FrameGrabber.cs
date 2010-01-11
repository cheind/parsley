using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

using Emgu.CV;
using Emgu.CV.Structure;
using System.Threading;

namespace Parsley.Core {

  /// <summary>
  /// Grabs continously frames from camera
  /// </summary>
  public class FrameGrabber {
    private BackgroundWorker _worker;
    private Camera _camera;


    public FrameGrabber(Camera camera) {
      _camera = camera;
      _worker = new BackgroundWorker();
      _worker.WorkerReportsProgress = true;
      _worker.WorkerSupportsCancellation = true;
      _worker.DoWork +=new DoWorkEventHandler(_worker_DoWork);
    }

    void _worker_DoWork(object sender, DoWorkEventArgs e)
    {
      BackgroundWorker bw = sender as BackgroundWorker;
      while (!bw.CancellationPending) {
        Image<Bgr, Byte> img = _camera.Frame();
        // Assumes at least one listener
        OnFrame(this, img);
      }
      e.Cancel = true;
    }

    public delegate void OnFrameHandler(FrameGrabber fg, Image<Bgr, Byte> img);
    public event OnFrameHandler OnFrame;

    public void Start() {
      if (OnFrame != null && !_worker.IsBusy) {
        _worker.RunWorkerAsync();
      }
    }

    public void Stop() {
      if (_worker.IsBusy) {
        _worker.CancelAsync();
      }
    }

    public Camera Camera {
      get { return _camera; }
    }
  }
}
