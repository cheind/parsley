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
    Emgu.CV.Image<Gray, byte> _marker;
    int _marker_size;

    public MarkerDetetection() {
      Emgu.CV.Image<Emgu.CV.Structure.Bgr, byte> m = new Image<Bgr, byte>("marker.bmp");
      Emgu.CV.Image<Gray, byte> gray = m.Convert<Gray, byte>();
      gray = gray.Resize(30, 30, INTER.CV_INTER_CUBIC);
      gray._ThresholdBinary(new Gray(50), new Gray(255.0));
      gray._Not();
      _marker = gray;
      _marker_size = _marker.Width;
    }

    #region IImageAlgorithm Members

    public void ProcessImage(Emgu.CV.Image<Emgu.CV.Structure.Bgr, byte> image) {
      Emgu.CV.Image<Gray, byte> gray = image.Convert<Gray, byte>();
      gray.SmoothGaussian(3);
      gray._ThresholdBinary(new Gray(50), new Gray(255.0));
      gray._Not();
      Emgu.CV.Contour<System.Drawing.Point> contour_points = gray.FindContours();

      MemStorage storage = new MemStorage();
      Matrix<double> warp = new Matrix<double>(3, 3);
      PointF[] dest = new PointF[] { 
            new PointF(0, 0),
            new PointF(0, _marker_size),
            new PointF(_marker_size, _marker_size),
            new PointF(_marker_size, 0)
            };
      Emgu.CV.Image<Gray, byte> roi = new Image<Gray,byte>(_marker_size, _marker_size);

      while (contour_points != null) {
        Contour<Point> c = contour_points.ApproxPoly(contour_points.Perimeter * 0.05, storage);
        double p = c.Perimeter;
        if (c.Total == 4 && p > 500) {
          

          PointF[] src = new PointF[] { 
            new PointF(c[0].X, c[0].Y),
            new PointF(c[1].X, c[1].Y),
            new PointF(c[2].X, c[2].Y),
            new PointF(c[3].X, c[3].Y)};

          CvInvoke.cvGetPerspectiveTransform(src, dest, warp);
          int flags = (int)INTER.CV_INTER_LINEAR + (int)WARP.CV_WARP_FILL_OUTLIERS;
          CvInvoke.cvWarpPerspective(gray, roi, warp, flags, new MCvScalar(0));

          //Emgu.CV.Image<Gray, byte> roi = gray.WarpPerspective(warp, _marker_size, _marker_size, INTER.CV_INTER_LINEAR, WARP.CV_WARP_FILL_OUTLIERS, new Gray(0));
          //roi._ThresholdBinary(new Gray(50), new Gray(255.0));
          /*for (int i = 0; i < _marker_size; ++i) {
            for (int j = 0; j < _marker_size; ++j) {
              image[i + _marker_size, j] = new Bgr(roi[i, j].Intensity, roi[i, j].Intensity, roi[i, j].Intensity);
              image[i, j] = new Bgr(_marker[i, j].Intensity, _marker[i, j].Intensity, _marker[i, j].Intensity);
            }
          }*/
         
          
          Emgu.CV.Image<Gray, float> positions = roi.MatchTemplate(_marker, TM_TYPE.CV_TM_SQDIFF_NORMED);
          double error = positions[0,0].Intensity;
          Console.WriteLine(error);
          if (error < 0.3) {
            image.DrawPolyline(c.ToArray(), true, new Bgr(Color.Green), 2);
          }
        }
        contour_points = contour_points.HNext;
      }

      /*
       * rotate
       *     // rotate image
        CvPoint2D32f center = cvPoint2D32f(frame->width / 2, frame->height / 2);
        angle = 90;
        scale = 1;
        cv2DRotationMatrix(center, angle, scale, rot_mat);
        // transform
        cvWarpAffine(frame, frame_rot, rot_mat,CV_INTER_LINEAR + CV_WARP_FILL_OUTLIERS,     cvScalarAll(0));
       * */


    }

    #endregion
  }
}
