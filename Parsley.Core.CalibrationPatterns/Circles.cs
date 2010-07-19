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
using Parsley.Core.Extensions;

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
    private int _binary_threshold;
    private int _min_contour_count;
    private float _mean_distance_threshold;
    private int _number_circle_points;
    private float _ellipse_distance;

    public Circles(int ncircles_x, int ncircles_y, float distance_x, float distance_y) {
      _mean_distance_threshold = 1.0f;
      _number_circle_points = 30;
      _ellipse_distance = 10;
      _distance_x = distance_x;
      _distance_y = distance_y;
      _number_circle_centers = new System.Drawing.Size(ncircles_x, ncircles_y);
      _binary_threshold = 40;
      _min_contour_count = 40;
      this.ObjectPoints = GenerateObjectCenters();
    }

    /// <summary>
    /// Default circles constructor
    /// </summary>
    public Circles()
      : this(5, 4, 30, 30) { }

    /// <summary>
    /// Get/set the number of circles in x and y
    /// </summary>
    [Description("Get/set the number of circles in x and y")]
    [DefaultValue(typeof(System.Drawing.Size), "2, 2")]
    public System.Drawing.Size Size {
      get { return _number_circle_centers; }
      set {
        System.Drawing.Size s;
        if (value.Height < 2 || value.Width < 2) {
          s = new System.Drawing.Size(2, 2);
        } else {
          s = value;
        }
        _number_circle_centers = s;
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
    public int BinaryThreshold {
      get { return _binary_threshold; }
      set { _binary_threshold = value; }
    }

    /// <summary>
    /// Set minimum number of contour points for ellipse filter
    /// </summary>
    [Description("Set minimum number of contour points for ellipse filter")]
    public int MinimumContourCount {
      get { return _min_contour_count; }
      set { _min_contour_count = value; }
    }

    /// <summary>
    /// Average allowed distance of fitted ellipse to contour points.
    /// </summary>
    [Description("Average allowed distance of fitted ellipse to contour points.")]
    public float MeanDistanceThreshold {
      get { return _mean_distance_threshold; }
      set { _mean_distance_threshold = value; }
    }

    /// <summary>
    /// Number of circle points for transition detection
    /// </summary>
    [Description("Number of circle points for transition detection")]
    public int NumberOfCirclePoints {
      get { return _number_circle_points; }
      set { _number_circle_points = value; }
    }

    /// <summary>
    /// Maximum allowed distance between detected ellipse and expected ellipse.
    /// </summary>
    [Description("Maximum allowed distance between detected ellipse and expected ellipse.")]
    public float MaximumEllipseDistance {
      get { return _ellipse_distance; }
      set { _ellipse_distance = value; }
    }

    /// <summary>
    /// Generate reference points
    /// </summary>
    /// <remarks>Generates object points per row from left to right.</remarks>
    /// <returns>Reference points in z-plane.</returns>
    Vector[] GenerateObjectCenters() {
      List<Vector> centers = new List<Vector>();
      for (int y = 0; y < _number_circle_centers.Height; ++y) {
        for (int x = 0; x < _number_circle_centers.Width; x++) {
          centers.Add(new Vector(new double[] { x * _distance_x, y * _distance_y, 0 }));
        }
      }
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
      gray._ThresholdBinary(new Gray(_binary_threshold), new Gray(255.0));
      gray._Not(); // Circles are black, black is considered backgroud, therefore flip.

      Parsley.Core.EllipseDetector ed = new Parsley.Core.EllipseDetector();
      ed.MinimumContourCount = this.MinimumContourCount;

      // Detect initial ellipses
      List<Parsley.Core.DetectedEllipse> ellipses =
        new List<Parsley.Core.DetectedEllipse>(ed.DetectEllipses(gray));

      // Filter out all ellipses below rating threshold
      List<Parsley.Core.DetectedEllipse> finals =
        new List<Parsley.Core.DetectedEllipse>(
          ellipses.Where(e => { return e.Rating < this.MeanDistanceThreshold; })
        );

      // At least the number of required ellipses need to be found
      if (finals.Count < _number_circle_centers.Width * _number_circle_centers.Height) {
        image_points = new System.Drawing.PointF[0];
        return false;
      }

      int[] marker_ids;
      if (!FindMarkerEllipses(gray, finals, out marker_ids)) {
        image_points = new System.Drawing.PointF[0];
        return false;
      }

      // Check that all markers are found
      if (marker_ids.Length != 4) {
        image_points = new System.Drawing.PointF[0];
        return false;
      }

      // Find intrinsic/extrinsic calibration matrices based on known marker correspondences
      Emgu.CV.IntrinsicCameraParameters icp;
      Emgu.CV.ExtrinsicCameraParameters ecp;
      ApproximatePlane(finals, marker_ids, out icp, out ecp, img.Size);

      // Project all object points to image points
      MCvPoint3D32f[] converted_object_points = Array.ConvertAll(
        this.ObjectPoints.ToArray(),
        value => { return value.ToEmguF(); });

      System.Drawing.PointF[] expected_image_points = 
        Emgu.CV.CameraCalibration.ProjectPoints(converted_object_points, ecp, icp);

      image_points =
        expected_image_points.Select(
          e => { return NearestEllipse(finals, e); }
        ).Where(
          ne => { return Math.Sqrt(ne.dist2) < _ellipse_distance; }
        ).Select(
          ne => { return ne.center; }
        ).ToArray();

      return image_points.Length == _number_circle_centers.Width * _number_circle_centers.Height;

    }

    struct NearestEllipseResult {
      public System.Drawing.PointF center;
      public double dist2;
    };

    private NearestEllipseResult NearestEllipse(List<DetectedEllipse> finals, System.Drawing.PointF ex) {
      NearestEllipseResult ner = new NearestEllipseResult();
      ner.dist2 = Double.MaxValue;
      foreach (DetectedEllipse de in finals) {
        System.Drawing.PointF p = de.Ellipse.MCvBox2D.center;
        double cur_dist2 = (ex.X - p.X) * (ex.X - p.X) + (ex.Y - p.Y) * (ex.Y - p.Y);
        if (cur_dist2 < ner.dist2) {
          ner.dist2 = cur_dist2;
          ner.center = p;
        }
      }
      return ner;
    }

    private void ApproximatePlane(List<DetectedEllipse> finals, int[] marker_ids, out Emgu.CV.IntrinsicCameraParameters icp, out Emgu.CV.ExtrinsicCameraParameters ecp, System.Drawing.Size size) {
      Vector[] object_points = new Vector[] {
        this.ObjectPoints[0], 
        this.ObjectPoints[_number_circle_centers.Width - 1],
        this.ObjectPoints[_number_circle_centers.Width * (_number_circle_centers.Height-1)],
        this.ObjectPoints[_number_circle_centers.Width * _number_circle_centers.Height - 1]};

      System.Drawing.PointF[] image_points =
        marker_ids.Select(id => { return finals[id].Ellipse.MCvBox2D.center; }).ToArray();
      
      Parsley.Core.IntrinsicCalibration ic = new Parsley.Core.IntrinsicCalibration(object_points, size);
      ic.AddView(image_points);
      icp = ic.Calibrate();
      Parsley.Core.ExtrinsicCalibration ec = new Parsley.Core.ExtrinsicCalibration(object_points, icp);
      ecp = ec.Calibrate(image_points);
    }

    private bool FindMarkerEllipses(Emgu.CV.Image<Gray, byte> gray, List<DetectedEllipse> finals, out int[] ids) {
      ids = new int[4] { -1, -1, -1, -1};

      for (int i = 0; i < finals.Count; ++i ) {
        int transitions = CountBinaryTransitions(finals[i], EllipseDetector.GetAffineFrame(finals[i].Ellipse), gray);
        switch (transitions) {
          case 8:
            if (ids[0] > -1) return false;
            ids[0] = i;
            break;
          case 6:
            if (ids[1] > -1) return false;
            ids[1] = i;
            break;
          case 4:
            if (ids[2] > -1) return false;
            ids[2] = i;
            break;
          case 2:
            if (ids[3] > -1) return false;
            ids[3] = i;
            break;
        }
      }
      return ids.Count(value => { return (value > -1); }) == 4;
    }

    private int CountBinaryTransitions(DetectedEllipse e, Matrix matrix, Emgu.CV.Image<Gray, byte> gray) {
      // Generate points on circle
      double r = e.Ellipse.MCvBox2D.size.Height * 0.5;
      double t_step = (2 * Math.PI) / _number_circle_points;

      System.Drawing.Rectangle rect = new System.Drawing.Rectangle(System.Drawing.Point.Empty, gray.Size);

      int count_transitions = 0;
      double last_intensity = 0;
      for (double t = 0; t <= 2 * Math.PI; t += t_step) {
        Vector v = new Vector(new double[] { r * Math.Cos(t), r * Math.Sin(t), 1.0 });
        Vector x = matrix.Multiply(v.ToColumnMatrix()).GetColumnVector(0);
        System.Drawing.Point p = new System.Drawing.Point((int)x[0], (int)x[1]);
        if (rect.Contains(p)) {
          if (t == 0) {
            last_intensity = gray[p].Intensity;
          } else {
            double i = gray[p].Intensity;
            if (i != last_intensity) {
              count_transitions += 1;
              last_intensity = i;
            }
          }
        }
      }
      return count_transitions;
    }
  }
}
