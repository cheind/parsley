// Parsley.Draw3D.h

#pragma once

#include <draw3d/node.h>

namespace osg {
  class Geode;
  class Geometry;
  class DrawElementsUInt;
}

namespace Parsley {
  namespace Draw3D {    

    public ref class PointCloud : public NodeT<osg::Geode> {
    public:
      PointCloud();

      /// Add point to point-cloud
      unsigned AddPoint(array<double>^ x, array<double>^ color);
      /// Update point that is already in point-cloud
      void UpdatePoint(unsigned id, array<double>^ x, array<double>^ color);
    private:
      auto_ptr_osg<osg::Geometry> _geometry;
      auto_ptr_osg<osg::Vec3Array> _vertices;
      auto_ptr_osg<osg::Vec4Array> _colors;
      auto_ptr_osg<osg::DrawElementsUInt> _primitives;
    };
  }
}