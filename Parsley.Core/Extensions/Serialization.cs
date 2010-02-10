using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Parsley.Core.Extensions {
  public static class Serialization {

    public static T GetValue<T>(this SerializationInfo info, string name) {
      return (T)info.GetValue(name, typeof(T));
    }

  }
}
