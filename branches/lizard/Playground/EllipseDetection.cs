using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

using Emgu.CV.Structure;
using MathNet.Numerics.LinearAlgebra;
using Parsley.Core.Extensions;

namespace Playground {

  [Parsley.Core.Addins.Addin]
  public class EllipseDetection : Parsley.Core.IImageAlgorithm {

    private int _threshold;
    private int _min_contour_count;
    private double _distance_threshold;

    public EllipseDetection() {
      _threshold = 40;
      _min_contour_count = 40;
      _distance_threshold = 1;
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

    public double MedianDistanceThreshold {
      get { return _distance_threshold; }
      set { _distance_threshold = value; }
    }

    public void ProcessImage(Emgu.CV.Image<Emgu.CV.Structure.Bgr, byte> image) {
      Emgu.CV.Image<Gray, byte> gray = image.Convert<Gray, byte>();
      gray._ThresholdBinary(new Gray(_threshold), new Gray(255.0));
      gray._Not();

      Parsley.Core.EllipseDetector ed = new Parsley.Core.EllipseDetector();
      ed.MinimumContourCount = _min_contour_count;

      List < Parsley.Core.DetectedEllipse > ellipses = 
        new List<Parsley.Core.DetectedEllipse>(ed.DetectEllipses(gray));

      List < Parsley.Core.DetectedEllipse > finals = 
        new List<Parsley.Core.DetectedEllipse>(
          ellipses.Where(e => { return e.Rating < _distance_threshold; })
        );

      finals.Sort(
        (a, b) => {
          double dista = a.Ellipse.MCvBox2D.center.X * a.Ellipse.MCvBox2D.center.X + a.Ellipse.MCvBox2D.center.Y * a.Ellipse.MCvBox2D.center.Y;
          double distb = b.Ellipse.MCvBox2D.center.X * b.Ellipse.MCvBox2D.center.X + b.Ellipse.MCvBox2D.center.Y * b.Ellipse.MCvBox2D.center.Y;
          return dista.CompareTo(distb);
        }
      );

      Bgr bgr = new Bgr(0, 255, 0);
      MCvFont f = new MCvFont(Emgu.CV.CvEnum.FONT.CV_FONT_HERSHEY_PLAIN, 0.8, 0.8);
      int count = 1;
      foreach (Parsley.Core.DetectedEllipse e in finals) {
        image.Draw(e.Ellipse, bgr, 2);
        image.Draw(count.ToString(), ref f, new System.Drawing.Point((int)e.Ellipse.MCvBox2D.center.X, (int)e.Ellipse.MCvBox2D.center.Y), bgr);
        count++;
      }
    }
  }
}
