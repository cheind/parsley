using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

using Emgu.CV.Structure;
using MathNet.Numerics.LinearAlgebra;

namespace Playground {

  [Parsley.Core.Addins.Addin]
  public class EllipseDetection : Parsley.Core.IImageAlgorithm {

    private int _threshold;
    private int _min_contour_count;
    private double _distance_threshold;



    public EllipseDetection() {
      _threshold = 40;
      _min_contour_count = 40;
      _distance_threshold = 0.3;
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

      //Emgu.CV.Contour<System.Drawing.Point> c = gray.FindContours(Emgu.CV.CvEnum.CHAIN_APPROX_METHOD.CV_CHAIN_CODE, Emgu.CV.CvEnum.RETR_TYPE.CV_RETR_LIST);
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

          Matrix m = Matrix.Identity(3,3);
          double a = final_ellipse.MCvBox2D.size.Width;
          double b = final_ellipse.MCvBox2D.size.Height;
          double s = a / b;
          Matrix scale = Matrix.Identity(3,3);
          scale[0, 0] = s;
          double angle = final_ellipse.MCvBox2D.angle / 180 * Math.PI;
          m[0, 0] = Math.Cos(angle);
          m[0, 1] = -Math.Sin(angle);
          m[0,2] = final_ellipse.MCvBox2D.center.X;
          m[1, 0] = Math.Sin(angle);
          m[1, 1] = Math.Cos(angle);
          m[1,2] = final_ellipse.MCvBox2D.center.Y;

          m = scale * m;
          Matrix inv = m.Inverse();
          List<double> ratings = new List<double>();
          

          double width = image.Width;
          double height = image.Height;
          
          foreach (System.Drawing.PointF p in mypoints) {
            Vector x = new Vector(new double[]{p.X, p.Y, 1});
            Vector r = (inv.Multiply(x.ToColumnMatrix())).GetColumnVector(0).Normalize();
            Vector closest = r * a;

            Vector closest_in_world = (m.Multiply(closest.ToColumnMatrix())).GetColumnVector(0);


            System.Drawing.Point mypoint = new System.Drawing.Point((int)closest_in_world[0], (int)closest_in_world[1]);
            if (mypoint.X >= 0 && mypoint.Y >= 0 && mypoint.X < width && mypoint.Y < height) {
              image[mypoint.Y, mypoint.X] = new Bgr(0, 0, 255);
              image[(int)p.Y, (int)p.X] = new Bgr(0, 255, 255);
            }

            Vector distance_in_world = closest_in_world - x;

            ratings.Add(distance_in_world.Norm());
          }

          ratings.Sort();
          double rating;
          if (ratings.Count % 2 == 0) {
            rating = (ratings[ratings.Count/2] + ratings[ratings.Count/2+1]) * 0.5;
          } else {
            rating = ratings[(ratings.Count+1)/2];
          }
          //Console.WriteLine(rating);
          if (rating < _distance_threshold) {
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
