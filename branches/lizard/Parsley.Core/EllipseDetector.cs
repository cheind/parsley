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
using Parsley.Core.Extensions;
using System.ComponentModel;

namespace Parsley.Core {

  /// <summary>
  /// Contains a single fitted ellipse.
  /// </summary>
  public struct DetectedEllipse {
    private Emgu.CV.Contour<System.Drawing.Point> _contour;
    private Emgu.CV.Structure.Ellipse _ellipse;
    private double _rating;

    /// <summary>
    /// Construct from values.
    /// </summary>
    /// <param name="contour"></param>
    /// <param name="ellipse"></param>
    /// <param name="rating"></param>
    public DetectedEllipse(
      Emgu.CV.Contour<System.Drawing.Point> contour,
      Emgu.CV.Structure.Ellipse ellipse,
      double rating) {
      _contour = contour;
      _ellipse = ellipse;
      _rating = rating;
    }

    /// <summary>
    /// The ellipse fitted in a least squares sense through all contour points.
    /// </summary>
    /// <remarks>The ellipse is described by a rotated
    /// rectangle consisting of the rotation angle in degrees and
    /// half the width / height of the box that circumscribes the ellipse.</remarks>
    public Emgu.CV.Structure.Ellipse Ellipse {
      get { return _ellipse; }
    }

    /// <summary>
    /// Goodness of fit
    /// </summary>
    /// <remarks>The rating is in the range of [0,inf] and
    /// describes the average distance from the contour points to 
    /// the fitted ellipse.</remarks>
    public double Rating {
      get { return _rating; }
    }

    /// <summary>
    /// Contour points from which the ellipse was fit
    /// </summary>
    public Emgu.CV.Contour<System.Drawing.Point> Contour {
      get { return _contour; }
    }
  };

  /// <summary>
  /// Detect ellipses in a grayscale image.
  /// </summary>
  /// <remarks>
  /// This algorithm takes as input a binary image and detects ellipses.
  /// It is assumed that the ellipses are white and background is black.
  /// </remarks>
  public class EllipseDetector {

    public EllipseDetector(int min_contour_count) {
      _min_contour_count = min_contour_count;
    }

    public EllipseDetector()
      : this(40) { }

    private int _min_contour_count;
    /// <summary>
    /// Set minimum number of contour points to attempt ellipse fit
    /// </summary>
    [Description("Set minimum number of contour points to attempt ellipse fit")]
    public int MinimumContourCount {
      get { return _min_contour_count; }
      set { _min_contour_count = value; }
    }

    /// <summary>
    /// Detect ellipses in image
    /// </summary>
    /// <param name="image">Binary image with white foreground</param>
    /// <returns>List of ellipses found</returns>
    public DetectedEllipse[] DetectEllipses(Emgu.CV.Image<Emgu.CV.Structure.Gray, byte> image) {
      List<DetectedEllipse> ellipses = new List<DetectedEllipse>();
      Emgu.CV.Contour<System.Drawing.Point> c = image.FindContours();

      while (c != null) {
        if (c.Count() >= _min_contour_count) {
          Emgu.CV.Structure.Ellipse e = FitByContour(c);
          double rating = GoodnessOfFit(e, c);
          ellipses.Add(new DetectedEllipse(c, e, rating));
        }
        c = c.HNext;
      }

      return ellipses.ToArray();
    }

    /// <summary>
    /// Fit ellipse through contour points in a least square sense.
    /// </summary>
    /// <param name="c">Contour</param>
    /// <returns>Ellipse</returns>
    private Emgu.CV.Structure.Ellipse FitByContour(Emgu.CV.Contour<System.Drawing.Point> c) {
      System.Drawing.PointF[] mypoints = Array.ConvertAll(
        c.ToArray<System.Drawing.Point>(),
        value => new System.Drawing.PointF(value.X, value.Y)
      );

      Emgu.CV.Structure.Ellipse e = Emgu.CV.PointCollection.EllipseLeastSquareFitting(mypoints);
      Emgu.CV.Structure.MCvBox2D box = e.MCvBox2D;
      // Modify ellipse to have width = a, height = b in the ellipse equation.
      box.size.Height *= 0.5f;
      box.size.Width *= 0.5f;
      return new Emgu.CV.Structure.Ellipse(box);
    }

    /// <summary>
    /// Get the affine matrix ellipse frame. Frame is with respect to world frame and
    /// scales the ellipse to become a circle of radius b, where b is the height of
    /// the rotated rect that contains the ellipse.
    /// </summary>
    public static Matrix GetAffineFrame(Emgu.CV.Structure.Ellipse e) {
      // The required scaling is given by the ratio of the extensions
      // of the ellipse main axes.

      double a = e.MCvBox2D.size.Width;
      double b = e.MCvBox2D.size.Height;
      double scaling = 1.0;
      if (a > b) {
        scaling = b / a;
      } else {
        scaling = a / b;
      }

      Matrix scale = Matrix.Identity(3, 3);
      scale[0, 0] = scaling;

      // Translation is simply the center of the ellipse

      Matrix translation = Matrix.Identity(3, 3);
      translation[0, 2] = e.MCvBox2D.center.X;
      translation[1, 2] = e.MCvBox2D.center.Y;

      // The rotation is the angle of the rotated rect with the x-axis
      // This rotation transform is equal to rotating a frame in reverse
      // direction.

      Matrix rotation = Matrix.Identity(3, 3);
      double angle = e.MCvBox2D.angle / 180 * Math.PI;
      rotation[0, 0] = Math.Cos(-angle);
      rotation[0, 1] = -Math.Sin(-angle);
      rotation[1, 0] = Math.Sin(-angle);
      rotation[1, 1] = Math.Cos(-angle);

      // The pose of the ellipse coordinate frame with respect
      // to the world (image) coordinate frame is given by
      Matrix m = translation * rotation * scale;

      return m;
    }


    /// <summary>
    /// Evaluate goodness of fit.
    /// </summary>
    /// <remarks>
    /// Calculates the average distance of all contour points to the ellipse. The distance of point to
    /// an ellipse is given by calculating the point on the ellipse that is closest to the query. This calculation
    /// is performed by transforming the point into a coordinate system where the ellipse is in main-pose
    /// (x-axis points toward a, y-axis points toward b, the origin is the center of the ellipse). Additionally,
    /// the coordinate system is crafted in such a way (non-uniform scaling) that the ellipse becomes a circle.
    /// Then, the closest point is simply the closest point on the circle. This point is transformed back into 
    /// the world coordinate system where the distance between the query and the returned point is computed.
    /// </remarks>
    /// <param name="e">Ellipse</param>
    /// <param name="c">Contour points</param>
    /// <returns>Goodness of fit</returns>
    private double GoodnessOfFit(Emgu.CV.Structure.Ellipse e, Emgu.CV.Contour<System.Drawing.Point> c) {
      Matrix m = EllipseDetector.GetAffineFrame(e);
      Matrix inv = m.Inverse();
      List<double> distances = new List<double>();

      double avg_distance = 0.0;
      foreach (System.Drawing.Point p in c) {
        // Bring the point into the ellipse coordinate frame
        Vector x = p.ToParsley().ToHomogeneous(1.0);
        Vector r = (inv.Multiply(x.ToColumnMatrix())).GetColumnVector(0);
        // Find the closest point on the circle to r
        // From the above scaling construction the resulting circle has radius b
        Vector closest = r.ToNonHomogeneous().Normalize().Scale(e.MCvBox2D.size.Height);
        // Transform the closest point back
        Vector closest_in_world = (m.Multiply(closest.ToHomogeneous(1.0).ToColumnMatrix())).GetColumnVector(0);
        // Calculate the squared distance between the query and the point.
        avg_distance += (closest_in_world - x).ToNonHomogeneous().SquaredNorm();
      }
      return Math.Sqrt(avg_distance / c.Count());
    }
  }
}