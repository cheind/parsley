/*
 * Parsley http://parsley.googlecode.com
 * Copyright (c) 2010, Christoph Heindl. All rights reserved.
 * Code license:	New BSD License
 */

#include <draw3d/axis.h>
#include <osg/Geometry>
#include <osg/Geode>

namespace Parsley {
  namespace Draw3D {

    Axis::Axis(double extension) {
      auto_ptr_osg<osg::Geode> geode = this->osg();
      osg::ref_ptr<osg::Geometry> axis(new osg::Geometry());

      osg::ref_ptr<osg::Vec3Array> vertices(new osg::Vec3Array());
      vertices->push_back(osg::Vec3(0, 0, 0));
      vertices->push_back(osg::Vec3(extension, 0, 0));
      vertices->push_back(osg::Vec3(0, extension, 0));
      vertices->push_back(osg::Vec3(0, 0, extension));
      axis->setVertexArray(vertices.get());

      osg::ref_ptr<osg::DrawElementsUInt> lines(new osg::DrawElementsUInt(osg::PrimitiveSet::LINES, 0));
      lines->push_back(0); lines->push_back(1);
      lines->push_back(0); lines->push_back(2);
      lines->push_back(0); lines->push_back(3);
      axis->addPrimitiveSet(lines.get());

      osg::ref_ptr<osg::Vec4Array> colors(new osg::Vec4Array);
      colors->push_back(osg::Vec4(1.0, 0.0, 0.0, 1.0));
      colors->push_back(osg::Vec4(0.0, 1.0, 0.0, 1.0));
      colors->push_back(osg::Vec4(0.0, 0.0, 1.0, 1.0));
      axis->setColorArray(colors.get());
      axis->setColorBinding(osg::Geometry::BIND_PER_PRIMITIVE);

      geode->getOrCreateStateSet()->setMode(GL_LIGHTING, osg::StateAttribute::OFF);
      geode->addDrawable(axis.get());
    }
  }
}