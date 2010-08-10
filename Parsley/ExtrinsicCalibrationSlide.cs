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
    private Matrix _plane_shift;
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
      _plane_shift = Matrix.Identity(4, 4);
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
      _last_detected_plane = null;
      _last_error = Double.MaxValue;
      _on_roi = true;
      _r = (Rectangle)e.InteractionResult;
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

      //save modified extrinsic camera parameters
      _last_detected_plane = CalculateShiftedECP();

      if (_last_detected_plane != null && saveFileDialog1.ShowDialog() == DialogResult.OK)
      {
        using (Stream s = File.OpenWrite(saveFileDialog1.FileName))
        {
          if (s != null)
          {
            IFormatter formatter = new BinaryFormatter();
            formatter.Serialize(s, _last_detected_plane);
            s.Close();
            _logger.Info("Extrinsics successfully saved.");
          }
        }
      }
      else
        _logger.Warn("Error saving Extrinsics.");
    }


    /// <summary>
    /// Calculates the "shifted" Extrinsic Camera Parameters in order to obtain a shifted plane.
    /// This is done by multiplying the Extrinsic Matrix by the _plane_shift transformation matrix.
    /// The extrinsic Calibration Slide is used to save the extrinsics for different patterns. Because of that,
    /// the modified extrinsic need to be created and saved. 
    /// Since an ExtrinsicCameraParameters Object holds the
    /// members "ExtrinsicMatrix" (Type: Matrix), "RotationVector" (Type: RotationVector3D, which represents the
    /// rotational Part of the Extrinsic Matrix, and "TranslationVector" (Type: Matrix, which represents the
    /// translational Part of the Extrinsic Matrix), a conversion of the RotationalMatrix into a RotationVector3D and
    /// the extraction of the translational vector is neccessary.
    /// 
    /// The Method "cvRodrigues2" is used to convert a rotational Matrix (3x3-matrix) into a RotationVector3D and vice versa.
    /// Additionally the function expects an empty 3x9-matrix to store some partial derivatives (we do not use them!).
    /// Using the resulting RotationVector3D and the translational vector (corresponds to the fourth column of the combined
    /// rotational and translational matrix), the modified extrinsics can be created using the given object constructor.
    /// </summary>
    /// <returns></returns>
    private ExtrinsicCameraParameters CalculateShiftedECP()
    {
      if (_last_detected_plane != null && _plane_shift != null)
      {
        RotationVector3D r_Vector = new RotationVector3D();
        Matrix<double> modified_ROTMatrix, modified_Translation;
        Matrix<double> jacobian = new Matrix<double>(3, 9);
        Matrix extrinsic_Mat = Parsley.Core.Extensions.ConvertToParsley.ToParsley(_last_detected_plane.ExtrinsicMatrix);
        Matrix help_matrix = Matrix.Identity(4, 4);

        //copy the 3x4 extrinsic matrix into the 4x4 matrix, at the given position (initial row, end row, initial column, end column, Matrix)
        help_matrix.SetMatrix(0, 2, 0, 3, extrinsic_Mat);
        //multiply with shift-matrix
        help_matrix = help_matrix.Multiply(_plane_shift);
        //extract rotationalMatrix and convert it to Emgu Matrix type (same parameters as SetMatrix)
        modified_ROTMatrix = Parsley.Core.Extensions.ConvertFromParsley.ToEmgu(help_matrix.GetMatrix(0, 2, 0, 2));
        //extract translational vector
        modified_Translation = Parsley.Core.Extensions.ConvertFromParsley.ToEmgu(help_matrix.GetMatrix(0, 2, 3, 3));

        //convert the rotational matrix into the RotationVector3D (r_Vector)
        CvInvoke.cvRodrigues2(modified_ROTMatrix.Ptr, r_Vector.Ptr, jacobian.Ptr);

        return (new ExtrinsicCameraParameters(r_Vector, modified_Translation));
      }
      else
      {
        _logger.Warn("No plane has been detected yet.");
        return null;
      }
    }

    /// <summary>
    /// Updates the coordinate transformation matrix (to shift the detected plane),
    /// when the value of the Numeric Input Fiels is changed.
    /// The matrix _plane_shift represents a common coordinate translation in z- direction.
    /// Value > 0: Plane is shifted in positive z-direction.
    /// Observe, that _plane_shift is 4x4 matrix. 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void numeric_field_translation_ValueChanged(object sender, EventArgs e)
    {
      _plane_shift[2, 3] = (double)numeric_field_translation.Value;
    }
  }
}
