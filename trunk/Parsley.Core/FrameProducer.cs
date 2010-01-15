using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Parsley.Core {

  /// <summary>
  /// Base class for entities producing colored frames.
  /// </summary>
  /// <remarks>
  /// The OnFrame event provided by this class is fully thread-safe. That is, the state of 
  /// the delegates cannot be changed while firing an event and, likewise, no events can be fired
  /// when delegates register or unregister. 
  /// 
  /// This bears the risk of deadlocks since arbitrary code is called when firing an event.
  /// For a discussion see http://bit.ly/6Qi8jS
  /// </remarks>
  public class FrameProducer : SharedResource {
    public delegate void OnFrameHandler(FrameProducer fp, Emgu.CV.Image<Emgu.CV.Structure.Bgr, byte> img);
    private OnFrameHandler _fh;
    private object _lock_fh;

    /// <summary>
    /// Initialize
    /// </summary>
    public FrameProducer() {
      _lock_fh = new object();
      _fh = null;
    }

    /// <summary>
    /// Register callback.
    /// </summary>
    public event OnFrameHandler OnFrame {
      add {
        lock (_lock_fh) { _fh = _fh + value; }
      }
      remove {
        lock (_lock_fh) { _fh = _fh - value; }
      }
    }

    /// <summary>
    /// Register callback.
    /// </summary>
    public event OnFrameHandler OnFramePrepend {
      add {
        lock (_lock_fh) { _fh = value + _fh; }
      }
      remove {
        lock (_lock_fh) { _fh = _fh - value; }
      }
    }


    /// <summary>
    /// Fire OnFrame event
    /// </summary>
    /// <param name="img">produced image</param>
    protected void FireOnFrame(Emgu.CV.Image<Emgu.CV.Structure.Bgr, byte> img) {
      lock (_lock_fh) {
        if (_fh != null) {
          _fh(this, img);
        }
      }
    }
  }
}
