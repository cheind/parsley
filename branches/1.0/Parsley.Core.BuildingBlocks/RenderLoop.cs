/*
 * Parsley http://parsley.googlecode.com
 * Copyright (c) 2010, Christoph Heindl. All rights reserved.
 * Code license:	New BSD License
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Threading;

namespace Parsley.Core.BuildingBlocks {

  /// <summary>
  /// Asynchronous render loop.
  /// </summary>
  public class RenderLoop : Core.Resource {
    private Parsley.Draw3D.Viewer _v;
    private BackgroundWorker _bw;
    private FixedTimeStep _fts;
    public delegate void BeforeRenderHandler(RenderLoop r);
    public delegate void AfterRenderHandler(RenderLoop r);
    private BeforeRenderHandler _before_render;
    private AfterRenderHandler _after_render;
    private object _event_lock;
    private ManualResetEvent _stopped;


    /// <summary>
    /// Initialize from pre-existing viewer
    /// </summary>
    /// <param name="v">viewer</param>
    public RenderLoop(Parsley.Draw3D.Viewer v) {
      _v = v;
      _fts = new FixedTimeStep();
      _bw = new BackgroundWorker();
      _bw.WorkerSupportsCancellation = true;
      _bw.DoWork += new DoWorkEventHandler(_bw_DoWork);
      _event_lock = new object();
      _stopped = new ManualResetEvent(false);
    }

    /// <summary>
    /// Initialize with render target
    /// </summary>
    /// <param name="render_target">Target control to render to</param>
    public RenderLoop(System.Windows.Forms.Control render_target) 
      : this(new Parsley.Draw3D.Viewer(render_target)) 
    {}

    /// <summary>
    /// Control the time-step
    /// </summary>
    public double FPS {
      set { _fts.FPS = value; }
      get { return _fts.FPS; }
    }

    /// <summary>
    /// Register callback.
    /// </summary>
    public event BeforeRenderHandler BeforeRender {
      add {
        lock (_event_lock) { _before_render = _before_render + value; }
      }
      remove {
        lock (_event_lock) { _before_render = _before_render - value; }
      }
    }

    /// <summary>
    /// Register callback.
    /// </summary>
    public event AfterRenderHandler AfterRender {
      add {
        lock (_event_lock) { _after_render = _after_render + value; }
      }
      remove {
        lock (_event_lock) { _after_render = _after_render - value; }
      }
    }

    /// <summary>
    /// Start rendering
    /// </summary>
    public void Start() {
      if (!_bw.IsBusy) {
        _bw.RunWorkerAsync();
        _stopped.Reset();
      }
    }

    /// <summary>
    /// Request render stop
    /// </summary>
    public void RequestStop() {
      _bw.CancelAsync();
    }

    /// <summary>
    /// Stop and wait until stopped
    /// </summary>
    public void Stop() {
      _bw.CancelAsync();
      _stopped.WaitOne();
    }

    /// <summary>
    /// Get the viewer
    /// </summary>
    /// <remarks>When modified while rendering is performed, make sure to lock the viewer to
    /// prevent undesired effects. Locks from BeforeRender or AfterRender callbacks are in sync
    /// with the renderloop and need no synchronization as far as the viewer is concerned.</remarks>
    public Parsley.Draw3D.Viewer Viewer {
      get { return _v; }
    }

    protected override void DisposeManaged() {
      this.RequestStop();
    }

    void _bw_DoWork(object sender, DoWorkEventArgs e) {
      BackgroundWorker bw = sender as BackgroundWorker;
      while (!bw.CancellationPending) {
        lock (_event_lock) { if (_before_render != null) _before_render(this); }
        lock (_v) { _v.Frame(); }
        lock (_event_lock) { if (_after_render != null) _after_render(this); }
        _fts.UpdateAndWait();
      }
      e.Cancel = true;
      _stopped.Set();
    }

  }
}
