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
  /// Extensions to MathNet.Numerics.LinearAlgebra.Vector deal with homogeneous coordinates.
  /// </summary>
  public static class HomogeneousCoordinates {

    /// <summary>
    /// Convert vector to homogeneous coordinates by adding the given coordinate
    /// </summary>
    /// <param name="v">Vector to make homogeneous</param>
    /// <param name="w">Coordinate to add</param>
    /// <returns></returns>
    public static MathNet.Numerics.LinearAlgebra.Vector 
      ToHomogeneous(this MathNet.Numerics.LinearAlgebra.Vector v, double w) 
    {
      MathNet.Numerics.LinearAlgebra.Vector r = 
        new MathNet.Numerics.LinearAlgebra.Vector(v.Length + 1);
      for (int i = 0; i < v.Length; ++i) {
        r[i] = v[i];
      }
      r[r.Length - 1] = w;
      return r;
    }

    /// <summary>
    /// Drop the homogeneous coordinate
    /// </summary>
    /// <param name="v">Vector to make un-homogenize</param>
    /// <returns></returns>
    public static MathNet.Numerics.LinearAlgebra.Vector
      ToNonHomogeneous(this MathNet.Numerics.LinearAlgebra.Vector v) 
    {
      MathNet.Numerics.LinearAlgebra.Vector r = new MathNet.Numerics.LinearAlgebra.Vector(v.Length - 1);
      for (int i = 0; i < v.Length - 1; ++i) {
        r[i] = v[i];
      }
      return r;
    }
  }
}
