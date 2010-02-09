using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Parsley.Core.Addins {

  /// <summary>
  /// Info on an addin
  /// </summary>
  /// <remarks>Modelled after System.Addin</remarks>
  public class AddinInfo {
    private Type _type;

    /// <summary>
    /// Construct from assembly and type
    /// </summary>
    /// <param name="t"></param>
    public AddinInfo(Type t) {
      _type = t;
    }

    /// <summary>
    /// Get assembly that contains this addin
    /// </summary>
    public Assembly Assembly {
      get { return _type.Assembly; }
    }

    /// <summary>
    /// Get addin type
    /// </summary>
    public Type Type {
      get { return _type; }
    }

    /// <summary>
    /// Get addin's full name
    /// </summary>
    public string FullName {
      get { return _type.FullName; }
    }

    /// <summary>
    /// Report name of addin
    /// </summary>
    /// <returns>Addin name</returns>
    public override string ToString() {
      return this.FullName;
    }

    /// <summary>
    /// True if type is default constructible
    /// </summary>
    public bool DefaultConstructible {
      get {
        return
            _type.IsAbstract == false
            && _type.IsGenericTypeDefinition == false
            && _type.IsInterface == false
            && _type.GetConstructor(Type.EmptyTypes) != null;
      }
    }

    /// <summary>
    /// Test if addin is of given type
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
    public bool TypeOf(Type t) {
      return t.IsAssignableFrom(_type);
    }
  }
}
