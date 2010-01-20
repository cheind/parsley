

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

    void 
    Viewer::Projection(Emgu::CV::IntrinsicCameraParameters ^icp, double near_p, double far_p, double w, double h)
    {
      osg::Camera *camera = _viewer->getCamera();
      // http://opencv.willowgarage.com/wiki/Posit 
      // http://opencv.willowgarage.com/documentation/camera_calibration_and_3d_reconstruction.html
      // http://cvlab.epfl.ch/~fua/courses/vision/math/notes/Cameras.pdf
      double fx = icp->IntrinsicMatrix->Data[0,0];
      double fy = icp->IntrinsicMatrix->Data[1,1];
      double px = icp->IntrinsicMatrix->Data[0,2];
      double py = icp->IntrinsicMatrix->Data[1,2];

      osg::Matrixd lens = osg::Matrixd::identity();
      lens(0,0) = 2.0 * (fx / w); lens(0,1) =            0.0; lens(0,2) =         2.0 * (px / w) - 1.0; lens(0,3) =                              0.0;
      lens(1,0) =            0.0; lens(1,1) = 2.0 * (fy / h); lens(1,2) =         2.0 * (py / h) - 1.0; lens(1,3) =                              0.0;      
      lens(2,0) =            0.0; lens(2,1) =            0.0; lens(2,2) = -( far_p+near_p ) / ( far_p-near_p ); lens(2,3) = -2.0 * far_p * near_p / ( far_p-near_p );
      lens(3,0) =            0.0; lens(3,1) =            0.0; lens(3,2) =                         -1.0; lens(3,3) =                              0.0;

      osg::Matrixd lens2;
      for (int i  =0; i < 4; ++i) {
        for (int j = 0; j<4; ++j) {
          lens2(i,j) = lens(j,i);
        }
      }

      // http://www.mail-archive.com/osg-users@lists.openscenegraph.org/msg19124.html
      camera->setProjectionMatrix(lens2);
    }

    void Viewer::SetupPerspectiveProjection(array<double,2> ^matrix) {
      osg::Camera *camera = _viewer->getCamera();
      camera->setProjectionMatrix(convert(matrix, transposed()));
    }

    void
    Viewer::LookAt(MCvPoint3D32f ^eye, MCvPoint3D32f ^center, MCvPoint3D32f ^up) {
      osgGA::MatrixManipulator *manip =  _viewer->getCameraManipulator();
      manip->setHomePosition(C::c(eye), C::c(center), C::c(up));
      manip->home(0);

      osg::Matrixd m = manip->getMatrix();
      m(0,0) = m(1,1) = -1;
      manip->setByMatrix(m);
    }

    void 
    Viewer::LookAt(array<double> ^eye, array<double> ^center, array<double> ^up)
    {
      osgGA::MatrixManipulator *manip =  _viewer->getCameraManipulator();
      manip->setHomePosition(convert(eye), convert(center), convert(up));
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
    Viewer::DisposeManaged()
    {
    }

    void
      Viewer::DisposeUnmanaged()
    {
      System::Console::WriteLine("Viewer-Dispose called!");
      _viewer.reset();
      _context.reset();
    }
    

    void 
    Viewer::AddCapsule(array<double> ^center) {
      osg::ref_ptr<osg::Geode> geode(new osg::Geode());
      osg::ref_ptr<osg::Capsule> capsule(new osg::Capsule(convert(center), 2, 1));
      osg::ref_ptr<osg::ShapeDrawable> drawable( new osg::ShapeDrawable(capsule.get()) );
      geode->addDrawable(drawable.get());
      _root->osg()->addChild(geode.get()); 
    }
  }
}