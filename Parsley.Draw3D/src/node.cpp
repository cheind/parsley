/*
 * Parsley http://parsley.googlecode.com
 * Copyright (c) 2010, Christoph Heindl. All rights reserved.
 * Code license:	New BSD License
 */

#include <draw3d/node.h>
#include <osgDB/ReadFile>
#include <msclr\marshal_cppstd.h>
#include <string>

namespace Parsley {
  namespace Draw3D {

    Node^ Node::ReadFromFile(System::String ^filepath) {
      std::string fs = msclr::interop::marshal_as<std::string>(filepath);
      osg::Node *n = osgDB::readNodeFile(fs);
      if (n != 0) {
        return gcnew Node(n);
      } else {
        return nullptr;
      }
    }
  }
}