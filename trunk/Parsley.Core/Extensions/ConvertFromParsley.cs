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
  /// Extensions to convert from Parsley to other datatypes.
  /// </summary>
  public static class ConvertFromParsley {

    /// <summary>
    /// Convert MathNet.Numerics.LinearAlgebra.Vector to Emgu.CV.Structure.MCvPoint3D32f
    /// </summary>
    /// <param name="v">MathNet.Numerics.LinearAlgebra.Vector</param>
    /// <returns>Emgu.CV.Structure.MCvPoint3D32f<double></returns>
    public static Emgu.CV.Structure.MCvPoint3D32f ToEmguF(this MathNet.Numerics.LinearAlgebra.Vector v) {
      // Assume equal dimensionality
      return new Emgu.CV.Structure.MCvPoint3D32f((float)v[0], (float)v[1], (float)v[2]);
    }

    /// <summary>
    /// Convert MathNet.Numerics.LinearAlgebra.Vector to Emgu.CV.Structure.MCvPoint3D64f
    /// </summary>
    /// <param name="v">MathNet.Numerics.LinearAlgebra.Vector</param>
    /// <returns>Emgu.CV.Structure.MCvPoint3D64f<double></returns>
    public static Emgu.CV.Structure.MCvPoint3D64f ToEmgu(this MathNet.Numerics.LinearAlgebra.Vector v) {
      // Assume equal dimensionality
      return new Emgu.CV.Structure.MCvPoint3D64f(v[0], v[1], v[2]);
    }

    /// <summary>
    /// Convert MathNet.Numerics.LinearAlgebra.Matrix to Emgu.CV.Matrix
    /// </summary>
    /// <param name="m">MathNet.Numerics.LinearAlgebra.Matrix</param>
    /// <returns>Emgu.CV.Matrix<double></returns>
    public static Emgu.CV.Matrix<double> ToEmgu(this MathNet.Numerics.LinearAlgebra.Matrix m) {
      Emgu.CV.Matrix<double> res = new Emgu.CV.Matrix<double>(m.RowCount, m.ColumnCount);
      for (int r = 0; r < m.RowCount; ++r) {
        for (int c = 0; c < m.ColumnCount; ++c) {
          res[r, c] = m[r, c];
        }
      }
      return res;
    }

    /// <summary>
    /// Convert MathNet.Numerics.LinearAlgebra.Vector to double[]
    /// </summary>
    /// <param name="v">MathNet.Numerics.LinearAlgebra.Vector</param>
    /// <returns>double[]</returns>
    public static double[] ToInterop(this MathNet.Numerics.LinearAlgebra.Vector v) {
      return v.CopyToArray();
    }

    /// <summary>
    /// Convert MathNet.Numerics.LinearAlgebra.Matrix to double[,]
    /// </summary>
    /// <param name="v">MathNet.Numerics.LinearAlgebra.Matrix</param>
    /// <returns>double[,]</returns>
    public static double[,] ToInterop(this MathNet.Numerics.LinearAlgebra.Matrix m) {
      return m.CopyToArray();
    }


  }
}
