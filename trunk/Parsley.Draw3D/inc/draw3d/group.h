// Parsley.Draw3D.h

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
    };
    
  }
}