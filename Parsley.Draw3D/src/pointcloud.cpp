/*
 * Parsley http://parsley.googlecode.com
 * Copyright (c) 2010, Christoph Heindl
 * Copyright (c) 2010, Matthias Plasch
 * All rights reserved.
 * Code license:	New BSD License
 */

#include <fstream>
#include <iostream>
#include <draw3d/pointcloud.h>
#include <draw3d/convert.h>
#include <osg/Geometry>
#include <osg/Geode>
#include <draw3d/marshal.h>

namespace Parsley {
  namespace Draw3D {

    PointCloud::PointCloud() {
      auto_ptr_osg<osg::Geode> geode = this->osg();
      _geometry = new osg::Geometry();
      _vertices = new osg::Vec3Array();
      _primitives = new osg::DrawElementsUInt(osg::PrimitiveSet::POINTS, 0);

      _geometry->setVertexArray(_vertices.get());
      _geometry->addPrimitiveSet(_primitives.get());

      _colors = new osg::Vec4Array();
      _geometry->setColorArray(_colors.get());
      _geometry->setColorBinding(osg::Geometry::BIND_PER_VERTEX);

      // Note: using _geometry->dirtyDisplayList on vertex modifications leads
      // to flickering. In the future we should switch to display lists when scanning
      // is complete, or no points have been added for a while.
      _geometry->setUseDisplayList(false);

      
      geode->getOrCreateStateSet()->setMode(GL_LIGHTING, osg::StateAttribute::OFF);
      geode->addDrawable(_geometry.get());
      
    }

    unsigned  
      PointCloud::AddPoint(cli::array<double,1> ^x, cli::array<double,1> ^color) 
    {
      unsigned id = _vertices->size();
      _colors->push_back(convert4(color));
      _vertices->push_back(convert3(x));
      _primitives->push_back(_primitives->size());
      return id;
    }

    void
      PointCloud::UpdatePoint(unsigned id, array<double>^ x)
    {
      _vertices->at(id) = convert3(x);
    }

    void
      PointCloud::UpdateColor(unsigned id, array<double>^ color)
    {
      _colors->at(id) = convert4(color);
    }

    void
      PointCloud::ClearPoints() 
    {
      _vertices->clear();
      _colors->clear();
      _primitives->clear();
    }

    void PointCloud::SaveAsCSV(System::String ^filename, System::String ^delimiter)
    {
      std::ofstream f_target;

      std::wstring myfilename;
      marshal(filename, myfilename);
      
      std::string mydelimiter;
      marshal(delimiter, mydelimiter);
      
      f_target.open(myfilename.c_str());
      if (f_target) {
        for(unsigned i = 0; i < _vertices->size(); i++) {
          f_target << _vertices->at(i).x() << mydelimiter
                   << _vertices->at(i).y() << mydelimiter
                   << _vertices->at(i).z() << mydelimiter
                   << _colors->at(i).x() << mydelimiter
                   << _colors->at(i).y() << mydelimiter
                   << _colors->at(i).z() << std::endl;
        }
      }
      f_target.close();
    }

    /*
    * Returns point at given pointcloud index in a 3x1 array (x,y,z)
    */
    cli::array<double,1> ^PointCloud::ReturnPointAtIndex(unsigned id)
    {
      cli::array<double,1> ^point_data = gcnew array<double, 1>(3);
      if(id < _vertices->size())
      {
        point_data[0] = _vertices->at(id).x();
        point_data[1] = _vertices->at(id).y();
        point_data[2] = _vertices->at(id).z();
      }
      return point_data;
    }

    unsigned PointCloud::NumberOfPoints()
    {
      return _vertices->size();
    }

    /*
    * Stores the points of the pointcloud in the given memory space (parameter pointArray).
    * The coordinate values x,y,z of one point are stored consecutively in the array memory.
    */
    void PointCloud::PointCloudToArray(cli::array<float,1> ^ %pointArray)
    {
      unsigned size = _vertices->size();
      unsigned id = 0, id_array = 0;

      //iterate the pointcloud _vertices
      while(id < size && id_array < (size * 3))
      {
        pointArray[id_array] = (float) _vertices->at(id).x();
        pointArray[id_array + 1] = (float) _vertices->at(id).y();
        pointArray[id_array + 2] = (float) _vertices->at(id).z();

        //array index for pointArray must be incremented by 3 since 3 float values (x,y,z) will be copied per point
        id_array = id_array + 3;
        id = id + 1;
      }
    }

    /*
    * Stores the range of points of the pointcloud (given by the indices start and stop) in the given memory space (parameter pointArray).
    * The coordinate values x,y,z of one point are stored consecutively in the array memory.
    * The number of copied points is returned.
    */
    unsigned PointCloud::PointCloudToArray(cli::array<float,1> ^ %pointArray, unsigned start, unsigned stop)
    {
      //set _vertices index to start index value
      unsigned size = _vertices->size();
      unsigned id = start, id_array = 0;
      unsigned range_size = 0;

      // if the range indices refer to a valid range..
      if(start <= stop && stop < size)
      {
        //calculate range size
        range_size = stop - start + 1;
        //iterate pointcloud...
        while(id <= stop && id_array < (range_size * 3))
        {
          pointArray[id_array] = (float) _vertices->at(id).x();
          pointArray[id_array + 1] = (float) _vertices->at(id).y();
          pointArray[id_array + 2] = (float) _vertices->at(id).z();

          //array index for pointArray must be incremented by 3 since 3 float values (x,y,z) will be copied per point
          // range_size * 3 -1 therefore is the maximum allowed index
          id_array = id_array + 3;
          id = id + 1;
        }
        return range_size;
      }
      return 0;
    }
  }
}