using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Parsley.Core {

  /// <summary>
  /// Associative container where items are indexed through a string.
  /// </summary>
  public class Bundle {
    private Dictionary<string, object> _map;

    /// <summary>
    /// Construct new bundle.
    /// </summary>
    public Bundle() {
      _map = new Dictionary<string, object>();
    }

    /// <summary>
    /// Fetch item from bundle
    /// </summary>
    /// <typeparam name="T">Type to convert item to</typeparam>
    /// <param name="key">Key to look for item</param>
    /// <returns>Item</returns>
    public T Fetch<T>(string key) {
      return (T)_map[key];
    }

    /// <summary>
    /// Store item in bundle
    /// </summary>
    /// <param name="key">Key to store item at</param>
    /// <param name="item">Item to store</param>
    public void Store(string key, object item) {
      _map[key] = item;
    }
  }
}
