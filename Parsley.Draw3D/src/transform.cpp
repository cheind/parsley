

#include <draw3d/transform.h>
#include <osg/MatrixTransform>

namespace Parsley {
  namespace Draw3D {

    bool 
    Transform::Add(Node ^node) { 
      return osg()->addChild(node->node().get());
    }

    Emgu::CV::Matrix<double> ^Transform::Matrix::get() {
      Emgu::CV::Matrix<double> ^ret = gcnew Emgu::CV::Matrix<double>(3,4);
      const osg::Matrixd &m = this->osg()->getMatrix();

      // http://www.openscenegraph.org/projects/osg/wiki/Support/Maths/MatrixTransformations

      for (unsigned r = 0; r < 3; ++r) {
        for (unsigned c = 0; c < 4; ++c) {
          ret->Data[r,c] = m(c,r);
        }
      }
      return ret;
    }

    void Transform::Matrix::set(Emgu::CV::Matrix<double> ^value) {
      osg::Matrixd m = osg::Matrix::identity();
      // http://www.openscenegraph.org/projects/osg/wiki/Support/Maths/MatrixTransformations
      for (unsigned r = 0; r < 3; ++r) {
        for (unsigned c = 0; c < 4; ++c) {
          m(c, r) = value->Data[r,c];
        }
      }
      this->osg()->setMatrix(m);
    }

  }
}