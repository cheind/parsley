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
  /// <remarks>
  /// The OnFrame event provided by this class is fully thread-safe. That is, the state of 
  /// the delegates cannot be changed while firing an event and, likewise, no events can be fired
  /// when delegates register or unregister. 
  /// 
  /// This bears the risk of deadlocks since arbitrary code is called when firing an event.
  /// For a discussion see http://bit.ly/6Qi8jS
  /// </remarks>
  public class FrameGrabber : Resource.SharedResource {
    private BackgroundWorker _bw;
    private Camera _camera;
    private FixedTimeStep _fts;

    /// <summary>
    /// On-Frame callback
    /// </summary>
    /// <param name="fp">frame grabber</param>
    /// <param name="img">image produced</param>
    public delegate void OnFrameHandler(FrameGrabber fp, Emgu.CV.Image<Emgu.CV.Structure.Bgr, byte> img);
    private OnFrameHandler _on_frame;
    private object _lock_event;

    /// <summary>
    /// Initialize frame grabber with camera
    /// </summary>
    /// <param name="camera"></param>
    public FrameGrabber(Camera camera) {
      _camera = camera;
      _bw = new BackgroundWorker();
      _bw.WorkerSupportsCancellation = true;
      _bw.DoWork += new DoWorkEventHandler(_bw_DoWork);
      _lock_event = new object();
      _fts = new FixedTimeStep(30);
    }

    /// <summary>
    /// Get and set desired frames per second
    /// </summary>
    public double FPS {
      get { return _fts.FPS; }
      set { _fts.FPS = value; }
    }

    /// <summary>
    /// Register callback.
    /// </summary>
    /// <remarks>Appends callback</remarks>
    public event OnFrameHandler OnFrame {
      add {
        lock (_lock_event) { _on_frame = _on_frame + value; }
      }
      remove {
        lock (_lock_event) { _on_frame = _on_frame - value; }
      }
    }

    /// <summary>
    /// Register callback.
    /// </summary>
    /// <remarks>Prepends callback</remarks>
    public event OnFrameHandler OnFramePrepend {
      add {
        lock (_lock_event) { _on_frame = value + _on_frame; }
      }
      remove {
        lock (_lock_event) { _on_frame = _on_frame - value; }
      }
    }

    /// <summary>
    /// Start frame grabbing asynchronously
    /// </summary>
    public void Start()
    {
      if (!_bw.IsBusy) {
        _bw.RunWorkerAsync();
      }
    }

    /// <summary>
    /// Request stop of frame grabbing.
    /// Will release breath on camera
    /// </summary>
    public void RequestStop() {
      _bw.CancelAsync();
    }

    /// <summary>
    /// Access the camera grabbed from
    /// </summary>
    public Camera Camera
    {
      get { return _camera; }
    }

    protected override void DisposeManaged() {
      this.RequestStop();
    }

    void _bw_DoWork(object sender, DoWorkEventArgs e) {
      BackgroundWorker bw = sender as BackgroundWorker;
      using (Resource.SharedResource.Breath b = _camera.KeepAlive()) {
        while (!bw.CancellationPending) {
          // Note: the image is disposed once the camera gets disposed.
          // Therefore you should copy the image if needed in another
          // thread or make sure the camera is not disposed.
          Image<Bgr, byte> img = _camera.Frame();
          lock (_lock_event) {
            if (_on_frame != null) _on_frame(this, img); 
          }
          img.Dispose();
          _fts.UpdateAndWait();
        }
      }
      e.Cancel = true;
    }
  }
}
