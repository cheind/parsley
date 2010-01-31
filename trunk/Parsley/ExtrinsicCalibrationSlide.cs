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
      _ec = new Parsley.Core.ExtrinsicCalibration(Context.CalibrationPattern.ObjectPoints, Context.Camera.Intrinsics);
      Context.ROIHandler.OnROI += new Parsley.UI.Concrete.ROIHandler.OnROIHandler(ROIHandler_OnROI);
      base.OnSlidingIn();
    }

    protected override void OnSlidingOut(CancelEventArgs args) {
      Context.ROIHandler.OnROI -= new Parsley.UI.Concrete.ROIHandler.OnROIHandler(ROIHandler_OnROI);
      base.OnSlidingOut(args);
    }

    protected override void OnFrame(Parsley.Core.BuildingBlocks.FrameGrabber fp, Emgu.CV.Image<Emgu.CV.Structure.Bgr, byte> img) {
      if (_on_roi) {
        Core.CalibrationPattern pattern = this.Context.CalibrationPattern;
        Image<Gray, Byte> gray = img.Convert<Gray, Byte>();
        pattern.FindPattern(gray, Context.ROIHandler.Last);
        if (pattern.PatternFound) {
          ExtrinsicCameraParameters ecp = _ec.Calibrate(pattern.ImagePoints);
          Context.ReferencePlanes.Add(new Parsley.Core.BuildingBlocks.ReferencePlane(ecp, Context.ROIHandler.Last));
        }
        _on_roi = false;
      }
      foreach (Core.BuildingBlocks.ReferencePlane p in Context.ReferencePlanes) {
        Context.CalibrationPattern.DrawCoordinateFrame(img, p.Extrinsic, Context.Camera.Intrinsics);
      }
    }

    void ROIHandler_OnROI(Rectangle r) {
      _on_roi = true;
    }
  }
}
