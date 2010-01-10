

#include <draw3d/quad.h>
#include <osg/Geometry>
#include <osg/Geode>

namespace Parsley {
  namespace Draw3D {

    Quad::Quad(double width, double height) {
      auto_ptr_osg<osg::Geode> geode = this->osg();
      osg::ref_ptr<osg::Geometry> quad(new osg::Geometry());

      osg::ref_ptr<osg::Vec3Array> vertices(new osg::Vec3Array());
      vertices->push_back(osg::Vec3(0, 0, 0));
      vertices->push_back(osg::Vec3(width, 0, 0));
      vertices->push_back(osg::Vec3(width, height, 0));
      vertices->push_back(osg::Vec3(0, height, 0));
      quad->setVertexArray(vertices.get());

      osg::ref_ptr<osg::DrawElementsUInt> q(new osg::DrawElementsUInt(osg::PrimitiveSet::QUADS, 0));
      q->push_back(3); q->push_back(2); q->push_back(1); q->push_back(0);
      quad->addPrimitiveSet(q);

      osg::ref_ptr<osg::Vec4Array> colors(new osg::Vec4Array);
      colors->push_back(osg::Vec4(0.0, 1.0, 0.0, 1.0));
      quad->setColorArray(colors.get());
      quad->setColorBinding(osg::Geometry::BIND_OVERALL);

      geode->getOrCreateStateSet()->setMode(GL_LIGHTING, osg::StateAttribute::OFF);
      geode->addDrawable(quad.get());
    }
  }
}