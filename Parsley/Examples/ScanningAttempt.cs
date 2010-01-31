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
    int _channel;


    public ScanningAttempt(Context c)
      : base(c) 
    {
      InitializeComponent();
      _lle = new Parsley.Core.BrightestPixelLLE();
      _lle.IntensityThreshold = 180;
      _channel = 2;

    }

    public ScanningAttempt() :base(null) {
      InitializeComponent();
    }

    protected override void OnSlidingIn() {
      _left = Context.ReferencePlanes[0];
      _right = Context.ReferencePlanes[1];
      base.OnSlidingIn();
    }

    protected override void OnFrame(Parsley.Core.BuildingBlocks.FrameGrabber fp, Emgu.CV.Image<Emgu.CV.Structure.Bgr, byte> img) {
      // 1. Extract laser-line
      _lle.FindLaserLine(img[_channel]);
      PointF[] laser_points = _lle.ValidLaserPoints.ToArray();
      Core.Ray[] eye_rays = Core.Ray.EyeRays(Context.Camera.Intrinsics, laser_points);
      Vector[] eye_ray_isects = new Vector[eye_rays.Length];

      for (int i = 0; i < eye_rays.Length; ++i) {
        double t_left, t_right;
        Core.Ray r = eye_rays[i];
        Core.Intersection.RayPlane(r, _left.Plane, out t_left);
        Core.Intersection.RayPlane(r, _right.Plane, out t_right);
        double min_t = Math.Min(t_left, t_right);
        eye_ray_isects[i] = r.At(min_t);
        
        //

 
        
        
        Color color;
        PointF projected;
        if (t_left < t_right) {
          color = Color.Green;
          projected = Backproject(eye_ray_isects[i], _left.Extrinsic);
        }else {
          color = Color.Blue;
          projected = Backproject(eye_ray_isects[i], _right.Extrinsic);
          
        }
        if (projected.X >= 0 && projected.X < img.Width && projected.Y >= 0 && projected.Y < img.Height)
          img[(int)projected.Y, (int)projected.X] = new Emgu.CV.Structure.Bgr(color);

        /*
        img.Draw(new LineSegment2D(new Point(0, 0), new Point(100, 0)), new Emgu.CV.Structure.Bgr(Color.Red), 2);
        img.Draw(new LineSegment2D(new Point(0, 0), new Point(0, 100)), new Emgu.CV.Structure.Bgr(Color.Green), 2);
         */

        img[100, 0] = new Bgr(Color.Red);
        img[0, 100] = new Bgr(Color.Green);


      
      }


      // 2.


      /*for (int i = 0; i < _lle.LaserPoints.Length; ++i) {
        float y = _lle.LaserPoints[i];
        if (y > 0) {
          lls.Add(new PointF(i, y));
        }
      }*/
      


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
