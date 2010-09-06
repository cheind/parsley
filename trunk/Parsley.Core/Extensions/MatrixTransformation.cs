using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Parsley.Core.Extensions
{
  public static class MatrixTransformation
  {
    public static IEnumerable<MathNet.Numerics.LinearAlgebra.Vector> TransformVectorToVector(MathNet.Numerics.LinearAlgebra.Matrix TMatrix, double w,
                                                                                      IEnumerable<MathNet.Numerics.LinearAlgebra.Vector> PointVector)
    {
      foreach(MathNet.Numerics.LinearAlgebra.Vector v in PointVector)
      {
        MathNet.Numerics.LinearAlgebra.Matrix colMatrix = new MathNet.Numerics.LinearAlgebra.Matrix(4, 1);
        colMatrix[3, 0] = w;
        colMatrix.SetMatrix(0, 2, 0, 0, v.ToColumnMatrix());

        yield return (TMatrix.Multiply(colMatrix)).GetColumnVector(0).ToNonHomogeneous();
      }
    }

    public static IEnumerable<Emgu.CV.Structure.MCvPoint3D32f> TransformVectorToEmgu(MathNet.Numerics.LinearAlgebra.Matrix TMatrix, double w,
                                                                                IEnumerable<MathNet.Numerics.LinearAlgebra.Vector> PointVector)
    {
      foreach (MathNet.Numerics.LinearAlgebra.Vector v in PointVector)
      {
        MathNet.Numerics.LinearAlgebra.Matrix colMatrix = new MathNet.Numerics.LinearAlgebra.Matrix(4, 1);
        colMatrix[3, 0] = w;
        colMatrix.SetMatrix(0, 2, 0, 0, v.ToColumnMatrix());

        yield return (TMatrix.Multiply(colMatrix)).GetColumnVector(0).ToNonHomogeneous().ToEmguF();
      }
    }
  }
}
