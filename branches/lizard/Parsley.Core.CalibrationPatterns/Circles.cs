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
using System.ComponentModel;

namespace Parsley.Core.CalibrationPatterns {

  /// <summary>
  /// Represents calibration pattern based on circles.
  /// </summary>
  /// <remarks>
  /// Circles become ellipses under perspective projection (except when the view is orthogonal to the circle). 
  /// Therefore this algorithm searches for ellipses instead of circles. The pattern is assumed to be a rectangular
  /// composition of circles of the same radius.
  /// First the input image is thresholded to become a binary image of black and white pixels. Next, the contours of
  /// the image are extracted and an ellipse is fit to each contour.
  /// </remarks>
  [Serializable]
  [Parsley.Core.Addins.Addin]
  public class Circles : CalibrationPattern {
    private System.Drawing.Size _number_circle_centers;
    private float _distance_x, _distance_y;
    private int _threshold;
    private int _min_contour_count;

    public Circles(int ncircles_x, int ncircles_y, float distance_x, float distance_y) {
      _distance_x = distance_x;
      _distance_y = distance_y;
      _number_circle_centers = new System.Drawing.Size(ncircles_x, ncircles_y);
      _threshold = 40;
      _min_contour_count = 40;
      this.ObjectPoints = GenerateObjectCenters();
    }

    /// <summary>
    /// Default circles constructor
    /// </summary>
    public Circles()
      : this(2, 2, 100, 80) { }

    /// <summary>
    /// Get/set the number of circles in x and y
    /// </summary>
    [Description("Get/set the number of circles in x and y")]
    [DefaultValue(typeof(System.Drawing.Size), "2, 2")]
    public System.Drawing.Size Size {
      get { return _number_circle_centers; }
      set {
        _number_circle_centers = value;
        this.ObjectPoints = GenerateObjectCenters();
      }
    }

    /// <summary>
    /// Distance between circle centers in y
    /// </summary>
    [Description("Distance between circle centers in y")]
    [DefaultValue(80)]
    public float CenterDistanceY {
      get { return _distance_y; }
      set { _distance_y = value; }
    }

    /// <summary>
    /// Distance between circle centers in x
    /// </summary>
    [DefaultValue(100)]
    [Description("Distance between circle centers in x")]
    public float CenterDistanceX {
      get { return _distance_x; }
      set { _distance_x = value; }
    }

    /// <summary>
    /// Set the threshold for blackness
    /// </summary>
    [Description("Set the threshold for blackness")]
    public int Threshold {
      get { return _threshold; }
      set { _threshold = value; }
    }

    /// <summary>
    /// Set minimum number of contour points for ellipse filter
    /// </summary>
    [Description("Set minimum number of contour points for ellipse filter")]
    public int MinContourCount {
      get { return _min_contour_count; }
      set { _min_contour_count = value; }
    }

    /// <summary>
    /// Generate reference points
    /// </summary>
    /// <returns>Reference points in z-plane.</returns>
    Vector[] GenerateObjectCenters() {
      List<Vector> centers = new List<Vector>();
      for (int y = 0; y < _number_circle_centers.Height; ++y) {
        for (int x = 0; x < _number_circle_centers.Width; x++) {
          centers.Add(new Vector(new double[] { x * _distance_x, y * _distance_y, 0 }));
        }
      }

      // TODO Document this sorting
      centers.Sort((a, b) => { return a.Norm().CompareTo(b.Norm()); });
      return centers.ToArray();
    }

    /// <summary>
    /// Find ellipses in image
    /// </summary>
    /// <param name="img">Image to search pattern for</param>
    /// <param name="image_points">Detected centers</param>
    /// <returns>True if pattern was found, false otherwise</returns>
    public override bool FindPattern(Emgu.CV.Image<Gray, byte> img, out System.Drawing.PointF[] image_points) {
      Emgu.CV.Image<Gray, byte> gray = img.Convert<Gray, byte>();
      gray._ThresholdBinary(new Gray(_threshold), new Gray(255.0));
      gray._Not(); // Circles are black, black is considered backgroud, therefore flip.
      Emgu.CV.Contour<System.Drawing.Point> c = gray.FindContours();

      List<System.Drawing.PointF> centers = new List<System.Drawing.PointF>();

      while (c != null) {
        if (c.Count() >= _min_contour_count) {
          System.Drawing.PointF[] mypoints = Array.ConvertAll(
            c.ToArray<System.Drawing.Point>(),
            value => new System.Drawing.PointF(value.X, value.Y)
          );

          Ellipse e = Emgu.CV.PointCollection.EllipseLeastSquareFitting(mypoints);
          centers.Add(e.MCvBox2D.center);
        }
        c = c.HNext;
      }

      centers.Sort(
        (a, b) => {
          double dista = a.X * a.X + a.Y * a.Y;
          double distb = b.X * b.X + b.Y * b.Y;
          return dista.CompareTo(distb);
        }
      );

      image_points = centers.ToArray();
      return centers.Count == (_number_circle_centers.Height * _number_circle_centers.Width);
    }
  }
}
