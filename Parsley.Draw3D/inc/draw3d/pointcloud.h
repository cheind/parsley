/*
 * Parsley http://parsley.googlecode.com
 * Copyright (c) 2010, Christoph Heindl
 * Copyright (c) 2010, Matthias Plasch
 * All rights reserved.
 * Code license:	New BSD License
 */

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
      void UpdatePoint(unsigned id, array<double>^ x);
      /// Update point color
      void UpdateColor(unsigned id, array<double>^ color);
      /// Clear points
      void ClearPoints();
      // Save pointcloud as six-dimensional CSV
      void SaveAsCSV(System::String ^filename, System::String ^delimiter);
      // Returns point of pointcloud with the given index
      cli::array<double,1> ^PointCloud::ReturnPointAtIndex(unsigned id);
      // The full pointcloud is stored at the array memory location, specified by the pointer (reference)
      void PointCloud::PointCloudToArray(cli::array<float,1> ^ %pointArray);
      // The pointcloud range, specified by start/stop index is stored at the specified memory location
      unsigned PointCloud::PointCloudToArray(cli::array<float,1> ^ %pointArray, unsigned start, unsigned stop);
      // Returns the number of points, stored in the cloud.
      unsigned PointCloud::NumberOfPoints();
    private:
      auto_ptr_osg<osg::Geometry> _geometry;
      auto_ptr_osg<osg::Vec3Array> _vertices;
      auto_ptr_osg<osg::Vec4Array> _colors;
      auto_ptr_osg<osg::DrawElementsUInt> _primitives;
    };
  }
}