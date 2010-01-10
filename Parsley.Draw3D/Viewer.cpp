#include "Viewer.h"
#include "convert.h"

#include <Windows.h>

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
   
    Viewer::Viewer(System::Windows::Forms::Control ^render_target){
      HWND hwnd = (HWND)render_target->Handle.ToPointer();
      this->initialize_viewer((void*)hwnd);
      _root = new osg::Group();
      _viewer->setSceneData(_root.get());
    }

    Viewer::~Viewer() {}
    Viewer::!Viewer() {}

    void 
    Viewer::Frame() { _viewer->frame(); }

    void Viewer::initialize_viewer(void *render_target) {
      HWND hwnd = (HWND) render_target;
      osg::ref_ptr<osg::GraphicsContext::Traits> traits = new osg::GraphicsContext::Traits();
      traits->inheritedWindowData = new osgViewer::GraphicsWindowWin32::WindowData( hwnd );
      traits->setInheritedWindowPixelFormat = true;
      traits->doubleBuffer = true;
      traits->windowDecoration = true;
      traits->sharedContext = NULL;
      traits->supportsResize = true;

      RECT rect;
      ::GetWindowRect( hwnd, &rect ); // requires User32.lib
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

    void
    Viewer::LookAt(MCvPoint3D32f ^eye, MCvPoint3D32f ^center, MCvPoint3D32f ^up) {
      osgGA::MatrixManipulator *manip =  _viewer->getCameraManipulator();
      manip->setHomePosition(C::c(eye), C::c(center), C::c(up));
      manip->home(0);
    }

    void 
    Viewer::AddCapsule(MCvPoint3D32f ^center) {
      osg::ref_ptr<osg::Geode> geode(new osg::Geode());
      osg::ref_ptr<osg::Capsule> capsule(new osg::Capsule(C::c(center), 2, 1));
      osg::ref_ptr<osg::ShapeDrawable> drawable( new osg::ShapeDrawable(capsule.get()) );
      geode->addDrawable(drawable.get());
      _root->addChild(geode.get()); 
    }
  }
}