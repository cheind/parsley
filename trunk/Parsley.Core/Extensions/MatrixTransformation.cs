using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Parsley.Core.Extensions
{
  /// <summary>
  /// The function members, provided by MatrixTransformation perform a simple coordinate transformation.
  /// 3D-Points which are passed to the function (parameter PointVector) are transformed with
  /// respect to the transformation matrix TMatrix. The translation Vector, contained in the
  /// transformation matrix is beeing scaled with the factor w.
  /// 
  /// Both functions return an object of type "IEnumerable" which determines an iterator interface type.
  /// The yield keyword is used inside an iterator block (foreach) together with "return", in order to
  /// provide a new value to the enumerator object. 
  /// </summary>
  public static class MatrixTransformation
  {

    /// <summary>
    /// Transforms the given 3D vector point using the given transformation matrix.
    /// </summary>
    /// <param name="TMatrix"> The 4x4 transformation matrix containing rotational and translational matrix. </param>
    /// <param name="w"> Determines the scaling parameter for the translation: 0... neglect translation; 1... include translation. </param>
    /// <param name="PointVector"> The IEnumerable object of type Vector (e.g. List, array,... of 3D point vectors). </param>
    /// <returns> Transformed Points as IEnumerable of type Vector. </returns>
    public static IEnumerable<MathNet.Numerics.LinearAlgebra.Vector> TransformVectorToVector(MathNet.Numerics.LinearAlgebra.Matrix TMatrix, double w,
                                                                                      IEnumerable<MathNet.Numerics.LinearAlgebra.Vector> PointVector)
    {
      foreach(MathNet.Numerics.LinearAlgebra.Vector v in PointVector)
      {
        //convert vector to a column matrix
        MathNet.Numerics.LinearAlgebra.Matrix colMatrix = new MathNet.Numerics.LinearAlgebra.Matrix(4, 1);
        colMatrix[3, 0] = w;
        colMatrix.SetMatrix(0, 2, 0, 0, v.ToColumnMatrix());

        //perform transformation of the point - column vector is extracted after the multiplication
        yield return (TMatrix.Multiply(colMatrix)).GetColumnVector(0).ToNonHomogeneous();
      }
    }

    /// <summary>
    /// Transforms the given 3D vector point using the given transformation matrix.
    /// </summary>
    /// <param name="TMatrix"> The 4x4 transformation matrix containing rotational and translational matrix. </param>
    /// <param name="w"> Determines the scaling parameter for the translation: 0... neglect translation; 1... include translation.</param>
    /// <param name="PointVector"> The IEnumerable object of type Vector (e.g. List, array,... of 3D point vectors). </param>
    /// <returns> Transformed Points as IEnumerable of Emgu type MCvPoint3D32f. </returns>
    public static IEnumerable<Emgu.CV.Structure.MCvPoint3D32f> TransformVectorToEmgu(MathNet.Numerics.LinearAlgebra.Matrix TMatrix, double w,
                                                                                IEnumerable<MathNet.Numerics.LinearAlgebra.Vector> PointVector)
    {
      foreach (MathNet.Numerics.LinearAlgebra.Vector v in PointVector)
      {
        //convert vector to a column matrix
        MathNet.Numerics.LinearAlgebra.Matrix colMatrix = new MathNet.Numerics.LinearAlgebra.Matrix(4, 1);
        colMatrix[3, 0] = w;
        colMatrix.SetMatrix(0, 2, 0, 0, v.ToColumnMatrix());

        //perform transformation of the point - column vector is extracted after the multiplication
        //the resulting vector is converted to the Emgu type MCvPoint3D32f using ToEmguF().
        yield return (TMatrix.Multiply(colMatrix)).GetColumnVector(0).ToNonHomogeneous().ToEmguF();
      }
    }
  }
}
