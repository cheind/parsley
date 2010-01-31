using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MathNet.Numerics.LinearAlgebra;

namespace Parsley.Core.BuildingBlocks {

  /// <summary>
  /// Provides creation of perspective matrices used for 3D rendering.
  /// </summary>
  public class Perspective {

    /// <summary>
    /// Creates a off-axis perspective matrix from camera intrinsics.
    /// </summary>
    /// <remarks> Note that OSG layouts their matrices in row-major order, so you
    /// need to transpose this matrix, before passing it to OSG</remarks>
    /// <param name="camera">Camera with intrinsic parameters</param>
    /// <returns>Perspective matrix in column-major</returns>
    public static Matrix FromCamera(Camera camera, double near, double far) {
      if (!camera.HasIntrinsics) {
        throw new ArgumentException("Camera has no intrinsic calibration");
      }
      // see
      // http://opencv.willowgarage.com/wiki/Posit 
      // http://opencv.willowgarage.com/documentation/camera_calibration_and_3d_reconstruction.html
      // http://aoeu.snth.net/static/gen-perspective.pdf
      // http://www.hitl.washington.edu/artoolkit/mail-archive/message-thread-00654-Re--Questions-concering-.html

      Matrix m = new Matrix(4, 4, 0.0);

      Emgu.CV.IntrinsicCameraParameters icp = camera.Intrinsics;
      double fx = icp.IntrinsicMatrix[0, 0];
      double fy = icp.IntrinsicMatrix[1, 1];
      double px = icp.IntrinsicMatrix[0, 2];
      double py = icp.IntrinsicMatrix[1, 2];
      double w = camera.FrameWidth;
      double h = camera.FrameHeight;

      double dist = far - near;

      // First row
      m[0, 0] = 2.0 * (fx / w);
      m[0, 2] = 2.0 * (px / w) - 1.0;
      
      // Second row
      m[1, 1] = 2.0 * (fy / h);
      m[1, 2] = 2.0 * (py / h) - 1.0;
      
      // Third row
      m[2, 2] = -(far + near) / dist;
      m[2, 3] = -2.0 * far * near / dist;

      // Fourth row (note the flip)
      m[3, 2] = -1.0;

      return m;
    }



  }
}
