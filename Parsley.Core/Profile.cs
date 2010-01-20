using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Parsley.Core {

  /// <summary>
  /// Quick profiling of scopes
  /// </summary>
  public class Profile : IDisposable {
    private string _name;
    private DateTime _start;

    public Profile(string name) {
      _name = name;
      _start = DateTime.Now;
    }

    public void Dispose() {
      Dispose(true);
      GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing) {
      if (disposing) {
        Console.WriteLine("Profile {0} took {1} seconds.", _name, (DateTime.Now - _start).TotalSeconds);
      }
    }
  }
}
