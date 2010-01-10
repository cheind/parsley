// Parsley.Draw3D.h

#pragma once

using namespace Emgu::CV::Structure;

#include <draw3d/node.h>


namespace osg {
  class Geode;
}

namespace Parsley {
  namespace Draw3D {    

    public ref class Quad : public NodeT<osg::Geode> {
    public:
      Quad(double width, double height);
    };
  }
}