using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Emgu.CV;
using Emgu.CV.Structure;
using MathNet.Numerics.LinearAlgebra;

namespace Parsley {
  public partial class ExtrinsicCalibrationSlide : FrameGrabberSlide {

    private bool _on_roi;
    private Core.ExtrinsicCalibration _ec;

    public ExtrinsicCalibrationSlide(Context c)
      : base(c) 
    {
      this.InitializeComponent();
      _on_roi = false;
    }

    private ExtrinsicCalibrationSlide() : base(null) 
    {
      this.InitializeComponent();
    }

    protected override void OnSlidingIn() {
      this.OnConfigurationLoaded(this, null);
      Context.ROIHandler.OnROI += new Parsley.UI.Concrete.ROIHandler.OnROIHandler(ROIHandler_OnROI);
      base.OnSlidingIn();
    }

    protected override void OnConfigurationLoaded(object sender, EventArgs e) {
      if (!Context.World.Camera.HasIntrinsics) {
        this.Logger.Error("An intrinsic calibration is required to perform extrinsic calibration.");
      } else {
        _ec = new Parsley.Core.ExtrinsicCalibration(Context.World.ExtrinsicPattern.ObjectPoints, Context.World.Camera.Intrinsics);
      }
    }

    protected override void OnSlidingOut(CancelEventArgs args) {
      Context.ROIHandler.OnROI -= new Parsley.UI.Concrete.ROIHandler.OnROIHandler(ROIHandler_OnROI);
      base.OnSlidingOut(args);
    }

    protected override void OnFrame(Parsley.Core.BuildingBlocks.FrameGrabber fp, Emgu.CV.Image<Emgu.CV.Structure.Bgr, byte> img) {
      // Constraint checking
      if (!Context.World.Camera.HasIntrinsics) {
        _on_roi = false;
        return;
      }

      Core.CalibrationPattern pattern = Context.World.ExtrinsicPattern;
      if (_on_roi) {
        Image<Gray, Byte> gray = img.Convert<Gray, Byte>();
        pattern.FindPattern(gray, Context.ROIHandler.Last);
        if (pattern.PatternFound) {
          ExtrinsicCameraParameters ecp = _ec.Calibrate(pattern.ImagePoints);
          double[] deviations;
          Vector[] points;

          Core.ExtrinsicCalibration.CalibrationError(
            ecp,
            Context.World.Camera.Intrinsics,
            pattern.ImagePoints,
            pattern.ObjectPoints,
            out deviations,
            out points);
          Context.World.ReferencePlanes.Add(new Core.Plane(ecp));
          Context.World.Extrinsics.Add(ecp);
          this.Logger.Info(String.Format("Plane #{0} detected. Maximum error {0:F2}", Context.World.ReferencePlanes.Count, deviations.Max()));
        } else {
          this.Logger.Warn("Plane not detected. Please repeat");
        }
        _on_roi = false;
      }
      foreach (Emgu.CV.ExtrinsicCameraParameters ecp in Context.World.Extrinsics) {
        pattern.DrawCoordinateFrame(img, ecp, Context.World.Camera.Intrinsics);
      }
    }

    void ROIHandler_OnROI(Rectangle r) {
      _on_roi = true;
    }
  }
}
