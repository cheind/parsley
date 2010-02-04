  using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Parsley.Core {

  /// <summary>
  /// Load types at runtime and performs queries on types
  /// </summary>
  public class TypeManager {

    /// <summary>
    /// Return an enumeration of default constructable types that
    /// are assignable to the given type.
    /// </summary>
    /// <remarks>Searches all loaded assemblies of the current domain.</remarks>
    /// <param name="type_of">All types must be assignable to this type.</param>
    /// <returns>Return an enumeration of default constructable types.</returns>
    public static IEnumerable<Type> AllDefaultConstructibleTypes(Type type_of) {
      return AppDomain.CurrentDomain.GetAssemblies()
        .SelectMany(assembly => assembly.GetTypes())
        .Where(type => (type_of.IsAssignableFrom(type) && TypeManager.IsDefaultConstrucible(type)));
    }

    /// <summary>
    /// Test if a type is default constructible
    /// </summary>
    /// <param name="t">Type to test</param>
    /// <returns>True if default constructible, false otherwise</returns>
    private static bool IsDefaultConstrucible(Type t) {
      return t.IsAbstract == false
          && t.IsGenericTypeDefinition == false
          && t.IsInterface == false
          && t.GetConstructor(Type.EmptyTypes) != null;
    }
  }
}
