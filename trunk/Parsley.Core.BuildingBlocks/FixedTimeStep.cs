using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Parsley.Core.BuildingBlocks {

  /// <summary>
  /// Provides methods to control the cylce time.
  /// </summary>
  /// <remarks>If the cycle-time is not eaten up by code, FixedTimeStep will block the calling thread to
  /// stick with the cylce time. The wait time is calculated by taking the average calculation of the 
  /// last 50 invocations.</remarks>
  public class FixedTimeStep {
    private Stopwatch _sw;
    private double _fps;
    private long _cycle_time_ms;

    /// <summary>
    /// Construct new fixed time-step helper
    /// </summary>
    public FixedTimeStep(double fps) {
      _sw = new Stopwatch();
      this.FPS = fps;
    }

    /// <summary>
    /// Construct new fixed time-step helper
    /// </summary>
    public FixedTimeStep() : this(Double.MaxValue) {
    }

    /// <summary>
    /// Control the desired frame-rate (frames per second)
    /// </summary>
    public double FPS {
      get { return _fps; }
      set {
        if (value <= 0.0) {
          throw new ArgumentException("FPS must be greater than zero");
        }
        _fps = value;
        _cycle_time_ms = (long)((1.0 / _fps) * 1000);
      }
    }

    /// <summary>
    /// Fetch latest amount of time elapsed and wait
    /// to satisfy cycle time
    /// </summary>
    public void UpdateAndWait() {
      if (_sw.IsRunning) {
        _sw.Stop();
        long elapsed = _sw.ElapsedMilliseconds;
        long wait_time = _cycle_time_ms - elapsed;
        if (wait_time > 0) {
          System.Threading.Thread.Sleep((int)wait_time);
        }
        _sw.Reset();
      }
      _sw.Start();
    }
  }
}
