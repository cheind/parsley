

#include <draw3d/pointcloud.h>
#include <draw3d/convert.h>
#include <osg/Geometry>
#include <osg/Geode>

namespace Parsley {
  namespace Draw3D {

    PointCloud::PointCloud() {
      auto_ptr_osg<osg::Geode> geode = this->osg();
      _geometry = new osg::Geometry();
      _vertices = new osg::Vec3Array();
      _primitives = new osg::DrawElementsUInt(osg::PrimitiveSet::POINTS, 0);

      _geometry->setVertexArray(_vertices.get());
      _geometry->addPrimitiveSet(_primitives.get());

      osg::ref_ptr<osg::Vec4Array> colors(new osg::Vec4Array);
      colors->push_back(osg::Vec4(0.7f, 0.7f, 0.7f, 1.0f));
      _geometry->setColorArray(colors.get());
      _geometry->setColorBinding(osg::Geometry::BIND_PER_PRIMITIVE_SET);

      _geometry->setUseDisplayList(false);
      geode->getOrCreateStateSet()->setMode(GL_LIGHTING, osg::StateAttribute::OFF);
      geode->addDrawable(_geometry.get());
      
    }

    void 
    PointCloud::AddPoint(cli::array<double,1> ^x) {
      _vertices->push_back(convert(x));
      _primitives->push_back(_primitives->size());
    }
  }
}