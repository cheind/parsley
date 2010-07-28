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
  /// A calibration pattern based on a rectangular assembly of circles.
  /// </summary>
  /// <remarks>
  /// The pattern offers a unique orientation through the four corner circles that contain special markings.
  /// These markers need to be detected by the algorithm, or the calibration will fail. 
  /// 
  /// First, the algorithm detects all visible ellipses (since circles become ellipses under perspective projection).
  /// The ellipses are filtered through rating them by the average distance of ellipse-contour points to the fitted
  /// ellipses. Then, the remaining ellipses are searched for the four special marker ellipses. If found, an initial
  /// calibration (intrinsic and extrinsic) is computed from the marker ellipse centers and the corresponding 
  /// object points. Next, the remaining ellipse centers are forcast by projecting the associated object centers
  /// into image coordinates. When the projected centers are close to detected image ellipse centers a correspondence
  /// is generated. Finally, the image centers of the detected ellipse centers that were brought into correspondence
  /// are returned.
  /// 
  /// The marker ellipses, circles with a varying count of white rectangular shapes in it, are found by counting the
  /// varying number of black/white transitions. In detail, for each ellipse that passed the initial tests, a coordinate 
  /// frame is generated in which the ellipse is transformed into a circle (through scaling) with radius b located at 
  /// the origin. In this frame user-defined number of points on a circle with radius b/2 are generated in counter-clockwise
  /// order and backtransformed into the image coordinate system. Then, a simple counter tracks the number of black/white
  /// changes while the intensity of each pixel in turn is analysed. 
  /// 
  /// The algorithm itself could cope with missing (non-marker) ellipses, but currently fails, because of design limitations
  /// of its base-class.
  /// </remarks>
  [Serializable]
  [Parsley.Core.Addins.Addin]
  public class Circles : CalibrationPattern {

    /// <summary>
    /// Defines the position of the origin of the object points
    /// </summary>
    public enum EOriginPosition {
      TopLeft,
      Center
    };
    
    private System.Drawing.Size _number_circle_centers;
    private float _distance_x, _distance_y;
    private int _binary_threshold;
    private int _min_contour_count;
    private float _mean_distance_threshold;
    private int _number_circle_points;
    private float _ellipse_distance;
    private EOriginPosition _origin_position;

    /// <summary>
    /// Construct from parameters
    /// </summary>
    /// <param name="ncircles_x">Number of circles in x-direction</param>
    /// <param name="ncircles_y">Number of circles in y-direction</param>
    /// <param name="distance_x">Distance of circle-centers in x direction</param>
    /// <param name="distance_y">Distance of circle-centers in y direction</param>
    public Circles(int ncircles_x, int ncircles_y, float distance_x, float distance_y) {
      _mean_distance_threshold = 1.0f;
      _number_circle_points = 60;
      _ellipse_distance = 10;
      _distance_x = distance_x;
      _distance_y = distance_y;
      _number_circle_centers = new System.Drawing.Size(ncircles_x, ncircles_y);
      _binary_threshold = 40;
      _min_contour_count = 40;
      _origin_position = EOriginPosition.TopLeft;
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
      set { 
        _distance_y = value;
        this.ObjectPoints = GenerateObjectCenters();
      }
    }

    /// <summary>
    /// Distance between circle centers in x
    /// </summary>
    [DefaultValue(100)]
    [Description("Distance between circle centers in x")]
    public float CenterDistanceX {
      get { return _distance_x; }
      set { 
        _distance_x = value;
        this.ObjectPoints = GenerateObjectCenters();
      }
    }

    /// <summary>
    /// Set the threshold for blackness, used to binarize the input image.
    /// All values less-than or equal to the treshold are made black, all others
    /// white.
    /// </summary>
    [Description("Set the threshold for blackness, used to binarize the input image." + 
                 " All values less-than or equal to the treshold are made black, all others white")]
    public int BinaryThreshold {
      get { return _binary_threshold; }
      set { _binary_threshold = value; }
    }

    /// <summary>
    /// Set minimum number of contour points to attempt an ellipse fit
    /// </summary>
    [Description("Set minimum number of contour points to attempt an ellipse fit")]
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
    [Description("Number of circle points to generate for transition detection")]
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

    [Description("Set the position of the origin")]
    public EOriginPosition OriginPosition {
      get { return _origin_position; }
      set { 
        _origin_position = value;
        this.ObjectPoints = GenerateObjectCenters();
      }
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

      if (_origin_position == EOriginPosition.Center) {
        Vector trans = Vector.Zeros(3);
        trans[0] = ((_number_circle_centers.Width - 1) * _distance_x) * 0.5;
        trans[1] = ((_number_circle_centers.Height - 1) * _distance_y) * 0.5;
        foreach (Vector v in centers) {
          v.SubtractInplace(trans);
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
          e => { return NearestEllipseCenter(finals, e); }
        ).Where(
          ne => { return Math.Sqrt(ne.dist2) < _ellipse_distance; }
        ).Select(
          ne => { return ne.center; }
        ).ToArray();

      // currently we need to detect all requested ellipses.
      return image_points.Length == _number_circle_centers.Width * _number_circle_centers.Height;

    }

    /// <summary>
    /// Result of a query to NearestEllipseResult
    /// </summary>
    struct NearestEllipseResult {
      public System.Drawing.PointF center;
      public double dist2;
    };

    /// <summary>
    /// Find the nearest ellipse-center to a query point in image space.
    /// </summary>
    /// <param name="finals">List of ellipses to test</param>
    /// <param name="ex">Query point</param>
    /// <returns>Nearest result</returns>
    private NearestEllipseResult NearestEllipseCenter(List<DetectedEllipse> finals, System.Drawing.PointF ex) {
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

    /// <summary>
    /// Generate a intrinsic/extrinsic calibration from an initial set of four marker points.
    /// </summary>
    /// <param name="finals">List of ellipses</param>
    /// <param name="marker_ids">Ellipse ids of detected marker-ellipses</param>
    /// <param name="icp">Resulting intrinsic calibration</param>
    /// <param name="ecp">Resulting extrinsic calibration</param>
    /// <param name="size">Image size</param>
    private void ApproximatePlane(List<DetectedEllipse> finals, int[] marker_ids, out Emgu.CV.IntrinsicCameraParameters icp, out Emgu.CV.ExtrinsicCameraParameters ecp, System.Drawing.Size size) {
      // Currently the marker points correspond to corner points in the rectangular pattern.
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

    /// <summary>
    /// Find the marker ellipses.
    /// </summary>
    /// <param name="gray">Binary image</param>
    /// <param name="finals">List of ellipses</param>
    /// <param name="ids">Resulting ids of marker ellipses, ordered by the number of transitions descending</param>
    /// <returns>True if all marker ellipses where found, false otherwise</returns>
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

    /// <summary>
    /// Count the number of black/white transitions for a single ellipse
    /// </summary>
    /// <param name="e">Ellipse</param>
    /// <param name="matrix">Affine ellipse frame that transforms the ellipse to a circle located at origin</param>
    /// <param name="gray">Binary image</param>
    /// <returns>The number of black/white transitions found</returns>
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
        System.Drawing.Point p = new System.Drawing.Point((int)Math.Round(x[0]), (int)Math.Round(x[1]));
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
