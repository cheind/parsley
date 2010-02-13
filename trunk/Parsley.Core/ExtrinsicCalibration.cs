/*
 * Parsley http://parsley.googlecode.com
 * Copyright (c) 2010, Christoph Heindl. All rights reserved.
 * Code license:	New BSD License
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Emgu.CV.Structure;
using MathNet.Numerics.LinearAlgebra;
using Parsley.Core.Extensions;

namespace Parsley.Core {

  /// <summary>
  /// Perform extrinsic calibration
  /// </summary>
  public class ExtrinsicCalibration : CalibrationBase {
    private Emgu.CV.IntrinsicCameraParameters _intrinsics;
    private MCvPoint3D32f[] _converted_object_points;

    public ExtrinsicCalibration(Vector[] object_points, Emgu.CV.IntrinsicCameraParameters intrinsics)
      : base(object_points) 
    {
      _intrinsics = intrinsics;
      // Since object points remain constant, we can apply their conversion right here
      _converted_object_points = Array.ConvertAll<Vector, MCvPoint3D32f>(
        this.ObjectPoints, 
        new Converter<Vector, MCvPoint3D32f>(Extensions.ConvertFromParsley.ToEmguF)
      );
    }

    public Emgu.CV.IntrinsicCameraParameters Intrinsics {
      get { return _intrinsics; }
    }

    /// <summary>
    /// Find extrinsic calibration
    /// </summary>
    /// <param name="image_points">Detected pattern points in image</param>
    /// <returns>Extrinsic calibration</returns>
    public Emgu.CV.ExtrinsicCameraParameters Calibrate(System.Drawing.PointF[] image_points) {
      return Emgu.CV.CameraCalibration.FindExtrinsicCameraParams2(
       _converted_object_points,
       image_points,
       _intrinsics);
    }

    public static void CalibrationError(
      Emgu.CV.ExtrinsicCameraParameters ecp,
      Emgu.CV.IntrinsicCameraParameters icp,
      System.Drawing.PointF[] image_points,
      Vector[] reference_points,
      out double[] deviations,
      out Vector[] isect_points) 
    {
      // Shoot rays through image points,
      // intersect with plane defined by extrinsic
      // and measure distance to reference points
      Matrix inv_ecp = Matrix.Identity(4, 4);
      inv_ecp.SetMatrix(0, 2, 0, 3, ecp.ExtrinsicMatrix.ToParsley());
      inv_ecp = inv_ecp.Inverse();

      Ray[] rays = Ray.EyeRays(icp, image_points);
      Plane p = new Plane(ecp);
      isect_points = new Vector[rays.Length];

      deviations = new double[rays.Length];
      for (int i = 0; i < rays.Length; ++i) {
        double t;
        Intersection.RayPlane(rays[i], p, out t);
        Vector isect = rays[i].At(t);
        Vector x = new Vector(new double[]{isect[0],isect[1],isect[2],1});
        x = (inv_ecp * x.ToColumnMatrix()).GetColumnVector(0);
        Vector final = new Vector(new double[]{x[0], x[1], x[2]});
        isect_points[i] = final;
        deviations[i] = (final - reference_points[i]).Norm();
      }
    }

    public static void VisualizeError(
      Emgu.CV.Image<Bgr, Byte> img,
      Emgu.CV.ExtrinsicCameraParameters ecp,
      Emgu.CV.IntrinsicCameraParameters icp,
      double[] deviations,
      Vector[] isect_points) 
    {
      Bgr bgr = new Bgr(System.Drawing.Color.Green);
      MCvFont f = new MCvFont(Emgu.CV.CvEnum.FONT.CV_FONT_HERSHEY_PLAIN, 0.8, 0.8);
      foreach (Vector p in isect_points) {
        
        System.Drawing.PointF[] coords = Emgu.CV.CameraCalibration.ProjectPoints(
          new MCvPoint3D32f[] { p.ToEmguF() },
          ecp, icp
        );
        img.Draw(new CircleF(coords[0], 1), bgr, 1);
      }
    }


  }
}
