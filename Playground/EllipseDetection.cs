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

    public EllipseDetection() {
      _threshold = 40;
      _min_contour_count = 40;
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
          m[0,0] = Math.Cos(final_ellipse.MCvBox2D.angle);
          m[0,1] = -Math.Sin(final_ellipse.MCvBox2D.angle);
          m[0,2] = final_ellipse.MCvBox2D.center.X;
          m[1,0] = Math.Sin(final_ellipse.MCvBox2D.angle);
          m[1,1] = Math.Cos(final_ellipse.MCvBox2D.angle);
          m[1,2] = final_ellipse.MCvBox2D.center.Y;
          
          Matrix inv = m.Inverse();
          double rating = 0.0;
          double a = final_ellipse.MCvBox2D.size.Width;
          double b = final_ellipse.MCvBox2D.size.Height;

          if (a < b) {
            double tmp = a;
            a = b;
            a = tmp;
          }
          foreach (System.Drawing.PointF p in mypoints) {
            Vector x = new Vector(new double[]{p.X, p.Y, 1});
            Matrix r = inv.Multiply(x.ToColumnMatrix());

            rating += Math.Abs((Math.Pow(r[0,0]/a, 2) + Math.Pow(r[1,0]/b, 2)) - 1);
          }
          Console.WriteLine(rating);
          if (rating < 50) {
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
