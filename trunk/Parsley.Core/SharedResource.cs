using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Parsley.Core {

  /// <summary>
  /// A shared resource behaves just like a Resource with additional
  /// synchronization mechanisms on disposing that resource.
  /// </summary>
  public class SharedResource : Resource {
    private int _keep_alive;
    private object _lock_keep_alive;
    private bool _pending_dispose;
    private ManualResetEvent _can_dispose;

    public class Breath : IDisposable {
      private SharedResource _resource;

      internal Breath() {
        _resource = null;
      }

      internal Breath(SharedResource resource) {
        // Note: Keep-alive counter is incremented at SharedResource
        // for synchronization safety.
        _resource = resource;
      }

      public bool IsBreathing {
        get { return _resource != null; }
      }

      public void Dispose() {
        _resource.DecKeepAlive();
      }
    };

    public SharedResource() {
      _keep_alive = 0;
      _lock_keep_alive = new object();
      _pending_dispose = false;
      _can_dispose = new ManualResetEvent(true);
    }

    public Breath KeepAlive() {
      if (this.IncKeepAlive()) {
        return new Breath(this);
      } else {
        return new Breath();
      }
    }

    internal bool IncKeepAlive() {
      bool ret = false;
      lock (_lock_keep_alive) {
        if (!_pending_dispose) {
          _keep_alive += 1;
          _can_dispose.Reset();
          ret = true;
        }
      }
      return ret;
    }

    internal void DecKeepAlive() {
      lock (_lock_keep_alive) {
        _keep_alive -= 1;
        if (_keep_alive == 0) {
          _can_dispose.Set();
        }
      }
    }

    protected override void Dispose(bool disposing) {
      lock (_lock_keep_alive) {
        _pending_dispose = true;
      }
      _can_dispose.WaitOne();
      base.Dispose(disposing);
    }

  }
}
