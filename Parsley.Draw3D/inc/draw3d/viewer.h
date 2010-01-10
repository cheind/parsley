// Parsley.Draw3D.h

#pragma once

using namespace Emgu::CV::Structure;

#include <draw3d/auto_ptr_osg.h>

namespace osgViewer {
  class Viewer;
}

namespace osg {
  class Group;
}

namespace Parsley {
  namespace Draw3D {    

    public ref class Viewer
    {
    public:
      Viewer(System::Windows::Forms::Control ^render_target);
      Viewer::~Viewer();
      Viewer::!Viewer();

      /// Render entire frame
      void Frame();
      void AddCapsule(MCvPoint3D32f ^center);
      void LookAt(MCvPoint3D32f ^eye, MCvPoint3D32f ^center, MCvPoint3D32f ^up);
    private:
      void initialize_viewer(void *hwnd_render_target);

      auto_ptr_osg<osgViewer::Viewer> _viewer;
      auto_ptr_osg<osg::Group> _root;
    };
  }
}