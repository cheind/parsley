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
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using log4net;

namespace Parsley {
  public partial class ExtrinsicCalibrationSlide : FrameGrabberSlide {

    private readonly ILog _logger = LogManager.GetLogger(typeof(PatternDesignerSlide));
    private Core.CalibrationPattern _pattern;
    private ExtrinsicCameraParameters _last_detected_plane;
    private UI.RectangleInteractor _interactor;
    private Rectangle _r;
    private bool _on_roi;
    private double _last_error;
    

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
      _last_error = Double.MaxValue;
      _interactor.InteractOn(Context.EmbeddableStream.PictureBox);
      _interactor.UnscaledSize = Context.Setup.Camera.FrameSize;
      base.OnSlidingIn(e);
    }

    protected override void OnSlidingOut(SlickInterface.SlidingEventArgs e) {
      _interactor.ReleaseInteraction();
      base.OnSlidingOut(e);
    }

    void Reset() {
      if (!Context.Setup.Camera.HasIntrinsics) {
        this.Logger.Error("An intrinsic calibration is required to perform extrinsic calibration.");
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

      if (_on_roi && _pattern != null) {
        Image<Gray, Byte> gray = img.Convert<Gray, Byte>();
        try {
          _pattern.FindPattern(gray, _r);
          if (_pattern.PatternFound) {
            Parsley.Core.ExtrinsicCalibration ec = new Parsley.Core.ExtrinsicCalibration(_pattern.ObjectPoints, Context.Setup.Camera.Intrinsics);
            ExtrinsicCameraParameters ecp = ec.Calibrate(_pattern.ImagePoints);
            double[] deviations;
            Vector[] points;

            Core.ExtrinsicCalibration.CalibrationError(
              ecp,
              Context.Setup.Camera.Intrinsics,
              _pattern.ImagePoints,
              _pattern.ObjectPoints,
              out deviations,
              out points);

            double max_error = deviations.Max();
            if (max_error < _last_error) {
              _last_detected_plane = ecp;
              _last_error = max_error;
              this.Logger.Info(String.Format("Extrinsics successfully calculated. Maximum error {0:F3}", _last_error));
              _on_roi = false;
            }
          } else {
            this.Logger.Warn("Pattern not found.");
          }
        } catch (System.Exception e) {
          this.Logger.Warn(String.Format("Failed to determine extrinsic calibration: {0}", e.Message));
        }
      }
      if (_last_detected_plane != null) {
        Core.Drawing.DrawCoordinateFrame(img, _last_detected_plane, Context.Setup.Camera.Intrinsics);
      }
    }

    void _interactor_InteractionCompleted(object sender, Parsley.UI.InteractionEventArgs e) {
      _on_roi = true;
      _r = (Rectangle)e.InteractionResult;
    }

    void _interactor_OnRectangle(Rectangle r) {
      _on_roi = true;
    }

    private void _btn_load_pattern_Click(object sender, EventArgs e) {
      if (openFileDialog1.ShowDialog() == DialogResult.OK) {
        using (Stream s = File.Open(openFileDialog1.FileName, FileMode.Open)) {
          if (s != null) {
            IFormatter formatter = new BinaryFormatter();
            _pattern = formatter.Deserialize(s) as Core.CalibrationPattern;
            s.Close();
            _last_detected_plane = null;
            _last_error = Double.MaxValue;
            _logger.Info(String.Format("Calibration pattern {0} successfully loaded.", new FileInfo(openFileDialog1.FileName).Name));
          }
        }
      }
    }

    private void _btn_save_extrinsics_Click(object sender, EventArgs e) {
      if (_last_detected_plane != null && saveFileDialog1.ShowDialog() == DialogResult.OK) {
        using (Stream s = File.OpenWrite(saveFileDialog1.FileName)) {
          if (s != null) {
            IFormatter formatter = new BinaryFormatter();
            formatter.Serialize(s, _last_detected_plane);
            s.Close();
            _logger.Info("Extrinsics successfully saved.");
          }
        }
      }
    }
  }
}
