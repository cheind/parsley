/*
 * Parsley http://parsley.googlecode.com
 * Copyright (c) 2010, Christoph Heindl. All rights reserved.
 * Code license:	New BSD License
 */

#include <draw3d/convert.h>

namespace Parsley {
  namespace Draw3D {

    osg::Matrixd convert(array<double,2> ^data) {
      osg::Matrixd m;
      for (unsigned i = 0; i < 4; ++i) {
        m(i,0) = data[i, 0];
        m(i,1) = data[i, 1];
        m(i,2) = data[i, 2];
        m(i,3) = data[i, 3];
      }
      return m;
    }

    osg::Matrixd convert(array<double,2> ^data, transposed t) {
      osg::Matrixd m;
      for (unsigned i = 0; i < 4; ++i) {
        m(i,0) = data[0, i];
        m(i,1) = data[1, i];
        m(i,2) = data[2, i];
        m(i,3) = data[3, i];
      }
      return m;
    }

    array<double,2> ^convert(const osg::Matrixd &m) {
      array<double,2> ^data = gcnew array<double, 2>(4,4);
      for (unsigned i = 0; i < 4; ++i) {
        data[i,0] = m(i, 0);
        data[i,1] = m(i, 1);
        data[i,2] = m(i, 2);
        data[i,3] = m(i, 3);
      }
      return data;
    }

    array<double,2> ^convert(const osg::Matrixd &m, transposed t) {
      array<double,2> ^data = gcnew array<double, 2>(4,4);
      for (unsigned i = 0; i < 4; ++i) {
        data[i,0] = m(0, 0);
        data[i,1] = m(1, i);
        data[i,2] = m(2, i);
        data[i,3] = m(3, i);
      }
      return data;
    }

    osg::Vec3 convert3(array<double, 1> ^data) {
      return osg::Vec3(data[0], data[1], data[2]);
    }

    osg::Vec4 convert4(array<double, 1> ^data) {
      return osg::Vec4(data[0], data[1], data[2], data[3]);
    }
  }
}