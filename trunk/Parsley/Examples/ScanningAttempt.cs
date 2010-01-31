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
    Core.BuildingBlocks.ReferencePlane _left;
    Core.BuildingBlocks.ReferencePlane _right;
    Parsley.Core.BrightestPixelLLE _lle;
    Core.NotParallelPlaneConstraint _constraint;
    int _channel;
    private Parsley.Draw3D.PointCloud _pointcloud;


    public ScanningAttempt(Context c)
      : base(c) 
    {
      InitializeComponent();
      _lle = new Parsley.Core.BrightestPixelLLE();
      _lle.IntensityThreshold = 180;
      _channel = 2;

      _pointcloud = new Parsley.Draw3D.PointCloud();
      lock (Context.Viewer) {
        Context.Viewer.Add(_pointcloud);
      }

    }

    public ScanningAttempt() :base(null) {
      InitializeComponent();
    }

    protected override void OnSlidingIn() {
      _left = Context.ReferencePlanes[0];
      _right = Context.ReferencePlanes[1];
      _constraint = new Parsley.Core.NotParallelPlaneConstraint(new Core.Plane[] { _left.Plane, _right.Plane });
      lock (Context.Viewer) {
       /* Context.Viewer.SetupPerspectiveProjection(
          Core.BuildingBlocks.Perspective.FromCamera(Context.Camera, 1.0, 5000).ToInterop()
        );
        */
        Context.Viewer.LookAt(new double[] { 0, 0, 0 }, new double[] { 0, 0, 1 }, new double[] { 0, 1, 0 });
      }
      base.OnSlidingIn();
    }

    protected override void OnFrame(Parsley.Core.BuildingBlocks.FrameGrabber fp, Emgu.CV.Image<Emgu.CV.Structure.Bgr, byte> img) {
      // 1. Extract laser-line
      _lle.FindLaserLine(img[_channel]);
      PointF[] laser_points = _lle.ValidLaserPoints.ToArray();
      Core.Ray[] eye_rays = Core.Ray.EyeRays(Context.Camera.Intrinsics, laser_points);
      bool[] in_left = new bool[eye_rays.Length];
      Vector[] eye_ray_isects = new Vector[eye_rays.Length];

      for (int i = 0; i < eye_rays.Length; ++i) {
        double t_left, t_right;
        Core.Ray r = eye_rays[i];
        Core.Intersection.RayPlane(r, _left.Plane, out t_left);
        Core.Intersection.RayPlane(r, _right.Plane, out t_right);
        double min_t = Math.Min(t_left, t_right);
        eye_ray_isects[i] = r.At(min_t);
        Color color;
        PointF projected;
        if (t_left < t_right) {
          in_left[i] = true;
          color = Color.Green;
          projected = Backproject(eye_ray_isects[i], _left.Extrinsic);
        }else {
          in_left[i] = false;
          color = Color.Blue;
          projected = Backproject(eye_ray_isects[i], _right.Extrinsic);
          
        }
        /*
        if (projected.X >= 0 && projected.X < img.Width && projected.Y >= 0 && projected.Y < img.Height)
          img[(int)projected.Y, (int)projected.X] = new Emgu.CV.Structure.Bgr(color);
         */
      }
      if (eye_ray_isects.Length > 3) {
        Core.Ransac<Core.PlaneModel> ransac = new Parsley.Core.Ransac<Parsley.Core.PlaneModel>(eye_ray_isects);
        Core.Ransac<Core.PlaneModel>.Hypothesis h = ransac.Run(30, 1.0, (int)(img.Width*0.4), _constraint);
        if (h != null) {
          lock (Context.Viewer) {
            foreach (int id in h.ConsensusIds) {
              PointF projected;
              if (in_left[id]) {
                projected = Backproject(eye_ray_isects[id], _left.Extrinsic);
              } else {
                projected = Backproject(eye_ray_isects[id], _right.Extrinsic);
              }
              if (projected.X >= 0 && projected.X < img.Width && projected.Y >= 0 && projected.Y < img.Height)
                img[(int)projected.Y, (int)projected.X] = new Emgu.CV.Structure.Bgr(Color.Green);
              _pointcloud.AddPoint(eye_ray_isects[id].ToInterop());
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
  }
}
