/*
 * Parsley http://parsley.googlecode.com
 * Copyright (c) 2010, Christoph Heindl. All rights reserved.
 * Code license:	New BSD License
 */

#pragma once

#include <draw3d/node.h>

namespace osg {
  class MatrixTransform;
}

namespace Parsley {
  namespace Draw3D {    
   
    public ref class Transform : public NodeT<osg::MatrixTransform> {
    public:
      bool Add(Node ^node);
      property array<double,2>^ Matrix {
        array<double,2>^ get();
        void set(array<double,2>^ value);
      }
    };
    
  }
}