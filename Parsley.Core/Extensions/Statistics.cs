/*
 * Parsley http://parsley.googlecode.com
 * Copyright (c) 2010, Christoph Heindl. All rights reserved.
 * Code license:	New BSD License
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Parsley.Core.Extensions {

  /// <summary>
  /// Statistical extensions
  /// </summary>
  public static class Statistics {

    /// <summary>
    /// Calculate the median form a set of numbers
    /// </summary>
    /// <param name="source">Set of numbers</param>
    /// <returns>Median value</returns>
    public static double Median(this IEnumerable<double> source) {
      if (source.Count() == 0) {
        throw new InvalidOperationException("Cannot compute median for an empty set.");
      }

      var sortedList = from number in source
                       orderby number
                       select number;

      int itemIndex = (int)sortedList.Count() / 2;

      if (sortedList.Count() % 2 == 0) {
        // Even number of items.
        return (sortedList.ElementAt(itemIndex) + sortedList.ElementAt(itemIndex - 1)) / 2;
      } else {
        // Odd number of items.
        return sortedList.ElementAt(itemIndex);
      }
    }
  }
}
