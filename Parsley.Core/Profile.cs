using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Parsley.Core {

  /// <summary>
  /// Quick profiling of scopes
  /// </summary>
  public class Profile : IDisposable {
    private string _name;
    private Stopwatch _sw;
    

    public Profile(string name) {
      _name = name;
      _sw = new Stopwatch();
      _sw.Start();
    }

    public void Dispose() {
      Dispose(true);
      GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing) {
      _sw.Stop();
      if (disposing) {
        Console.WriteLine("Profile {0} took {1} seconds.", _name, _sw.Elapsed.TotalSeconds);
      }
    }
  }
}
