#include "Viewer.h"

#include <osg/Camera>
#include <osgGA/TrackballManipulator>
#include <osg/Viewport>
#include <osg/GraphicsContext>
#include <osgViewer/GraphicsWindowWin32>
#include <osgViewer/Viewer>
#include <osgViewer/Viewer>

// demo
#include <osg/Geode>
#include <osg/ShapeDrawable>


namespace Parsley {
  namespace Draw3D {
    namespace Internal {

      Viewer::Viewer(HWND render_target) {
        initialize_viewer(render_target);
        _root = new osg::Group();
        _viewer->setSceneData(_root.get());
      }

      void Viewer::frame() {
        _viewer->frame();
      }

      void Viewer::add_capsule(osg::Vec3 center) {
        osg::ref_ptr<osg::Geode> geode(new osg::Geode());
        osg::ref_ptr<osg::Capsule> capsule(new osg::Capsule(center, 1, 2));
        osg::ref_ptr<osg::ShapeDrawable> drawable( new osg::ShapeDrawable(capsule.get()) );
        geode->addDrawable(drawable.get());
        _root->addChild(geode.get());
        
      }

      void Viewer::initialize_viewer(HWND render_target) {
        osg::ref_ptr<osg::GraphicsContext::Traits> traits = new osg::GraphicsContext::Traits();
        traits->inheritedWindowData = new osgViewer::GraphicsWindowWin32::WindowData( render_target );
        traits->setInheritedWindowPixelFormat = true;
        traits->doubleBuffer = true;
        traits->windowDecoration = true;
        traits->sharedContext = NULL;
        traits->supportsResize = true;

        RECT rect;
        ::GetWindowRect( render_target, &rect ); // requires User32.lib
        traits->x = 0;
        traits->y = 0;
        traits->width = rect.right - rect.left;
        traits->height = rect.bottom - rect.top;

        osg::ref_ptr<osg::GraphicsContext> context = osg::GraphicsContext::createGraphicsContext( traits.get() ); 

        _viewer = new osgViewer::Viewer();
        osg::Camera *camera = _viewer->getCamera();
        camera->setGraphicsContext(context.get());
        osg::ref_ptr<osg::Viewport> viewport = new osg::Viewport( 0, 0, traits->width, traits->height);
        camera->setViewport(viewport);
        camera->setProjectionMatrixAsPerspective(30.0f, static_cast<double>(traits->width)/static_cast<double>(traits->height), 1.0f, 10000.0f);
        _viewer->setCameraManipulator(new osgGA::TrackballManipulator);
      }
    }

    Viewer::Viewer(System::Windows::Forms::Control ^render_target){
      HWND hwnd = (HWND)render_target->Handle.ToPointer();
      _viewer = new Internal::Viewer(hwnd);
    }

    Viewer::~Viewer() {
      if (_viewer != 0) {
        delete _viewer;
        _viewer = 0;
      }
    }

    Viewer::!Viewer() {
      if (_viewer != 0) {
        delete _viewer;
        _viewer = 0;
      }
    }

    void 
    Viewer::Frame() { _viewer->frame(); }

    void 
    Viewer::AddCapsule(float x, float y, float z) {
      _viewer->add_capsule(osg::Vec3(x, y, z));
    }
  }
}