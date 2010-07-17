/*
 * Parsley http://parsley.googlecode.com
 * Copyright (c) 2010, Christoph Heindl. All rights reserved.
 * Code license:	New BSD License
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

using MathNet.Numerics.LinearAlgebra;
using Emgu.CV.Structure;
using System.ComponentModel;

namespace Parsley.Core {

  /// <summary>
  /// A Calibration pattern
  /// </summary>
  [Serializable]
  public abstract class CalibrationPattern {
    private Vector[] _object_points;
    [NonSerialized]
    private PointF[] _image_points;
    [NonSerialized]
    private bool _pattern_found;

    public CalibrationPattern() {}

    /// <summary>
    /// Get or set the object points
    /// </summary>
    [Browsable(false)]
    public Vector[] ObjectPoints {
      get { return _object_points; }
      set { _object_points = value; }
    }

    /// <summary>
    /// Get the corresponding image points from the last call to FindPattern
    /// </summary>
    [Browsable(false)]
    public PointF[] ImagePoints {
      get { return _image_points; }
    }

    /// <summary>
    /// True if pattern was found by last FindPattern call; false otherwise.
    /// </summary>
    [Browsable(false)]
    public bool PatternFound {
      get { return _pattern_found; }
    }

    /// <summary>
    /// Find pattern in image and make it accessible through local properties.
    /// </summary>
    /// <param name="img">Image to find pattern in.</param>
    /// <returns>True if pattern was found in image, false otherwise</returns>
    public bool FindPattern(Emgu.CV.Image<Gray, byte> img){
      _pattern_found =  this.FindPattern(img, out _image_points);
      return _pattern_found;
    }

    /// <summary>
    /// Find pattern in image region  and make it accessible through local properties.
    /// </summary>
    /// <param name="img">Image to find pattern in.</param>
    /// <param name="roi">Region of interest</param>
    /// <returns>True if pattern was found in image, false otherwise</returns>
    public bool FindPattern(Emgu.CV.Image<Gray, byte> img, Rectangle roi) {
      _pattern_found = this.FindPattern(img, roi, out _image_points);
      return _pattern_found;
    }

    /// <summary>
    /// Find pattern in image region
    /// </summary>
    /// <param name="img">Image to find pattern in</param>
    /// <param name="roi">Region of interest</param>
    /// <param name="image_points">Image points relative to original image</param>
    /// <returns></returns>
    public bool FindPattern(Emgu.CV.Image<Gray, byte> img, Rectangle roi, out PointF[] image_points) {
      try {
        if (!roi.IsEmpty) {
          Emgu.CV.Image<Gray, byte> selected = img.GetSubRect(roi); // Shares memory with original image
          bool found = this.FindPattern(selected, out image_points);
          // Transform points back to original image coordinates
          PointF origin = roi.Location;
          for (int i = 0; i < image_points.Length; i++) {
            image_points[i] = new PointF(image_points[i].X + origin.X, image_points[i].Y + origin.Y);
          }
          return found;
        } else {
          image_points = new PointF[0];
          return false;
        }
      } catch (Emgu.CV.CvException) {
        image_points = new PointF[0];
        return false;
      }
    }

    
    /// <summary>
    /// Find pattern in image.
    /// </summary>
    /// <param name="img">Image to find pattern in.</param>
    /// <param name="image_points">Pattern points in image.</param>
    /// <returns>True if pattern was found in image, false otherwise.</returns>
    abstract public bool FindPattern(Emgu.CV.Image<Gray, byte> img, out PointF[] image_points);

    /// <summary>
    /// Draw a visual indication of the pattern coordinate frame
    /// </summary>
    /// <param name="img">Image to draw to</param>
    /// <param name="ecp">Extrinsic calibration</param>
    /// <param name="icp">Intrinsic calibration</param>
    public virtual void DrawCoordinateFrame(
      Emgu.CV.Image<Bgr, Byte> img,
      Emgu.CV.ExtrinsicCameraParameters ecp,
      Emgu.CV.IntrinsicCameraParameters icp) 
    {
      Drawing.DrawCoordinateFrame(img, ecp, icp);
    }

    /// <summary>
    /// Draw pattern to image.
    /// </summary>
    /// <param name="img">Colored image to draw to.</param>
    /// <param name="image_points">Image center points.</param>
    /// <param name="pattern_found">If true green indicators are drawn, red ones otherwise.</param>
    public virtual void DrawPattern(Emgu.CV.Image<Bgr, Byte> img, PointF[] image_points, bool pattern_found) {
      System.Drawing.Color color = pattern_found ? System.Drawing.Color.Green : System.Drawing.Color.Red;
      Bgr bgr = new Bgr(color);
      MCvFont f = new MCvFont(Emgu.CV.CvEnum.FONT.CV_FONT_HERSHEY_PLAIN, 0.8, 0.8);
      int count = 1;
      foreach (PointF point in image_points) {
        img.Draw(new CircleF(point, 4), bgr, 2);
        img.Draw(count.ToString(), ref f, new System.Drawing.Point((int)point.X + 5, (int)point.Y - 5), bgr);
        count++;
      }
    }
  };
}
