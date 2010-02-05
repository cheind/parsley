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
using System.IO;

namespace Parsley.Examples {
  public partial class ScanningAttempt : FrameGrabberSlide {
    Parsley.Core.BrightestPixelLLE _lle;
    Core.NotParallelPlaneConstraint _constraint;
    private Parsley.Draw3D.PointCloud _pointcloud;
    Emgu.CV.Image<Emgu.CV.Structure.Bgr, byte> _ref_image;
    bool _take_ref_image;
    Core.PointPerPixelAccumulator _acc;

    public ScanningAttempt(Context c)
      : base(c) 
    {
      InitializeComponent();
      _lle = new Parsley.Core.BrightestPixelLLE();
      _lle.IntensityThreshold = 20;

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
          Core.BuildingBlocks.Perspective.FromCamera(Context.World.Camera, 1.0, 5000).ToInterop()
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
      if (r != Rectangle.Empty) {
        _acc = new Core.MedianPointAccumulator(r, 100);
      }
    }

    protected override void OnFrame(Parsley.Core.BuildingBlocks.FrameGrabber fp, Emgu.CV.Image<Emgu.CV.Structure.Bgr, byte> img) {

      if (_take_ref_image) {
        _ref_image = img.Copy();
        _take_ref_image = false;
      }

      // 1. Extract laser-line
      Context.World.Laser.FindLaserLine(img);
      PointF[] laser_points = Context.World.Laser.ValidLaserPoints.ToArray();

      if (_acc != null) {
        img.Draw(_acc.ROI, new Bgr(Color.Green), 1);
      }

      if (laser_points.Length < 3 || _ref_image == null || _acc == null) {
        return;
      }

      Core.Ray[] eye_rays = Core.Ray.EyeRays(Context.World.Camera.Intrinsics, laser_points);
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
      int min_consensus = (int)Math.Max(laser_points.Length * (float)_nrc_consensus.Value, img.Width * 0.1);
      Core.Ransac<Core.PlaneModel>.Hypothesis h = ransac.Run(30, (double)_nrc_distance.Value, min_consensus, _constraint);

      Vector z = new Vector(new double[] { 0, 0, 1 });
      if (h != null) {
        Core.Plane laser_plane = h.Model.Plane;

        if (Math.Abs(laser_plane.Normal.ScalarMultiply(z)) < 0.3) {
          Console.WriteLine(laser_plane.Normal);
          return;
        }

        /*
        using (TextWriter tw = new StreamWriter(String.Format("distances{0}.txt", Guid.NewGuid()))) {
          for (int i = 0; i < laser_points.Length; ++i) {
            if (h.ConsensusIds.Contains(i)) {
              double t;
              Core.Intersection.RayPlane(eye_rays[i], laser_plane, out t);
              tw.WriteLine(String.Format("{0} {1} {2}", laser_points[i].X, (eye_rays[i].At(t) - eye_ray_isects[i]).Norm(), Math.Abs(t-min_ts[i])));
            }
          }
        }
         */





        lock (Context.Viewer) {
          for (int i = 0; i < laser_points.Length; ++i) {
            Point lp = new Point((int)laser_points[i].X, (int)laser_points[i].Y);

            if (_acc.ROI.Contains(lp)) {

              double t;
              Core.Intersection.RayPlane(eye_rays[i], laser_plane, out t);

              img[lp.Y, lp.X] = new Bgr(Color.Red);
              Bgr bgr = _ref_image[lp.Y, lp.X];
              Vector color = new Vector(new double[] { bgr.Red / 255.0, bgr.Green / 255.0, bgr.Blue / 255.0, 1.0 });

              //_pointcloud.AddPoint(final.ToInterop(), color.ToInterop());
              Point p_in_roi = _acc.MakeRelativeToROI(lp);
              bool first;
              _acc.Accumulate(p_in_roi, eye_rays[i], t, out first);
              if (first) {
                _acc.SetId(p_in_roi, _pointcloud.AddPoint(_acc.Extract(p_in_roi).ToInterop(), color.ToInterop()));
              } else {
                _pointcloud.UpdatePoint(_acc.GetId(p_in_roi), _acc.Extract(p_in_roi).ToInterop(), color.ToInterop());
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
        Context.World.Camera.Intrinsics);
      return coords[0];

    }

    private void _btn_take_ref_image_Click(object sender, EventArgs e) {
      _take_ref_image = true;
    }

    private void _btn_restart_Click(object sender, EventArgs e) {
      lock (Context.Viewer) {
        _acc = null;
        _pointcloud.ClearPoints();
      }
    }

    private void _btn_save_pointcloud_Click(object sender, EventArgs e) {
      using (TextWriter tw = new StreamWriter("points.csv")) {
        foreach (Vector v in _acc.Points) {
          tw.WriteLine(String.Format("{0} {1} {2}", v[0], v[1], v[2]));
        }
      }
    }
  }
}
