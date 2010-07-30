using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;


using Emgu.CV.Structure;
using Emgu.CV;
using Emgu.CV.CvEnum;
using System.ComponentModel;
using MathNet.Numerics.LinearAlgebra;
using System.Drawing.Design;

namespace Parsley.Core.CalibrationPatterns {

  /// <summary>
  /// A calibration pattern based on markers.
  /// </summary>
  /// <remarks>
  /// The pattern to be detected is a user-defined square binary image that has a thick black border and 
  /// a non-symmetric black/white interior. The marker is provided as an ordinary image. Additionally, 
  /// the length of pattern in the real world in units of your choice (e.g millimeter) needs to be configured.
  /// The calibration yields four correspondences (corner pixels of the marker) when successful.
  /// 
  /// The algorithm works by first binarizing the image into black and white. On this image the contours are extracted.
  /// For each extracted contour an poly-line through the contour is approximated. If the poly-line has exactly four vertices,
  /// the algorithm treats the poly-line as a possible marker border. For each marker hypothesis, the pixels contained in 
  /// inside the  poly-line are back-projected to a square image of the same size (in pixels) as the marker image. This image 
  /// is called 'warped image'. Since the contour points do not empose a constant ordering, there are four possibilities for 
  /// the warped image to align with the marker image, namely: a rotation by 0°, 90°, 180°, 270°. For possible configuration 
  /// the marker image is matched with the transformed warped image. The best one (in terms of the normed sum of squared 
  /// pixel-intensity differences) is chosen if the error is below a predefined error. The best orientation is then used
  /// to order the corner pixels accordingly.
  /// </remarks>
  [Serializable]
  [Parsley.Core.Addins.Addin]
  public class Marker : Core.CalibrationPattern {
    private Image<Bgr, byte> _marker;
    private Image<Gray, byte> _binary_marker, _warped, _tmp;
    private int _marker_size;
    private double _marker_length, _max_error_normed;
    private int _binary_threshold;
    private MemStorage _contour_storage;
    private object _sync;
    Matrix<double> _warp_matrix;
    PointF[] _warp_dest;

    /// <summary>
    /// Default constructor
    /// </summary>
    public Marker() {
      _marker = null;
      _binary_marker = null;
      _warped = null;
      _tmp = null;
      _marker_size = 0;
      _marker_length = 50;
      _max_error_normed = 0.4;
      _binary_threshold = 60;
      _warp_matrix = new Matrix<double>(3, 3);
      _contour_storage = new MemStorage();
      _sync = new object();
      this.ObjectPoints = UpdateObjectPoints();
    }

    /// <summary>
    /// Access the marker image used as pattern
    /// </summary>
    [Description("Get/Set the marker image used as pattern")]
    [Editor(typeof(Core.ImageTypeEditor), typeof(UITypeEditor))]
    public Image<Bgr, byte> MarkerImage {
      get { return _marker; }
      set {
        _marker = value; UpdateMarker();
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
    /// Get/Set the length of the marker
    /// </summary>
    [Description("Length of marker")]
    public double MarkerLength {
      get { return _marker_length; }
      set { _marker_length = value; this.ObjectPoints = UpdateObjectPoints(); }
    }

    /// <summary>
    /// Get/Set the maximum allowed error for pattern detection.
    /// </summary>
    [Description("Get/Set the maximum allowed error for pattern detection.")]
    public double MaximumErrorNormed {
      get { return _max_error_normed; }
      set { _max_error_normed = value; }
    }

    /// <summary>
    /// Update object reference points.
    /// </summary>
    /// <returns></returns>
    private Vector[] UpdateObjectPoints() {

      // 0   1
      // +---+
      // |   |
      // +---+
      // 2   3
      return new Vector[] {
        new Vector(new double[]{0.0, 0.0, 0.0}),
        new Vector(new double[]{_marker_length, 0.0, 0.0}),
        new Vector(new double[]{_marker_length, _marker_length, 0.0}),
        new Vector(new double[]{0.0, _marker_length, 0.0})
      };
    }

    /// <summary>
    /// Update associated marker structures
    /// </summary>
    /// <param name="marker_size"></param>
    private void UpdateMarker() {
      if (_marker == null)
        return;
      lock (_sync) {
        _binary_marker = _marker.Convert<Gray, byte>();
        _marker_size = _binary_marker.Width;

        // Warp points are specified in counter-clockwise order
        // since cvApproxpoly seems to return poly-points in counter-clockwise order.
        //
        // 0   3
        // +---+
        // |   |
        // +---+
        // 1   2
        _warp_dest = new PointF[] { 
          new PointF(0, 0),
          new PointF(0, _marker_size),
          new PointF(_marker_size, _marker_size),
          new PointF(_marker_size, 0)
        };

        // Storage to hold warped image
        _warped = new Image<Gray, byte>(_marker_size, _marker_size);
        // Storage to test orientations
        _tmp = new Image<Gray, byte>(_marker_size, _marker_size);
      }
    }


    public override bool FindPattern(Image<Gray, byte> img, out PointF[] image_points) {
      image_points = null;

      if (_marker == null) {
        return false;
      }

      Contour<Point> contour_points;
      // Find contour points in black/white image
      using (Image<Gray, byte> binary = new Image<Gray, byte>(img.Size)) {
        CvInvoke.cvThreshold(img, binary, _binary_threshold, 255, THRESH.CV_THRESH_BINARY | THRESH.CV_THRESH_OTSU);
        binary._Not(); // Contour is searched on white points, marker envelope is black.
        contour_points = binary.FindContours();
      }
      lock (_sync) { // Lock here in case the pattern is changed while using it.
        bool marker_found = false;
        double best_error = _max_error_normed;
        while (contour_points != null) {
          // Approximate contour points by poly-lines.
          // For our marker-envelope should generate a poly-line consisting of four vertices.
          Contour<Point> c = contour_points.ApproxPoly(contour_points.Perimeter * 0.05, _contour_storage);
          if (c.Total == 4 && c.Perimeter > 200) {

            // Warp content of poly-line as if looking at it from the top
            PointF[] warp_source = new PointF[] { 
              new PointF(c[0].X, c[0].Y),
              new PointF(c[1].X, c[1].Y),
              new PointF(c[2].X, c[2].Y),
              new PointF(c[3].X, c[3].Y)
            };

            CvInvoke.cvGetPerspectiveTransform(warp_source, _warp_dest, _warp_matrix);
            CvInvoke.cvWarpPerspective(
              img, _warped, _warp_matrix,
              (int)INTER.CV_INTER_CUBIC + (int)WARP.CV_WARP_FILL_OUTLIERS,
              new MCvScalar(0)
            );
            CvInvoke.cvThreshold(_warped, _warped, _binary_threshold, 255, THRESH.CV_THRESH_BINARY | THRESH.CV_THRESH_OTSU);

            // Perform a template matching with the stored pattern in order to
            // determine if content of the envelope matches the stored pattern and
            // determine the orientation of the pattern in the image.
            // Orientation is encoded
            // 0: 0°, 1: 90°, 2: 180°, 3: 270°
            double error;
            int orientation;
            MatchPattern(out error, out orientation);

            if (error <  best_error) {
              best_error = error;
              int id_0 = orientation;
              int id_1 = (orientation + 1) % 4;
              int id_2 = (orientation + 2) % 4;
              int id_3 = (orientation + 3) % 4;

              // ids above are still counterclockwise ordered, we need to permute them
              // 0   3    0   1
              // +---+    +---+
              // |   | -> |   |
              // +---+    +---+
              // 1   2    2   3

              image_points = new PointF[4];
              image_points[0] = c[id_0];
              image_points[1] = c[id_3];
              image_points[2] = c[id_1];
              image_points[3] = c[id_2];

              marker_found = true;
            }
          }
          contour_points = contour_points.HNext;
        }
        return marker_found;
      }
    }

    private void MatchPattern(out double error, out int orientation) {
      // 0 degrees
      orientation = 0;
      error = _warped.MatchTemplate(_binary_marker, TM_TYPE.CV_TM_SQDIFF_NORMED)[0, 0].Intensity;

      // 90 degrees
      CvInvoke.cvTranspose(_warped, _tmp);
      CvInvoke.cvFlip(_tmp, IntPtr.Zero, 1); // y-axis 
      double err = _tmp.MatchTemplate(_binary_marker, TM_TYPE.CV_TM_SQDIFF_NORMED)[0, 0].Intensity;
      if (err < error) {
        error = err;
        orientation = 1;
      }

      // 180 degrees
      CvInvoke.cvFlip(_warped, _tmp, -1);
      err = _tmp.MatchTemplate(_binary_marker, TM_TYPE.CV_TM_SQDIFF_NORMED)[0, 0].Intensity;
      if (err < error) {
        error = err;
        orientation = 2;
      }

      // 270 degrees
      CvInvoke.cvTranspose(_warped, _tmp);
      CvInvoke.cvFlip(_tmp, IntPtr.Zero, 0); // x-axis 
      err = _tmp.MatchTemplate(_binary_marker, TM_TYPE.CV_TM_SQDIFF_NORMED)[0, 0].Intensity;
      if (err < error) {
        error = err;
        orientation = 3;
      }
    }


  }
}
