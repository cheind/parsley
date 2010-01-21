// Parsley.Draw3D.h

#pragma once

#include <osg/vec3>
#include <osg/matrixd>

namespace Parsley {
  namespace Draw3D {    

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