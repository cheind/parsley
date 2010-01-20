// Parsley.Draw3D.h

#pragma once

using namespace Emgu::CV::Structure;

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

    public ref class Viewer : public Core::Resource::SharedResource
    {
    public:
      Viewer(System::Windows::Forms::Control ^render_target);

      /// Render entire frame
      void Frame();
      void AddCapsule(array<double> ^center);
      bool Add(Node ^node);
      void LookAt(MCvPoint3D32f ^eye, MCvPoint3D32f ^center, MCvPoint3D32f ^up);
      void Projection(Emgu::CV::IntrinsicCameraParameters ^icp, double near_p, double far_p, double w, double h);

      void SetupPerspectiveProjection(array<double,2> ^matrix);
      void LookAt(array<double> ^eye, array<double> ^center, array<double> ^up);

    protected:
      virtual void DisposeUnmanaged() override;
      virtual void DisposeManaged() override;

    private:
      void initialize_viewer(void *hwnd_render_target);

      auto_ptr_osg<osgViewer::Viewer> _viewer;
      auto_ptr_osg<osg::GraphicsContext> _context;
      Group ^_root;
      void *_hwnd;
    };
  }
}