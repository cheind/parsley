using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using MathNet.Numerics.LinearAlgebra;
using Emgu.CV.Structure;
using Parsley.Core.Extensions;

namespace Parsley.Examples {
  public partial class ScanningAttempt : FrameGrabberSlide {
    Parsley.Core.BrightestPixelLLE _lle;
    Core.NotParallelPlaneConstraint _constraint;
    int _channel;
    private Parsley.Draw3D.PointCloud _pointcloud;
    Emgu.CV.Image<Emgu.CV.Structure.Bgr, byte> _ref_image;
    bool _take_ref_image;
    Rectangle _model_roi;
    AVG[,] _avgs;
    MEDIAN[,] _meds;


    class AVG {
      public uint id;
      public Vector sum;
      public int count;
    };

    class MEDIAN {
      public uint id;
      public List<double> x;
      public List<double> y;
      public List<double> z;
    }


    public ScanningAttempt(Context c)
      : base(c) 
    {
      InitializeComponent();
      _lle = new Parsley.Core.BrightestPixelLLE();
      _lle.IntensityThreshold = 20;
      _channel = 2;
      _model_roi = Rectangle.Empty;

      _pointcloud = new Parsley.Draw3D.PointCloud();
      lock (Context.Viewer) {
        Context.Viewer.Add(_pointcloud);
      }

    }

    public ScanningAttempt() :base(null) {
      InitializeComponent();
    }

    protected override void OnSlidingIn() {
      Context.ROIHandler.OnROI += new Parsley.UI.Concrete.ROIHandler.OnROIHandler(ROIHandler_OnROI);
      _constraint = new Parsley.Core.NotParallelPlaneConstraint(new Core.Plane[]{Context.ReferencePlanes[0].Plane, Context.ReferencePlanes[1].Plane});
      lock (Context.Viewer) {
        Context.Viewer.SetupPerspectiveProjection(
          Core.BuildingBlocks.Perspective.FromCamera(Context.Camera, 1.0, 5000).ToInterop()
        );
        Context.Viewer.LookAt(new double[] { 0, 0, 0 }, new double[] { 0, 0, 400 }, new double[] { 0, 1, 0 });
      }
      base.OnSlidingIn();
    }

    protected override void OnSlidingOut(CancelEventArgs args) {
      Context.ROIHandler.OnROI -= new Parsley.UI.Concrete.ROIHandler.OnROIHandler(ROIHandler_OnROI);
      base.OnSlidingOut(args);
    }

    void ROIHandler_OnROI(Rectangle r) {
      _avgs = new AVG[r.Width, r.Height];
      _meds = new MEDIAN[r.Width, r.Height];

      _model_roi = r;
      
    }

    protected override void OnFrame(Parsley.Core.BuildingBlocks.FrameGrabber fp, Emgu.CV.Image<Emgu.CV.Structure.Bgr, byte> img) {

      if (_take_ref_image) {
        _ref_image = img.Copy();
        _take_ref_image = false;
      }

      // 1. Extract laser-line
      Context.Laser.FindLaserLine(img);
      PointF[] laser_points = Context.Laser.ValidLaserPoints.ToArray();

      img.Draw(_model_roi, new Bgr(Color.Green), 1);

      if (laser_points.Length < 3 || _ref_image == null || _model_roi == Rectangle.Empty) {
        return;
      }



      Core.Ray[] eye_rays = Core.Ray.EyeRays(Context.Camera.Intrinsics, laser_points);
      Vector[] eye_ray_isects = new Vector[eye_rays.Length];
      double[] min_ts = new double[eye_ray_isects.Length];


      for (int i = 0; i < laser_points.Length; ++i) {
        // Intersect with all planes and choose closest
        Core.Ray r = eye_rays[i];

        double min_t = Double.MaxValue;
        foreach (Core.BuildingBlocks.ReferencePlane p in Context.ReferencePlanes) {
          double t;
          Core.Intersection.RayPlane(r, p.Plane, out t);
          if (t < min_t) {
            min_t = t;
          }
        }
        min_ts[i] = min_t;
        eye_ray_isects[i] = r.At(min_t);
      }

      Core.Ransac<Core.PlaneModel> ransac = new Parsley.Core.Ransac<Parsley.Core.PlaneModel>(eye_ray_isects);
      Core.Ransac<Core.PlaneModel>.Hypothesis h = ransac.Run(20, (double)_nrc_distance.Value, (int)(img.Width * (float)_nrc_consensus.Value), _constraint);

      Vector z = new Vector(new double[] { 0, 0, 1 });
      if (h != null) {
        Core.Plane laser_plane = h.Model.Plane;

        if (Math.Abs(laser_plane.Normal.ScalarMultiply(z)) < 0.30) {
          Console.WriteLine(laser_plane.Normal);
          return;
        }

        lock (Context.Viewer) {
          for (int i = 0; i < laser_points.Length; ++i) {
            Point lp = new Point((int)laser_points[i].X, (int)laser_points[i].Y);
            
            if (_model_roi.Contains(lp)) {
              
              double t;
              Core.Intersection.RayPlane(eye_rays[i], laser_plane, out t);               
              
              Vector final = eye_rays[i].At(t);
              img[lp.Y, lp.X] = new Bgr(Color.Red);
              Bgr bgr = _ref_image[lp.Y, lp.X];
              Vector color = new Vector(new double[] { bgr.Red / 255.0, bgr.Green / 255.0, bgr.Blue / 255.0, 1.0 });

              //_pointcloud.AddPoint(final.ToInterop(), color.ToInterop());
              Point p_in_roi = new Point(lp.X - _model_roi.X, lp.Y - _model_roi.Y);
              /*
              AVG avg = _avgs[p_in_roi.X, p_in_roi.Y];
              if (avg == null) {
                avg = new AVG();
                avg.id = 0;
                avg.sum = final.Clone();
                avg.count = 1;
                avg.id = _pointcloud.AddPoint(final.ToInterop(), color.ToInterop());
                _avgs[p_in_roi.X, p_in_roi.Y] = avg;
              } else {
                avg.sum.AddInplace(final);
                avg.count++;
                _pointcloud.UpdatePoint(avg.id, avg.sum / avg.count, color.ToInterop());
              }*/
 
              
              MEDIAN med = _meds[p_in_roi.X, p_in_roi.Y];
              if (med == null) {
                med = new MEDIAN();
                med.x = new List<double>();
                med.y = new List<double>();
                med.z = new List<double>();
                med.x.Add(final[0]);
                med.y.Add(final[1]);
                med.z.Add(final[2]);
                med.id = _pointcloud.AddPoint(final.ToInterop(), color.ToInterop());
                _meds[p_in_roi.X, p_in_roi.Y] = med;
              } else {
                med.x.Insert(~med.x.BinarySearch(final[0]), final[0]);
                med.y.Insert(~med.y.BinarySearch(final[1]), final[1]);
                med.z.Insert(~med.z.BinarySearch(final[2]), final[2]);
                Vector v = new Vector(3, 0.0);
                v[0] = med.x[med.x.Count / 2];
                v[1] = med.y[med.y.Count / 2];
                v[2] = med.z[med.z.Count / 2];

                _pointcloud.UpdatePoint(med.id, v, color.ToInterop());
              }
            }
          }
        }
      }


    }

    PointF Backproject(Vector p, Emgu.CV.ExtrinsicCameraParameters ecp) {
      Matrix m = Matrix.Identity(4, 4);
      m.SetMatrix(0, 2, 0, 3, ecp.ExtrinsicMatrix.ToParsley());
      Matrix i = m.Inverse();
      Vector x = new Vector(new double[] { p[0], p[1], p[2], 1 });
      Matrix r = i * x.ToColumnMatrix();
      PointF[] coords = Emgu.CV.CameraCalibration.ProjectPoints(
        new MCvPoint3D32f[] { 
          new MCvPoint3D32f((float)r[0,0], (float)r[1,0], (float)r[2,0])
        },
        ecp,
        Context.Camera.Intrinsics);
      return coords[0];

    }

    private void _btn_take_ref_image_Click(object sender, EventArgs e) {
      _take_ref_image = true;
    }
  }
}
