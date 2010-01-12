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
  /// <remarks>The public OnFire event is not explicitely guarded by a lock,
  /// because field-like events are generated with a lock(this). However, I'm not
  /// sure if that holds for firing events as well.
  /// http://msdn.microsoft.com/en-us/library/aa664455(VS.71).aspx
  /// </remarks>
  public class FrameGrabber : Resource {
    private BackgroundWorker _bw;
    private ManualResetEvent _stopped;
    private Camera _camera;


    public FrameGrabber(Camera camera) {
      _camera = camera;
      _bw = new BackgroundWorker();
      _bw.WorkerSupportsCancellation = true;
      _stopped = new ManualResetEvent(false);
      _bw.DoWork += new DoWorkEventHandler(_bw_DoWork);
    }

    public delegate void OnFrameHandler(FrameGrabber fg, Image<Bgr, Byte> img);
    public event OnFrameHandler OnFrame;

    public void Start()
    {
      if (!_bw.IsBusy) {
        _stopped.Reset();
        _bw.RunWorkerAsync();
      }
    }

    public void RequestStop() {
      _bw.CancelAsync();
    }

    public void Stop()
    {
      RequestStop();
      _stopped.WaitOne();
    }

    public Camera Camera
    {
      get { return _camera; }
    }

    protected override void DisposeManaged() {
      this.Stop(); // Stop producing
    }

    void _bw_DoWork(object sender, DoWorkEventArgs e) {
      BackgroundWorker bw = sender as BackgroundWorker;
      while (!bw.CancellationPending) {
        Image<Bgr, Byte> img = _camera.Frame();
        if (OnFrame != null) {
          OnFrame(this, img); // synchronize this?
        }
      }
      e.Cancel = true;
      _stopped.Set();
    }
  }
}
