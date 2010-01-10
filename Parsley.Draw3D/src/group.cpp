

#include <draw3d/group.h>
#include <osg/Group>

namespace Parsley {
  namespace Draw3D {

    bool 
    Group::Add(Node ^node) { 
      return osg()->addChild(node->node().get());
    }
  }
}