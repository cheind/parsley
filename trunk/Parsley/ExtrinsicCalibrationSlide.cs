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
    private UI.RectangleInteractor _interactor;

    public ExtrinsicCalibrationSlide(Context c)
      : base(c) 
    {
      this.InitializeComponent();
      _interactor = new Parsley.UI.RectangleInteractor();
      _on_roi = false;
    }

    private ExtrinsicCalibrationSlide() : base(null) 
    {
      this.InitializeComponent();
    }

    protected override void OnSlidingIn() {
      this.Reset();
      _interactor.InteractOn(Context.EmbeddableStream.PictureBox);
      _interactor.UnscaledSize = Context.Setup.World.Camera.FrameSize;
      Context.PropertyChanged += new PropertyChangedEventHandler(Context_PropertyChanged);
      base.OnSlidingIn();
    }

    void Context_PropertyChanged(object sender, PropertyChangedEventArgs e) {
      if (e.PropertyName == "Setup") {
        Reset();
      }
    }

    protected override void OnSlidingOut(CancelEventArgs args) {
      Context.PropertyChanged -= new PropertyChangedEventHandler(Context_PropertyChanged);
      _interactor.ReleaseInteraction();
      base.OnSlidingOut(args);
    }

    void Reset() {
      if (!Context.Setup.World.Camera.HasIntrinsics) {
        this.Logger.Error("An intrinsic calibration is required to perform extrinsic calibration.");
      } else {
        _ec = new Parsley.Core.ExtrinsicCalibration(Context.Setup.ExtrinsicPattern.ObjectPoints, Context.Setup.World.Camera.Intrinsics);
      }
    }

    protected override void OnFrame(Parsley.Core.BuildingBlocks.FrameGrabber fp, Emgu.CV.Image<Emgu.CV.Structure.Bgr, byte> img) {
      // Constraint checking
      if (!Context.Setup.World.Camera.HasIntrinsics) {
        _on_roi = false;
        return;
      }

      if (_interactor.DraggingState == Parsley.UI.RectangleInteractor.State.Dragging) {
        img.Draw(_interactor.CurrentRectangle, new Bgr(Color.Green), 2);
      } else {
        img.Draw(_interactor.LastCompleteRectangle, new Bgr(Color.Green), 2);
      }

      Core.CalibrationPattern pattern = Context.Setup.ExtrinsicPattern;
      if (_on_roi) {
        Image<Gray, Byte> gray = img.Convert<Gray, Byte>();
        pattern.FindPattern(gray, _interactor.LastCompleteRectangle);
        if (pattern.PatternFound) {
          ExtrinsicCameraParameters ecp = _ec.Calibrate(pattern.ImagePoints);
          double[] deviations;
          Vector[] points;

          Core.ExtrinsicCalibration.CalibrationError(
            ecp,
            Context.Setup.World.Camera.Intrinsics,
            pattern.ImagePoints,
            pattern.ObjectPoints,
            out deviations,
            out points);
          Context.Setup.World.ReferencePlanes.Add(new Core.Plane(ecp));
          Context.Setup.World.Extrinsics.Add(ecp);
          this.Logger.Info(String.Format("Plane #{0} detected. Maximum error {0:F2}", Context.Setup.World.ReferencePlanes.Count, deviations.Max()));
        } else {
          this.Logger.Warn("Plane not detected. Please repeat");
        }
        _on_roi = false;
      }
      foreach (Emgu.CV.ExtrinsicCameraParameters ecp in Context.Setup.World.Extrinsics) {
        pattern.DrawCoordinateFrame(img, ecp, Context.Setup.World.Camera.Intrinsics);
      }
    }

    void ROIHandler_OnROI(Rectangle r) {
      _on_roi = true;
    }

    private void _btn_clear_extrinsics_Click(object sender, EventArgs e) {
      Context.Setup.World.ReferencePlanes.Clear();
      Context.Setup.World.Extrinsics.Clear();
    }
  }
}
