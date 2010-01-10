// Parsley.Draw3D.h

#pragma once

#include <osg/Vec3>

namespace Parsley {
  namespace Draw3D {    

    public ref class C {
    public:
      static osg::Vec3 c(Emgu::CV::Structure::MCvPoint3D32f ^p) {
        return osg::Vec3(p->x, p->y, p->z);
      }
    };
  }
}