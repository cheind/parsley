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
    private Camera _camera;
    public delegate void OnFrameHandler(FrameGrabber fg, Image<Bgr, Byte> img);
    private OnFrameHandler _fh;  // Holds the delegate
    private object _lock_fh;

    public FrameGrabber(Camera camera) {
      _camera = camera;
      _bw = new BackgroundWorker();
      _bw.WorkerSupportsCancellation = true;
      _bw.DoWork += new DoWorkEventHandler(_bw_DoWork);
      _lock_fh = new object();
    }


    public event OnFrameHandler OnFrame {
      add {
        lock (_lock_fh) { _fh = _fh + value; }
      }
      remove {
        lock (_lock_fh) { _fh = _fh - value; }
      }
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
      this.RequestStop(); // Stop or request stop?
    }

    void _bw_DoWork(object sender, DoWorkEventArgs e) {
      BackgroundWorker bw = sender as BackgroundWorker;
      using (SharedResource.Breath b = _camera.KeepAlive()) {
        while (!bw.CancellationPending) {
          Image<Bgr, Byte> img = _camera.Frame();
          lock (_lock_fh) {
            if (_fh != null) {
              _fh(this, img);
            }
          }
        }
      }
      e.Cancel = true;
    }
  }
}
