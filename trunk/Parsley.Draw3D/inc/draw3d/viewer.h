// Parsley.Draw3D.h

#pragma once

#include <draw3d/auto_ptr_osg.h>
#include <draw3d/group.h>

namespace osgViewer {
  class Viewer;
}

namespace osg {
  class Group;
  class GraphicsContext;
}

namespace Parsley {
  namespace Draw3D {    

    public ref class Viewer 
    {
    public:
      Viewer(System::Windows::Forms::Control ^render_target);
      ~Viewer();
      !Viewer();

      /// Render entire frame
      void Frame();
      bool Add(Node ^node);
      void SetupPerspectiveProjection(array<double,2> ^matrix);
      void LookAt(array<double> ^eye, array<double> ^center, array<double> ^up);

    private:
      void initialize_viewer(void *hwnd_render_target);

      auto_ptr_osg<osgViewer::Viewer> _viewer;
      auto_ptr_osg<osg::GraphicsContext> _context;
      Group ^_root;
      void *_hwnd;
    };
  }
}