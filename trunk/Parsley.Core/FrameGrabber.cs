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
  /// Grabs continously frames from camera asynchronously.
  /// </summary>
  public class FrameGrabber : FrameProducer {
    private BackgroundWorker _bw;
    private Camera _camera;

    public FrameGrabber(Camera camera) {
      _camera = camera;
      _bw = new BackgroundWorker();
      _bw.WorkerSupportsCancellation = true;
      _bw.DoWork += new DoWorkEventHandler(_bw_DoWork);
    }

    public void Start()
    {
      if (!_bw.IsBusy) {
        _bw.RunWorkerAsync();
      }
    }

    public void RequestStop() {
      _bw.CancelAsync();
    }

    public Camera Camera
    {
      get { return _camera; }
    }

    protected override void DisposeManaged() {
      this.RequestStop();
    }

    void _bw_DoWork(object sender, DoWorkEventArgs e) {
      BackgroundWorker bw = sender as BackgroundWorker;
      using (SharedResource.Breath b = _camera.KeepAlive()) {
        while (!bw.CancellationPending) {
          // Note: the image is disposed once the camera gets disposed.
          // Therefore you should copy the image if needed in another
          // thread or make sure the camera is not disposed.
          Image<Bgr, byte> img = _camera.Frame();
          this.FireOnFrame(img);
          img.Dispose();
        }
      }
      e.Cancel = true;
    }
  }
}
