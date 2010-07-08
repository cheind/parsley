/*
 * Parsley http://parsley.googlecode.com
 * Copyright (c) 2010, Christoph Heindl. All rights reserved.
 * Code license:	New BSD License
 */

#include <draw3d/viewer.h>
#include <draw3d/convert.h>

#include <Windows.h>

#include <osg/Camera>
#include <osgGA/TrackballManipulator>
#include <osg/Viewport>
#include <osg/GraphicsContext>
#include <osgViewer/GraphicsWindowWin32>
#include <osgViewer/Viewer>
#include <osgViewer/Viewer>
#include <osg/Geometry>

// demo
#include <osg/Geode>
#include <osg/ShapeDrawable>


namespace Parsley {
  namespace Draw3D {

    Viewer::Viewer(System::Windows::Forms::Control ^render_target){
      HWND hwnd = (HWND)render_target->Handle.ToPointer();
      this->initialize_viewer((void*)hwnd);
      _root = gcnew Group();
      _viewer->setSceneData(_root->osg().get());
    }

    Viewer::~Viewer() {
      delete _root;
      this->!Viewer();
    }

    Viewer::!Viewer() {
      _viewer.reset();
      _context.reset();
    }

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

      _context = osg::GraphicsContext::createGraphicsContext( traits.get() ); 

      _viewer = new osgViewer::Viewer();
      osg::Camera *camera = _viewer->getCamera();
      camera->setGraphicsContext(_context.get());
      osg::ref_ptr<osg::Viewport> viewport = new osg::Viewport( 0, 0, traits->width, traits->height);
      camera->setViewport(viewport);
      camera->setProjectionMatrixAsPerspective(30.0f, static_cast<double>(traits->width)/static_cast<double>(traits->height), 1.0f, 10000.0f);
      _viewer->setCameraManipulator(new osgGA::TrackballManipulator);
    }

    void Viewer::SetupPerspectiveProjection(array<double,2> ^matrix) {
      osg::Camera *camera = _viewer->getCamera();
      camera->setProjectionMatrix(convert(matrix, transposed()));
    }

    void 
    Viewer::LookAt(array<double> ^eye, array<double> ^center, array<double> ^up)
    {
      osgGA::MatrixManipulator *manip =  _viewer->getCameraManipulator();
      manip->setHomePosition(convert3(eye), convert3(center), convert3(up));
      manip->home(0);

      osg::Matrixd m = manip->getMatrix();
      m(0,0) = m(1,1) = -1;
      manip->setByMatrix(m);
    }

    bool 
    Viewer::Add(Node ^node) {
      return _root->Add(node);
    }

    void
    Viewer::SetClearColor(array<double> ^color)
    {
      osg::Camera *camera = _viewer->getCamera();
      camera->setClearColor(convert4(color));
    }
  }
}