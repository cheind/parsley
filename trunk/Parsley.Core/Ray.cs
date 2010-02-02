using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MathNet.Numerics.LinearAlgebra;
using System.Drawing;

namespace Parsley.Core {
  
  /// <summary>
  /// Defines a ray through the origin
  /// </summary>
  public class Ray {
    private Vector _direction;

    /// <summary>
    /// Initialize ray from direction
    /// </summary>
    /// <param name="origin">Origin of ray</param>
    /// <param name="direction">Direction of ray as unit-length vector</param>
    public Ray(Vector direction) {
      _direction = direction;
    }

    /// <summary>
    /// Get direction of ray
    /// </summary>
    public Vector Direction {
      get { return _direction; }
    }

    /// <summary>
    /// Evaluate the parameteric ray equation x = o + t*direction, where o is
    /// the zero-point.
    /// </summary>
    /// <returns>Position on ray</returns>
    public Vector At(double t) {
      return _direction * t;
    }

    /// <summary>
    /// Construct eye-rays through the specified pixels
    /// </summary>
    /// <param name="icp">Intrinsic camera parameters</param>
    /// <param name="pixels">Pixels</param>
    /// <returns>Enumerable ray collection</returns>
    public static Ray[] EyeRays(Emgu.CV.IntrinsicCameraParameters icp, PointF[] pixels) {
      Ray[] rays = new Ray[pixels.Length];
      if (pixels.Length > 0) {
        // 1. Undistort pixels
        PointF[] undistorted_pixels = icp.Undistort(pixels, null, icp.IntrinsicMatrix);
        // 2. Create rays
        // Use inverse intrinsic calibration and depth = 1
        double cx = icp.IntrinsicMatrix.Data[0, 2];
        double cy = icp.IntrinsicMatrix.Data[1, 2];
        double fx = icp.IntrinsicMatrix.Data[0, 0];
        double fy = icp.IntrinsicMatrix.Data[1, 1];


        Vector direction = new Vector(3);
        for (int i = 0; i < undistorted_pixels.Length; ++i) {
          PointF pixel = undistorted_pixels[i];
          direction[0] = (pixel.X - cx) / fx;
          direction[1] = (pixel.Y - cy) / fy;
          direction[2] = 1;
          rays[i] = new Ray(direction.Normalize());
        }
      }
      return rays;
    }
  }
}
