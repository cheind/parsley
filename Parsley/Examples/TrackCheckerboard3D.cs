using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Emgu.CV.Structure;
using MathNet.Numerics.LinearAlgebra;
using Parsley.Core.Extensions;

namespace Parsley.Examples {
  public partial class TrackCheckerboard3D : FrameGrabberSlide {
    private Parsley.Core.ExtrinsicCalibration _ex;
    private Parsley.Draw3D.Axis _axis;
    private Parsley.Draw3D.Quad _board;
    private Parsley.Draw3D.Transform _board_transform;

    public TrackCheckerboard3D(Context c) : base(c) {
      InitializeComponent();
      _board_transform = new Parsley.Draw3D.Transform();
      _board = new Parsley.Draw3D.Quad(200.0, 125.0);
      _board_transform.Add(_board);
      _axis = new Parsley.Draw3D.Axis(25);
      _board_transform.Add(_axis);

      lock (Context.Viewer) {
        Context.Viewer.Add(_board_transform);
      }
    }

    protected override void OnSlidingIn() {
      _ex = new Parsley.Core.ExtrinsicCalibration(Context.CalibrationPattern.ObjectPoints, Context.Camera.Intrinsics);
      lock (Context.Viewer) {
        Context.Viewer.LookAt(new double[] { 0, 0, 0 }, new double[] { 0, 0, 1 }, new double[] { 0, 1, 0 });
      }
      base.OnSlidingIn();
    }

    protected override void OnSlidingOut(CancelEventArgs args) {
      base.OnSlidingOut(args);
    }

    override protected void OnFrame(Parsley.Core.BuildingBlocks.FrameGrabber fp, Emgu.CV.Image<Emgu.CV.Structure.Bgr, byte> img) {
      Core.CalibrationPattern pattern = this.Context.CalibrationPattern;
      Emgu.CV.Image<Gray, Byte> gray = img.Convert<Gray, Byte>();
      gray._EqualizeHist();
      pattern.FindPattern(gray);

      if (pattern.PatternFound) {
        Emgu.CV.ExtrinsicCameraParameters ecp = _ex.Calibrate(pattern.ImagePoints);
        lock (Context.Viewer) {
          Matrix m = Matrix.Identity(4, 4);
          m.SetMatrix(0, 2, 0, 3, ecp.ExtrinsicMatrix.ToParsley());
          _board_transform.Matrix = m.ToInterop();
        }
      }

      pattern.DrawPattern(img, pattern.ImagePoints, pattern.PatternFound);
    }
  }
}
