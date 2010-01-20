// Parsley.Draw3D.h

#pragma once

#include <osg/vec3>
#include <osg/matrixd>

namespace Parsley {
  namespace Draw3D {    

    public ref class C {
    public:
      static osg::Vec3 c(Emgu::CV::Structure::MCvPoint3D32f ^p) {
        return osg::Vec3(p->x, p->y, p->z);
      }

      /*
      /// Creates a transposed matrix from two-dimensional array.
      /// Since osg has matrices in row-major form but Parsley using
      /// column major, the constructor copies the data transposed.
      /// see http://www.openscenegraph.org/projects/osg/wiki/Support/Maths/MatrixTransformations
      static osg::Matrixd c(array<double,2> ^data) {
        for (unsigned i = 0; i < 4; ++i) {
          _m(i,0) = data[0, i];
          _m(i,1) = data[1, i];
          _m(i,2) = data[2, i];
          _m(i,3) = data[3, i];
        }
      }
      */
    };

    struct transposed {};

    /// Creates a matrix from two-dimensional array.
    osg::Matrixd convert(array<double,2> ^data);

    /// Creates a transposed matrix from two-dimensional array.
    /// Since osg has matrices in row-major form but Parsley using
    /// column major, the constructor copies the data transposed.
    /// see http://www.openscenegraph.org/projects/osg/wiki/Support/Maths/MatrixTransformations
    osg::Matrixd convert(array<double,2> ^data, transposed t);

    /// Create a two-dimensional array from the matrix
    array<double,2> ^convert(const osg::Matrixd &m);

    /// Create a transposed two-dimensional array from the matrix
    array<double,2> ^convert(const osg::Matrixd &m, transposed t);

    /// Creates a three-dimensional vector from data
    osg::Vec3 convert(array<double> ^data);



  }
}