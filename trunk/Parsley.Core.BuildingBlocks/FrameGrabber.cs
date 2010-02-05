using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

using Emgu.CV;
using Emgu.CV.Structure;
using System.Threading;

namespace Parsley.Core.BuildingBlocks {

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
    private bool _pending_start;

    /// <summary>
    /// On-Frame callback
    /// </summary>
    /// <param name="fp">frame grabber</param>
    /// <param name="img">image produced</param>
    public delegate void OnFrameHandler(FrameGrabber fp, Emgu.CV.Image<Emgu.CV.Structure.Bgr, byte> img);
    private OnFrameHandler _on_frame;
    private object _lock_event;
    private ManualResetEvent _stopped;

    /// <summary>
    /// Initialize frame grabber with camera
    /// </summary>
    /// <param name="camera"></param>
    public FrameGrabber(Camera camera) {
      _camera = camera;
      _lock_event = new object();
      _fts = new FixedTimeStep(30);
      _bw = new BackgroundWorker();
      _bw.WorkerSupportsCancellation = true;
      _bw.DoWork += new DoWorkEventHandler(_bw_DoWork);
      _bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_bw_RunWorkerCompleted);
      _stopped = new ManualResetEvent(false);
    }

    void _bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
      if (_pending_start) {
        _pending_start = false;
        this.Start();
      }
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
        _stopped.Reset();
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

    public void Stop() {
      _bw.CancelAsync();
      _stopped.WaitOne();
    }

    /// <summary>
    /// Get/Set the camera grabbed from
    /// </summary>
    public Camera Camera
    {
      get { return _camera; }
      set {
        bool busy = _bw.IsBusy;
        if (busy) {
          this.Stop();
        }
        _camera = value;
        if (busy) {
          _pending_start = true;
        }
      }
    }

    protected override void DisposeManaged() {
      this.RequestStop();
    }

    void _bw_DoWork(object sender, DoWorkEventArgs e) {
      BackgroundWorker bw = sender as BackgroundWorker;
      Camera camera = _camera;
      using (Resource.SharedResource.Breath b = camera.KeepAlive()) {
        // Note: the image is disposed once the camera gets disposed.
        // Therefore you should copy the image if needed in another
        // thread or make sure the camera is not disposed.
        Image<Bgr, byte> img;
        while (!bw.CancellationPending) {
          img = camera.Frame();
          if (img != null) {
            lock (_lock_event) {
              if (_on_frame != null) _on_frame(this, img);
            }
            img.Dispose();
          }
          _fts.UpdateAndWait();
        }
      }
      e.Cancel = true;
      _stopped.Set();
    }
  }
}
