// Parsley.Draw3D.h

#pragma once

#include <Windows.h>
#include <osg/ref_ptr>
#include <osg/Vec3>

namespace osgViewer {
  class Viewer;
}

namespace osg {
  class Group;
}

namespace Parsley {
  namespace Draw3D {
    namespace Internal {

      /// Provides access to osgViewer::Viewer
      class Viewer {
      public:
        Viewer(HWND render_target);
        void frame();
        void add_capsule(osg::Vec3 center);

      private:
        void initialize_viewer(HWND render_target);

        osg::ref_ptr<osgViewer::Viewer> _viewer;
        osg::ref_ptr<osg::Group> _root;
      };
    }

    public ref class Viewer
    {
    public:
      Viewer(System::Windows::Forms::Control ^render_target);
      Viewer::~Viewer();
      Viewer::!Viewer();
      /// Render entire frame
      void Frame();
      void AddCapsule(float x, float y, float z);
    private:
      Internal::Viewer *_viewer;
    };
  }
}