using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Parsley.Core.Addins {

  /// <summary>
  /// Flags addins
  /// </summary>
  [AttributeUsage(AttributeTargets.Class)]
  public class AddinAttribute : System.Attribute {
    public AddinAttribute() {}
  }
}
