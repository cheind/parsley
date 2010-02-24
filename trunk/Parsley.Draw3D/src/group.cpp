/*
 * Parsley http://parsley.googlecode.com
 * Copyright (c) 2010, Christoph Heindl. All rights reserved.
 * Code license:	New BSD License
 */

#include <draw3d/group.h>
#include <osg/Group>

namespace Parsley {
  namespace Draw3D {

    bool 
    Group::Add(Node ^node) { 
      return osg()->addChild(node->node().get());
    }

    bool
    Group::Remove(Node ^node) {
        return osg()->removeChild(node->node().get());
    }
  }
}