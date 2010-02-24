/*
 * Parsley http://parsley.googlecode.com
 * Copyright (c) 2010, Christoph Heindl. All rights reserved.
 * Code license:	New BSD License
 */

#pragma once

#include <draw3d/node.h>


namespace osg {
  class Group;
}

namespace Parsley {
  namespace Draw3D {    
   
    public ref class Group : public NodeT<osg::Group> {
    public:
      bool Add(Node ^node);
      bool Remove(Node ^node);
    };
    
  }
}