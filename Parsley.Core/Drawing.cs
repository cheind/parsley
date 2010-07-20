/*
 * Parsley http://parsley.googlecode.com
 * Copyright (c) 2010, Christoph Heindl. All rights reserved.
 * Code license:	New BSD License
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MathNet.Numerics.LinearAlgebra;
using Emgu.CV.Structure;
using System.Drawing;

namespace Parsley.Core {

  /// <summary>
  /// Drawing Utils.
  /// </summary>
  public static class Drawing {

    /// <summary>
    /// Draw a visual indication of the pattern coordinate frame
    /// </summary>
    /// <param name="img">Image to draw to</param>
    /// <param name="ecp">Extrinsic calibration</param>
    /// <param name="icp">Intrinsic calibration</param>
    public static void DrawCoordinateFrame(
      Emgu.CV.Image<Bgr, Byte> img,
      Emgu.CV.ExtrinsicCameraParameters ecp,
      Emgu.CV.IntrinsicCameraParameters icp) {
      float extension = img.Width / 10;
      PointF[] coords = Emgu.CV.CameraCalibration.ProjectPoints(
        new MCvPoint3D32f[] { 
          new MCvPoint3D32f(0, 0, 0),
          new MCvPoint3D32f(extension, 0, 0),
          new MCvPoint3D32f(0, extension, 0),
          new MCvPoint3D32f(0, 0, extension),
        },
        ecp, icp);

      img.Draw(new LineSegment2DF(coords[0], coords[1]), new Bgr(System.Drawing.Color.Red), 2);
      img.Draw(new LineSegment2DF(coords[0], coords[2]), new Bgr(System.Drawing.Color.Green), 2);
      img.Draw(new LineSegment2DF(coords[0], coords[3]), new Bgr(System.Drawing.Color.Blue), 2);
    }
    
  }
}