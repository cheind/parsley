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
      Emgu.CV.Contour<System.Drawing.Point> c = gray.FindContours();
      List<Ellipse> ellipses = new List<Ellipse>();
      
      while (c != null) {
        if (c.Count() >= _min_contour_count) {
          System.Drawing.PointF[] mypoints = Array.ConvertAll(
            c.ToArray<System.Drawing.Point>(), 
            value => new System.Drawing.PointF(value.X, value.Y)
          );

          Ellipse e = Emgu.CV.PointCollection.EllipseLeastSquareFitting(mypoints);
          MCvBox2D box = e.MCvBox2D;
          box.size.Height *= 0.5f;
          box.size.Width *= 0.5f;
          Ellipse final_ellipse = new Ellipse(box);

          // Find out which dimension is bigger
          double a = final_ellipse.MCvBox2D.size.Width;
          double b = final_ellipse.MCvBox2D.size.Height;
          
          double scaling = 1.0;
          if (a > b) {
            scaling = b / a;
          } else {
            scaling = a / b;
          }
          
          Matrix scale = Matrix.Identity(3, 3);
          scale[0, 0] = scaling; // Why scale the x-axis

          Matrix translation = Matrix.Identity(3, 3);
          translation[0, 2] = final_ellipse.MCvBox2D.center.X;
          translation[1, 2] = final_ellipse.MCvBox2D.center.Y;

          Matrix rotation = Matrix.Identity(3, 3);
          double angle = final_ellipse.MCvBox2D.angle / 180 * Math.PI;
          // Rotation counterclockwise
          rotation[0, 0] = Math.Cos(-angle);
          rotation[0, 1] = -Math.Sin(-angle);
          rotation[1, 0] = Math.Sin(-angle);
          rotation[1, 1] = Math.Cos(-angle);

          Matrix m = translation * rotation * scale;
          Matrix inv = m.Inverse();
          List<double> ratings = new List<double>();
          

          double width = image.Width;
          double height = image.Height;
          
          foreach (System.Drawing.PointF p in mypoints) {
            Vector x = p.ToParsley().ToHomogeneous(1.0);
            Vector r = (inv.Multiply(x.ToColumnMatrix())).GetColumnVector(0);
            Vector closest = r.ToNonHomogeneous().Normalize().Scale(b);
            Vector closest_in_world = (m.Multiply(closest.ToHomogeneous(1.0).ToColumnMatrix())).GetColumnVector(0);         
            ratings.Add((closest_in_world - x).ToNonHomogeneous().Norm());
          }

          double median_distance = ratings.Median();
          if (median_distance < _distance_threshold) {
            ellipses.Add(final_ellipse);
          }
        }
        c = c.HNext;
      }
      
      ellipses.Sort(
        (a, b) => {
          double dista = a.MCvBox2D.center.X * a.MCvBox2D.center.X + a.MCvBox2D.center.Y * a.MCvBox2D.center.Y;
          double distb = b.MCvBox2D.center.X * b.MCvBox2D.center.X + b.MCvBox2D.center.Y * b.MCvBox2D.center.Y;
          return dista.CompareTo(distb);
        }
      );

      Bgr bgr = new Bgr(0, 255, 0);
      MCvFont f = new MCvFont(Emgu.CV.CvEnum.FONT.CV_FONT_HERSHEY_PLAIN, 0.8, 0.8);
      int count = 1;
      foreach (Ellipse e in ellipses) {
        image.Draw(e, bgr, 2);
        image.Draw(count.ToString(), ref f, new System.Drawing.Point((int)e.MCvBox2D.center.X, (int)e.MCvBox2D.center.Y), bgr);
        count++;
      }
    }
  }
}
