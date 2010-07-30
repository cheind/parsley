using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Emgu.CV.Structure;
using System.Drawing;
using Emgu.CV;

using Parsley.Core.Extensions;
using Emgu.CV.CvEnum;

namespace Playground {
  [Parsley.Core.Addins.Addin]
  public class MarkerDetetection : Parsley.Core.IImageAlgorithm {

    enum Orientation {
      Degrees0,
      Degrees90,
      Degrees180,
      Degrees270
    }

    Emgu.CV.Image<Gray, byte> _marker, _roi, _tmp;
    int _marker_size;
    PointF[] _dest;

    public MarkerDetetection() {
      Emgu.CV.Image<Emgu.CV.Structure.Bgr, byte> m = new Image<Bgr, byte>("marker2.bmp");
      Emgu.CV.Image<Gray, byte> gray = m.Convert<Gray, byte>();
      gray = gray.Resize(30, 30, INTER.CV_INTER_LINEAR);
      gray._ThresholdBinary(new Gray(50), new Gray(255.0));
      _marker = gray;
      _marker_size = _marker.Width;

      _dest = new PointF[] { 
            new PointF(0, 0),
            new PointF(0, _marker_size),
            new PointF(_marker_size, _marker_size),
            new PointF(_marker_size, 0)
            };

      _roi = new Image<Gray,byte>(_marker_size, _marker_size);
      _tmp = new Image<Gray, byte>(_marker_size, _marker_size);
    }

    #region IImageAlgorithm Members

    public void ProcessImage(Emgu.CV.Image<Emgu.CV.Structure.Bgr, byte> image) {
      Emgu.CV.Image<Gray, byte> gray = image.Convert<Gray, byte>();

      

      Emgu.CV.Image<Gray, byte> binary  = new Image<Gray,byte>(image.Size);
      CvInvoke.cvThreshold(gray, binary, 40, 255, THRESH.CV_THRESH_BINARY | THRESH.CV_THRESH_OTSU);
      binary._Not();
      Emgu.CV.Contour<System.Drawing.Point> contour_points = binary.FindContours();
      
      MemStorage storage = new MemStorage();
      Matrix<double> warp = new Matrix<double>(3, 3);

      while (contour_points != null) {
        Contour<Point> c = contour_points.ApproxPoly(contour_points.Perimeter * 0.05, storage);
        double p = c.Perimeter;
        if (c.Total == 4 && p > 300) {
          
          PointF[] src = new PointF[] { 
            new PointF(c[0].X, c[0].Y),
            new PointF(c[1].X, c[1].Y),
            new PointF(c[2].X, c[2].Y),
            new PointF(c[3].X, c[3].Y)};

          CvInvoke.cvGetPerspectiveTransform(src, _dest, warp);
          int flags = (int)INTER.CV_INTER_LINEAR + (int)WARP.CV_WARP_FILL_OUTLIERS;
          CvInvoke.cvWarpPerspective(gray, _roi, warp, flags, new MCvScalar(0));


          double min_error;
          Orientation orient;

          FindBestOrientation(out min_error, out orient);
          if (min_error < 0.4) {
            image.DrawPolyline(c.ToArray(), true, new Bgr(Color.Green), 2);
            System.Console.WriteLine(min_error + " " + orient);

            switch (orient) {
              case Orientation.Degrees0:
                image.Draw(new LineSegment2D(c[0], c[3]), new Bgr(System.Drawing.Color.Red), 2);
                break;
              case Orientation.Degrees90:
                image.Draw(new LineSegment2D(c[1], c[0]), new Bgr(System.Drawing.Color.Red), 2);
                break;
              case Orientation.Degrees180:
                image.Draw(new LineSegment2D(c[2], c[1]), new Bgr(System.Drawing.Color.Red), 2);
                break;
              case Orientation.Degrees270:
                image.Draw(new LineSegment2D(c[3], c[2]), new Bgr(System.Drawing.Color.Red), 2);
                break;
            }
            


          }

          // 0 degrees
        }
        contour_points = contour_points.HNext;
      }
    }

    private void FindBestOrientation(out double min_error, out Orientation orient) {

      

      // 0 degrees
      orient = Orientation.Degrees0;
      min_error = _roi.MatchTemplate(_marker, TM_TYPE.CV_TM_SQDIFF_NORMED)[0,0].Intensity;

      // 90 degrees
      CvInvoke.cvTranspose(_roi, _tmp);
      CvInvoke.cvFlip(_tmp, IntPtr.Zero, 1); // y-axis 
      double error = _tmp.MatchTemplate(_marker, TM_TYPE.CV_TM_SQDIFF_NORMED)[0, 0].Intensity;
      if (error < min_error) {
        min_error = error;
        orient = Orientation.Degrees90;
      }

      //180 degrees
      CvInvoke.cvFlip(_roi, _tmp, -1);
      error = _tmp.MatchTemplate(_marker, TM_TYPE.CV_TM_SQDIFF_NORMED)[0, 0].Intensity;
      if (error < min_error) {
        min_error = error;
        orient = Orientation.Degrees180;
      }

      //270 degrees
      CvInvoke.cvTranspose(_roi, _tmp);
      CvInvoke.cvFlip(_tmp, IntPtr.Zero, 0); // x-axis 
      error = _tmp.MatchTemplate(_marker, TM_TYPE.CV_TM_SQDIFF_NORMED)[0, 0].Intensity;
      if (error < min_error) {
        min_error = error;
        orient = Orientation.Degrees270;
      }

    }

    #endregion
  }
}
