using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using log4net;
using System.Reflection;

namespace Parsley.Core.Addins {
  public static class AddinStore {
    private static List<AddinInfo> _addins = new List<AddinInfo>();
    private static readonly ILog _logger = LogManager.GetLogger(typeof(AddinStore));

    /// <summary>
    /// Discover addins from the directory path
    /// </summary>
    /// <param name="directory_path">Directory path</param>
    /// <param name="recursive"></param>
    public static void Discover(string directory_path) {
      List<string> warnings = new List<string>();
      foreach (string file in Directory.GetFiles(directory_path, "*.dll")) {
        DiscoverTypes(file);
      }
    }

    /// <summary>
    /// Find addins that are assignable to the provided type.
    /// </summary>
    /// <param name="type_of">Type</param>
    /// <returns>Enumeration of addin infos</returns>
    public static IEnumerable<AddinInfo> FindAddins(Type type_of) {
      return _addins.Select(ai => ai).Where(ai => ai.TypeOf(type_of));
    }

    /// <summary>
    /// Find addins that are assignable to the provided type.
    /// </summary>
    /// <param name="type_of">Type</param>
    /// <returns>Enumeration of addin infos</returns>
    public static IEnumerable<AddinInfo> FindAddins(Type type_of, Func<AddinInfo, bool> predicate) {
      return _addins.Select(ai => ai).Where(ai => ai.TypeOf(type_of) && predicate(ai));
    }

    /// <summary>
    /// Create default constructed instance of addin
    /// </summary>
    /// <param name="ai"></param>
    /// <returns></returns>
    public static object CreateInstance(AddinInfo ai) {
      return Activator.CreateInstance(ai.Type);
    }

    /// <summary>
    /// Discover exported types in assembly
    /// </summary>
    /// <param name="assembly_path">Path to assembly</param>
    private static void DiscoverTypes(string assembly_path) {
      try {
        Assembly a = Assembly.LoadFrom(assembly_path);
        foreach (Type t in a.GetExportedTypes()) {
          _addins.Add(new AddinInfo(t));
        }
        _logger.Info(String.Format("'{0}' successfully loaded.", assembly_path));
      } catch (System.BadImageFormatException) {
        _logger.Warn(String.Format("'{0}' is not a valid assembly.", assembly_path));
      } catch (System.IO.FileLoadException) {
        _logger.Info(String.Format("'{0}' already loaded.", assembly_path));
      }
    }
  }
}
