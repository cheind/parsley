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
  public class OrderedEllipseDetection : EllipseDetection {
    Emgu.CV.Image<Bgr, Byte> _color_tab;

    public OrderedEllipseDetection() {
      Random r = new Random();
      _color_tab = new Emgu.CV.Image<Bgr, Byte>(1, 100);
      for (int i = 0; i < 100; ++i) {
        _color_tab[i, 0] = new Bgr(r.Next(255), r.Next(255), r.Next(255));
      }

    }


    public override void ProcessImage(Emgu.CV.Image<Emgu.CV.Structure.Bgr, byte> image) {
      Emgu.CV.Image<Gray, byte> gray = image.Convert<Gray, byte>();
      gray._ThresholdBinary(new Gray(this.Threshold), new Gray(255.0));
      gray._Not();

      Parsley.Core.EllipseDetector ed = new Parsley.Core.EllipseDetector();
      ed.MinimumContourCount = this.MinContourCount;

      List < Parsley.Core.DetectedEllipse > ellipses = 
        new List<Parsley.Core.DetectedEllipse>(ed.DetectEllipses(gray));

      List < Parsley.Core.DetectedEllipse > finals = 
        new List<Parsley.Core.DetectedEllipse>(
          ellipses.Where(e => { return e.Rating < this.MeanDistanceThreshold; })
        );

      
      List <System.Drawing.Point> seeds = new List<System.Drawing.Point>();
      foreach (Parsley.Core.DetectedEllipse e in finals) {
        image.Draw(e.Ellipse, new Bgr(System.Drawing.Color.Green), 2);
        seeds.Add(new System.Drawing.Point((int)e.Ellipse.MCvBox2D.center.X, (int)e.Ellipse.MCvBox2D.center.Y));
      }

      Parsley.Core.SeededBlobDetector d = new Parsley.Core.SeededBlobDetector();
      Parsley.Core.Blob[] blobs = d.DetectBlobs(
        gray,
        (from, to) => { return gray[to].Intensity == 255; },
        seeds.ToArray());

      foreach (Parsley.Core.Blob b in blobs) {
        System.Console.WriteLine(b.Pixels.Length);
        /*foreach (System.Drawing.Point p in b.Pixels) {
          image[p] = new Bgr(System.Drawing.Color.Red);
        }*/
      }

      int pattern_size_x = 4;
      int pattern_size_y = 3;
      double center_distance = 50;

      Vector[] object_points = new Vector[pattern_size_x * pattern_size_y];
      for (int y = 0; y < pattern_size_y; ++y) {
        for (int x = 0; x < pattern_size_x; x++) {
          int id = y * pattern_size_x + x;
          object_points[id] = new Vector(new double[] { x * center_distance, y * center_distance, 0 });
        }
      }

      if (finals.Count == 0)
        return;

      int[] labels = ClusterCenters(finals, pattern_size_x * pattern_size_y);
      int[] markers = FindMarkers(finals, labels, new int[] { 7, 5, 3, 2 });
      if (markers.Length != 4)
        return;

      System.Drawing.PointF[] eps = finals.Select(value => { return value.Ellipse.MCvBox2D.center; }).ToArray();
      Emgu.CV.PlanarSubdivision subdivision = new Emgu.CV.PlanarSubdivision(eps);

      Emgu.CV.IntrinsicCameraParameters icp;
      Emgu.CV.ExtrinsicCameraParameters ecp;

      Vector[] object_markers = new Vector[4]{object_points[0], object_points[1], object_points[pattern_size_x], object_points[pattern_size_x + 1]};
      System.Drawing.PointF[] image_markers = new System.Drawing.PointF[]{eps[markers[0]], eps[markers[1]], eps[markers[2]], eps[markers[3]]};
      FindCalibration(object_markers, image_markers, out icp, out ecp, image.Size);

      // For each object point find closest image point
      List<Vector> final_object_points = new List<Vector>();
      List<System.Drawing.PointF> final_image_points = new List<System.Drawing.PointF>();


      for (int i = 0; i < object_points.Length; ++i) {
        Vector o = object_points[i];
        // Estimate position of o in image
        System.Drawing.PointF[] coords = Emgu.CV.CameraCalibration.ProjectPoints(
          new MCvPoint3D32f[] { o.ToEmguF() },
          ecp, icp
        );
        // Find closest

        double dist2;
        System.Drawing.PointF nearest = FindNearest(coords[0], eps, out dist2);

        // Calculate distance
        System.Console.WriteLine(Math.Sqrt(dist2));
        if (Math.Sqrt(dist2) < 10) {
          final_object_points.Add(o);
          final_image_points.Add(nearest);
        }
      }
      // Reestimate calibration
      //FindCalibration(final_object_points.ToArray(), final_image_points.ToArray(), out icp, out ecp, image.Size);

      Bgr bgr = new Bgr(System.Drawing.Color.Green);
      MCvFont f = new MCvFont(Emgu.CV.CvEnum.FONT.CV_FONT_HERSHEY_PLAIN, 0.8, 0.8);
      int count = 1;
      foreach (System.Drawing.PointF point in final_image_points) {
        image.Draw(new CircleF(point, 4), bgr, 2);
        image.Draw(count.ToString(), ref f, new System.Drawing.Point((int)point.X + 5, (int)point.Y - 5), bgr);
        count++;
      }
    }

    private System.Drawing.PointF FindNearest(System.Drawing.PointF x, System.Drawing.PointF[] eps, out double dist2) {
      dist2 = Double.MaxValue;
      System.Drawing.PointF nearest = eps.First();
      foreach (System.Drawing.PointF p in eps) {
        double cur_dist2 = (x.X - p.X) * (x.X - p.X) + (x.Y - p.Y) * (x.Y - p.Y);
        if (cur_dist2 < dist2) {
          dist2 = cur_dist2;
          nearest = p;
        }
      }
      return nearest;
    }

    private void FindCalibration(Vector[] object_markers, System.Drawing.PointF[] image_markers, out Emgu.CV.IntrinsicCameraParameters icp, out Emgu.CV.ExtrinsicCameraParameters ecp, System.Drawing.Size size) {
      Parsley.Core.IntrinsicCalibration ic = new Parsley.Core.IntrinsicCalibration(object_markers, size);
      ic.AddView(image_markers);
      icp = ic.Calibrate();
      Parsley.Core.ExtrinsicCalibration ec = new Parsley.Core.ExtrinsicCalibration(object_markers, icp);
      ecp = ec.Calibrate(image_markers);
    }

    private int[] FindMarkers(List<Parsley.Core.DetectedEllipse> finals, int[] labels, int[] center_count) {
      List<int> ids = new List<int>();
      for (int i = 0; i < center_count.Length; ++i) {
        int id = FindEllipse(finals, labels, center_count[i]);
        if (id > -1) {
          ids.Add(id);
        }
      }
      return ids.ToArray();
    }

    private int FindEllipse(List<Parsley.Core.DetectedEllipse> finals, int[] labels, int count) {
      int label = -1;
      for (int i = 0; i <= labels.Max(); ++i) {
        if (labels.Count(value => { return value == i; }) == count) {
          label = i;
          break;
        }
      }
      if (label >= 0) {
        double max_area = 0.0;
        int id = -1;
        for (int i = 0; i < finals.Count; ++i) {
          if (labels[i] == label) {
            double area = finals[i].Ellipse.MCvBox2D.size.Width * finals[i].Ellipse.MCvBox2D.size.Height;
            if (area > max_area) {
              max_area = area;
              id = i;
            }
          }
        }
        return id;
      }
      return -1;
    }

    private int[] ClusterCenters(List<Parsley.Core.DetectedEllipse> finals, int max_clusters) {
      Emgu.CV.Matrix<float> samples = new Emgu.CV.Matrix<float>(finals.Count, 2);
      for (int i = 0; i < finals.Count; ++i) {
        samples[i, 0] = finals[i].Ellipse.MCvBox2D.center.X;
        samples[i, 1] = finals[i].Ellipse.MCvBox2D.center.Y;
      }
      Emgu.CV.Matrix<int> labels = new Emgu.CV.Matrix<int>(finals.Count, 1);
      Emgu.CV.CvInvoke.cvKMeans2(samples, max_clusters, labels, new MCvTermCriteria(3, 5.0), 2, IntPtr.Zero, 0, IntPtr.Zero, IntPtr.Zero);

      int[] l = new int[finals.Count];
      for (int i = 0; i < finals.Count; ++i) {
        l[i] = labels[i, 0];
      }
      return l;
    }
  }
}
