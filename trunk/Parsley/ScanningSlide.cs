/*
 * Parsley http://parsley.googlecode.com
 * Copyright (c) 2010, Christoph Heindl. All rights reserved.
 * Code license:	New BSD License
 */

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


    protected override void OnSlidingIn(SlickInterface.SlidingEventArgs e) {
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
      base.OnSlidingIn(e);
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
        _pointcloud.ClearPoints();
      }

      if (Context.Setup.Camera.FrameSize != _pixel_point_ids.Size) {
        _pixel_point_ids.Size = Context.Setup.Camera.FrameSize;
      }

      List<Vector> points;
      List<System.Drawing.Point> pixels;

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
        // Note: 0 is used as not-set marker
        uint id = _pixel_point_ids.PixelData[i];
        if (id > 0) {
          double[] color = GetPixelColor(ref p);
          _pointcloud.UpdateColor(id - 1, color);
        }
      }
    }

    private void UpdatePoints(List<Vector> points, List<System.Drawing.Point> pixels)
    {
      for (int i = 0; i < points.Count; ++i) {
        System.Drawing.Point pixel = pixels[i];
        //System.Drawing.Point rel_p = Core.IndexHelper.MakeRelative(pixel, Context.Setup.ScanWorkflow.ROI);
        uint id = _pixel_point_ids[pixel];
        if (id > 0) { // we use default value as not-set
          // Update point
          _pointcloud.UpdatePoint(id - 1, points[i].ToInterop());
        } else {
          id = _pointcloud.AddPoint(points[i].ToInterop(), GetPixelColor(ref pixel));
          _pixel_point_ids[pixel] = id + 1; // 0 is used as not-set marker
        }
      }
    }

    private double[] GetPixelColor(ref System.Drawing.Point pixel) {
      if (_texture_image != null) {
        Bgr bgr = _texture_image[pixel.Y, pixel.X];
        return bgr.ToInterop();
      } else {
        // default color
        return new double[] { 0.7, 0.7, 0.7, 1.0 };
      }
    }

    private void _btn_take_texture_image_Click(object sender, EventArgs e) {
      _take_texture_image = true;
    }

    private void _btn_clear_points_Click(object sender, EventArgs e) {
      _clear_points = true;
    }

    private void _btn_save_points_Click(object sender, EventArgs e)
    {
      if (saveFileDialog1.ShowDialog() == DialogResult.OK)
      {
        _pointcloud.SaveAsCSV(saveFileDialog1.FileName, " ");
      }
    }

    private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
    {

    }
  }
}
