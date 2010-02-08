  using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;

namespace Parsley.Core {

  /// <summary>
  /// Load types at runtime and performs queries on types
  /// </summary>
  public class TypeManager {

    public TypeManager() {
      AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);
    }

    Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args) {
      return null;
      /*
      string assemblyPath = Environment.CurrentDirectory;
      string assemblyName = args.Name.Substring(0, args.Name.IndexOf(",")) + ".dll";
      string assemblyFullName = Path.Combine(assemblyPath, assemblyName);
      Assembly assembly = Assembly.Load(assemblyFullName);
      return assembly;
       */
    }

    /// <summary>
    /// Load assemblies from directory
    /// </summary>
    /// <param name="directory_path">Directory path</param>
    /// <param name="recursive">If true, recurses into sub-folders</param>
    /// <param name="update_path">If true, updates environment path veriable before loading assembly</param>
    public static void LoadAllFrom(string directory_path, bool recursive) {
      if (!Directory.Exists(directory_path))
        throw new ArgumentException(String.Format("Directory {0} does not exist", directory_path));

      foreach (string path in TypeManager.Files(directory_path, "*.dll", true)) {
        try {
          Assembly a = Assembly.LoadFrom(path);
        } catch (Exception) {}
      }
    }

    /// <summary>
    /// Find files
    /// </summary>
    /// <param name="directory_path">Start directory</param>
    /// <param name="file_pattern">File pattern</param>
    /// <param name="recursive">If true, sub-folders are searched</param>
    /// <returns>Found files</returns>
    public static IEnumerable<string> Files(string directory_path, string file_pattern, bool recursive) {
      foreach (string s in Directory.GetFiles(directory_path, file_pattern)) {
        yield return s;
      }
      if (recursive) {
        foreach (string d in SubDirectories(directory_path, false)) {
          foreach (string s in Files(d, file_pattern, true)) {
            yield return s;
          }
        }
      }
    }

    /// <summary>
    /// Find sub-directories
    /// </summary>
    /// <param name="directory_path">Start directory</param>
    /// <param name="recursive">If true, sub-directories are searched recursively.</param>
    /// <returns>Found sub-directories</returns>
    public static IEnumerable<string> SubDirectories(string directory_path, bool recursive) {
      foreach (string s in Directory.GetDirectories(directory_path)) {
        yield return s;
        if (recursive) {
          foreach(string s2 in SubDirectories(s, recursive))
            yield return s2;
        }
      }
    }


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
    /// Returns the first constructible type that is assignable to the given type.
    /// </summary>
    /// <remarks>Searches all loaded assemblies of the current domain.</remarks>
    /// <param name="type_of">Type to search for must be assignable to this type.</param>
    /// <returns>First found or default.</returns>
    public static Type FirstDefaultConstructibleType(Type type_of) {
      return AllDefaultConstructibleTypes(type_of).FirstOrDefault();
    }

    /// <summary>
    /// Test if a type is default constructible
    /// </summary>
    /// <param name="t">Type to test</param>
    /// <returns>True if default constructible, false otherwise</returns>
    private static bool IsDefaultConstrucible(Type t) {
      return
          t.IsAbstract == false
          && t.IsGenericTypeDefinition == false
          && t.IsInterface == false
          && t.GetConstructor(Type.EmptyTypes) != null;
    }
  }
}
