using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Parsley.Core.Extensions;
using MathNet.Numerics.LinearAlgebra;
using Emgu.CV.Structure;

namespace Parsley {
  public partial class ScanningSlide : FrameGrabberSlide {
    private Parsley.Draw3D.PointCloud _pointcloud;
    private Core.DensePixelGrid<uint> _pixel_point_ids;
    bool _take_texture_image;
    bool _clear_points;
    System.Drawing.Rectangle _next_roi;
    Emgu.CV.Image<Emgu.CV.Structure.Bgr, byte> _texture_image;

    public ScanningSlide(Context c)
      : base(c) 
    {
      this.InitializeComponent();
      _pointcloud = new Parsley.Draw3D.PointCloud();
      _pixel_point_ids = new Parsley.Core.DensePixelGrid<uint>();
      lock (Context.Viewer) {
        Context.Viewer.Add(_pointcloud);
      }
    }


    protected override void OnSlidingIn() {
      lock (Context.Viewer) {
        Context.Viewer.SetupPerspectiveProjection(
          Core.BuildingBlocks.Perspective.FromCamera(Context.Setup.Camera, 1.0, 5000).ToInterop()
        );
        Context.Viewer.LookAt(
          new double[] { 0, 0, 0 },
          new double[] { 0, 0, 400 },
          new double[] { 0, 1, 0 }
        );
      }
      base.OnSlidingIn();
    }

    protected override void OnSlidingOut(CancelEventArgs args) {
      base.OnSlidingOut(args);
    }

    private ScanningSlide()
      : base(null) {
      InitializeComponent();
    }

    protected override void OnFrame(Parsley.Core.BuildingBlocks.FrameGrabber fp, Emgu.CV.Image<Emgu.CV.Structure.Bgr, byte> img) {
      if (_take_texture_image) {
        _take_texture_image = false;
        _texture_image = img.Copy();
        lock (Context.Viewer) {
          UpdateAllColors();
        }
      }

      if (_clear_points) {
        _clear_points = false;
        _pixel_point_ids.Reset();
        Context.Setup.ScanWorkflow.Reset();
      }

      if (Context.Setup.Camera.FrameSize != _pixel_point_ids.Size) {
        _pixel_point_ids.Size = Context.Setup.Camera.FrameSize;
      }

      Vector[] points;
      System.Drawing.Point[] pixels;

      if (Context.Setup.ScanWorkflow.Process(Context.Setup, img, out points, out pixels)) {
        lock (Context.Viewer) {
          UpdatePoints(points, pixels);
        }
        foreach (System.Drawing.Point p in pixels) {
          img[p.Y, p.X] = new Bgr(Color.Green);
        }
      }
    }

    private void UpdateAllColors() {
      for (int i = 0; i < _pixel_point_ids.PixelData.Length; ++i) {
        System.Drawing.Point p = Core.IndexHelper.PixelFromArrayIndex(i, _pixel_point_ids.Size);
        //we currently use image coordinate system as reference
        //p = Core.IndexHelper.MakeAbsolute(p, Context.Setup.ScanWorkflow.ROI);
        Vector color = GetPixelColor(ref p);
        // Note: 0 is used as not-set marker
        uint id = _pixel_point_ids.PixelData[i];
        if (id > 0) {
          _pointcloud.UpdateColor(id - 1, color);
        }
      }
    }

    private void UpdatePoints(Vector[] points, System.Drawing.Point[] pixels)
    {
      for (int i = 0; i < points.Length; ++i) {
        System.Drawing.Point pixel = pixels[i];
        //System.Drawing.Point rel_p = Core.IndexHelper.MakeRelative(pixel, Context.Setup.ScanWorkflow.ROI);
        uint id = _pixel_point_ids[pixel];
        if (id > 0) { // we use default value as not-set
          // Update point
          _pointcloud.UpdatePoint(id - 1, points[i]);
        } else {
          Vector color;
          color = GetPixelColor(ref pixel);
          id = _pointcloud.AddPoint(points[i], color);
          _pixel_point_ids[pixel] = id + 1; // 0 is used as not-set marker
        }
      }
    }

    private Vector GetPixelColor(ref System.Drawing.Point pixel) {
      Vector color;
      if (_texture_image != null) {
        Bgr bgr = _texture_image[pixel.Y, pixel.X];
        color = new Vector(new double[] { 
               bgr.Red / 255.0, 
               bgr.Green / 255.0, 
               bgr.Blue / 255.0, 
               1.0 
             });
      } else {
        // default color
        color = new Vector(new double[] { 0.7, 0.7, 0.7, 1.0 });
      }
      return color;
    }

    private void _btn_take_texture_image_Click(object sender, EventArgs e) {
      _take_texture_image = true;
    }

    private void _btn_clear_points_Click(object sender, EventArgs e) {
      _clear_points = true;
    }
  }
}
