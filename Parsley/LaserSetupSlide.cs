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
using System.IO;
using System.Diagnostics;

using Parsley.Core.Extensions;

namespace Parsley {
  public partial class LaserSetupSlide : FrameGrabberSlide {
    private bool _save_laser_data;

    public LaserSetupSlide(Context c) : base(c) 
    {
      InitializeComponent();
#if !DEBUG
      this._btn_save_laser_data.Visible = false;
#endif
    }

    public LaserSetupSlide() :base(null) {
    }

    protected override void OnFrame(Parsley.Core.BuildingBlocks.FrameGrabber fp, Emgu.CV.Image<Emgu.CV.Structure.Bgr, byte> img) 
    {
      Core.Bundle b = new Parsley.Core.Bundle();
      Core.BundleBookmarks bb = new Parsley.Core.BundleBookmarks(b);

      bb.Image = img;
      bb.LaserColor = Context.Setup.Laser.Color;
           
      if (!Context.Setup.ScanWorkflow.LaserLineAlgorithm.FindLaserLine(bb.Bundle)) return;
      if (!Context.Setup.ScanWorkflow.LaserLineFilterAlgorithm.FilterLaserLine(bb.Bundle)) return;

      SaveLaserData(bb.LaserPixel);
      foreach (System.Drawing.PointF p in bb.LaserPixel) {
        img[p.ToNearestPoint()] = new Emgu.CV.Structure.Bgr(System.Drawing.Color.Green);
      }
    }

    [Conditional("DEBUG")]
    void SaveLaserData(List<System.Drawing.PointF> laser_points) {
      if (_save_laser_data) {
        _save_laser_data = false;
        using (TextWriter tw = new StreamWriter(string.Format("laser_{0}.txt", Guid.NewGuid()))) {
          foreach (System.Drawing.PointF p in laser_points.Where(p => p != PointF.Empty)) {
            tw.WriteLine(String.Format("{0};{1}", p.X, p.Y));
          }
        }
      }
    }

    private void _btn_save_laser_data_Click(object sender, EventArgs e) {
      _save_laser_data = true;
    }
  }
}
