/*
 * Parsley http://parsley.googlecode.com
 * Copyright (c) 2010, Christoph Heindl. All rights reserved.
 * Code license:	New BSD License
 */

#include <draw3d/transform.h>
#include <draw3d/convert.h>
#include <osg/MatrixTransform>

namespace Parsley {
  namespace Draw3D {

    bool 
    Transform::Add(Node ^node) { 
      return osg()->addChild(node->node().get());
    }

    array<double,2>^ Transform::Matrix::get() {
      return convert(this->osg()->getMatrix(), transposed());
    }

    void Transform::Matrix::set(array<double,2>^ data) {
      this->osg()->setMatrix(convert(data, transposed()));
    }

  }
}