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

using Emgu.CV;
using Emgu.CV.Structure;
using MathNet.Numerics.LinearAlgebra;

namespace Parsley {
  public partial class ExtrinsicCalibrationSlide : FrameGrabberSlide {

    private bool _on_roi;
    private Core.ExtrinsicCalibration _ec;
    private UI.RectangleInteractor _interactor;
    private Rectangle _r;
    private bool _clear_request;

    public ExtrinsicCalibrationSlide(Context c)
      : base(c) 
    {
      this.InitializeComponent();
      _interactor = new Parsley.UI.RectangleInteractor();
      _interactor.InteractionCompleted += new EventHandler<Parsley.UI.InteractionEventArgs>(_interactor_InteractionCompleted);
      _on_roi = false;
    }

    private ExtrinsicCalibrationSlide() : base(null) 
    {
      this.InitializeComponent();
    }

    protected override void OnSlidingIn(SlickInterface.SlidingEventArgs e) {
      this.Reset();
      _interactor.InteractOn(Context.EmbeddableStream.PictureBox);
      _interactor.UnscaledSize = Context.Setup.Camera.FrameSize;
      Context.PropertyChanged += new PropertyChangedEventHandler(Context_PropertyChanged);
      base.OnSlidingIn(e);
    }

    void Context_PropertyChanged(object sender, PropertyChangedEventArgs e) {
      if (e.PropertyName == "Setup") {
        Reset();
      }
    }

    protected override void OnSlidingOut(SlickInterface.SlidingEventArgs e) {
      Context.PropertyChanged -= new PropertyChangedEventHandler(Context_PropertyChanged);
      _interactor.ReleaseInteraction();
      base.OnSlidingOut(e);
    }

    void Reset() {
      if (!Context.Setup.Camera.HasIntrinsics) {
        this.Logger.Error("An intrinsic calibration is required to perform extrinsic calibration.");
      } else {
        _ec = new Parsley.Core.ExtrinsicCalibration(Context.Setup.ExtrinsicPattern.ObjectPoints, Context.Setup.Camera.Intrinsics);
      }
    }

    protected override void OnFrame(Parsley.Core.BuildingBlocks.FrameGrabber fp, Emgu.CV.Image<Emgu.CV.Structure.Bgr, byte> img) {
      // Constraint checking
      if (!Context.Setup.Camera.HasIntrinsics) {
        _on_roi = false;
        return;
      }

      if (_interactor.State == Parsley.UI.InteractionState.Interacting) {
        _interactor.DrawIndicator(_interactor.Current, img);
      } else {
        _interactor.DrawIndicator(_r, img);
      }

      if (_clear_request) {
        _clear_request = false;
        Context.Setup.ReferencePlanes.Clear();
        Context.Setup.Extrinsics.Clear();
      }

      Core.CalibrationPattern pattern = Context.Setup.ExtrinsicPattern;
      if (_on_roi) {
        Image<Gray, Byte> gray = img.Convert<Gray, Byte>();
        pattern.FindPattern(gray, _r);
        if (pattern.PatternFound) {
          ExtrinsicCameraParameters ecp = _ec.Calibrate(pattern.ImagePoints);
          double[] deviations;
          Vector[] points;

          Core.ExtrinsicCalibration.CalibrationError(
            ecp,
            Context.Setup.Camera.Intrinsics,
            pattern.ImagePoints,
            pattern.ObjectPoints,
            out deviations,
            out points);
          Context.Setup.ReferencePlanes.Add(new Core.Plane(ecp));
          Context.Setup.Extrinsics.Add(ecp);
          this.Logger.Info(String.Format("Plane #{0} detected. Maximum error {1:F3}", Context.Setup.ReferencePlanes.Count, deviations.Max()));
        } else {
          this.Logger.Warn("Plane not detected. Please repeat");
        }
        _on_roi = false;
      }
      foreach (Emgu.CV.ExtrinsicCameraParameters ecp in Context.Setup.Extrinsics) {
        pattern.DrawCoordinateFrame(img, ecp, Context.Setup.Camera.Intrinsics);
      }
    }

    void _interactor_InteractionCompleted(object sender, Parsley.UI.InteractionEventArgs e) {
      _on_roi = true;
      _r = (Rectangle)e.InteractionResult;
    }



    void _interactor_OnRectangle(Rectangle r) {
      _on_roi = true;
    }

    private void _btn_clear_extrinsics_Click(object sender, EventArgs e) {
      _clear_request = true;

    }
  }
}
