// Parsley.Draw3D.h

#pragma once

using namespace Emgu::CV::Structure;

#include <draw3d/node.h>


namespace osg {
  class MatrixTransform;
}

namespace Parsley {
  namespace Draw3D {    
   
    public ref class Transform : public NodeT<osg::MatrixTransform> {
    public:
      bool Add(Node ^node);
      property Emgu::CV::Matrix<double> ^Matrix {
        Emgu::CV::Matrix<double> ^get();
        void set(Emgu::CV::Matrix<double> ^value);
      }
    };
    
  }
}