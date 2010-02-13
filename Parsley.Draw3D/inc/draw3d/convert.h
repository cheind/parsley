/*
 * Parsley http://parsley.googlecode.com
 * Copyright (c) 2010, Christoph Heindl. All rights reserved.
 * Code license:	New BSD License
 */

#pragma once

#include <osg/vec3>
#include <osg/vec4>
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
    osg::Vec3 convert3(array<double> ^data);

    /// Creates a four-dimensional vector from data
    osg::Vec4 convert4(array<double> ^data);
  }
}