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

  public static class ConvertToParsley {

    /// <summary>
    /// Convert MCvPoint3D32f to MathNet.Numerics.LinearAlgebra.Vector
    /// </summary>
    /// <param name="p">MCvPoint3D32f to convert</param>
    /// <returns>MathNet.Numerics.LinearAlgebra.Vector</returns>
    public static MathNet.Numerics.LinearAlgebra.Vector ToParsley(this Emgu.CV.Structure.MCvPoint3D32f p) {
      MathNet.Numerics.LinearAlgebra.Vector v = new MathNet.Numerics.LinearAlgebra.Vector(3);
      v[0] = p.x;
      v[1] = p.y;
      v[2] = p.z;
      return v;
    }

    /// <summary>
    /// Convert System.Drawing.PointF to MathNet.Numerics.LinearAlgebra.Vector
    /// </summary>
    /// <param name="p">System.Drawing.PointF to convert</param>
    /// <returns>MathNet.Numerics.LinearAlgebra.Vector</returns>
    public static MathNet.Numerics.LinearAlgebra.Vector ToParsley(this System.Drawing.PointF p) {
      MathNet.Numerics.LinearAlgebra.Vector v = new MathNet.Numerics.LinearAlgebra.Vector(2);
      v[0] = p.X;
      v[1] = p.Y;
      return v;
    }

    /// <summary>
    /// Convert System.Drawing.Point to MathNet.Numerics.LinearAlgebra.Vector
    /// </summary>
    /// <param name="p">System.Drawing.Point to convert</param>
    /// <returns>MathNet.Numerics.LinearAlgebra.Vector</returns>
    public static MathNet.Numerics.LinearAlgebra.Vector ToParsley(this System.Drawing.Point p) {
      MathNet.Numerics.LinearAlgebra.Vector v = new MathNet.Numerics.LinearAlgebra.Vector(2);
      v[0] = p.X;
      v[1] = p.Y;
      return v;
    }

    /// <summary>
    /// Convert Emgu.CV.Matrix to MathNet.Numerics.LinearAlgebra.Matrix
    /// </summary>
    /// <param name="m"> Emgu.CV.Matrix</param>
    /// <returns>MathNet.Numerics.LinearAlgebra.Matrix</returns>
    public static MathNet.Numerics.LinearAlgebra.Matrix ToParsley(this Emgu.CV.Matrix<double> m) {
      return MathNet.Numerics.LinearAlgebra.Matrix.Create(m.Data);
    }

  }
}
