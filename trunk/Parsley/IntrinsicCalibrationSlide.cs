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
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO;
using log4net;

namespace Parsley {
  public partial class IntrinsicCalibrationSlide : FrameGrabberSlide {
    private readonly ILog _logger = LogManager.GetLogger(typeof(PatternDesignerSlide));

    private Core.IntrinsicCalibration _ic;
    private Emgu.CV.IntrinsicCameraParameters _icp;
    private Core.ExtrinsicCalibration _ec; // used for illustration of coordinate frame only.
    private Core.CalibrationPattern _pattern;
    private bool _take_image_request;
    private bool _calibrate_request;
    private Timer _timer_auto;
    private bool _was_pattern_found;
    private bool _first_time;

    public IntrinsicCalibrationSlide(Context c) : base(c) {
      InitializeComponent();
      
      _timer_auto = new Timer();
      _timer_auto.Interval = 3000;
      _timer_auto.Tick += new EventHandler(_timer_auto_Tick);
    }


    protected override void OnSlidingIn(SlickInterface.SlidingEventArgs e) {
      this.Reset();
      base.OnSlidingIn(e);
    }

    protected override void OnSlidingOut(SlickInterface.SlidingEventArgs e) {
      _timer_auto.Enabled = false;
      base.OnSlidingOut(e);
    }

    /// <summary>
    /// Reset slide to initial
    /// </summary>
    void Reset() {
      if (_ic != null)
        _ic.ClearViews();
      _first_time = true;
      _timer_auto.Enabled = false;
      _cb_auto_take.Checked = false;
      _btn_calibrate.Enabled = false;
      _btn_take_image.Enabled = true;
      _calibrate_request = false;
    }

    void _timer_auto_Tick(object sender, EventArgs e) {
      _take_image_request = true;
    }

    protected override void OnFrame(Parsley.Core.BuildingBlocks.FrameGrabber fp, Emgu.CV.Image<Emgu.CV.Structure.Bgr, byte> img) {
      Core.CalibrationPattern pattern = _pattern;
      if (pattern != null) {
        Image<Gray, Byte> gray = img.Convert<Gray, Byte>();
        pattern.FindPattern(gray);
        this.UpdateStatusDisplay(pattern.PatternFound);
        this.HandleCalibrateRequest();
        this.HandleTakeImageRequest();
        this.DrawCoordinateFrame(img);
        pattern.DrawPattern(img, pattern.ImagePoints, pattern.PatternFound);
      }
    }

    private void UpdateStatusDisplay(bool pattern_found) {
      bool status_changed = _was_pattern_found != pattern_found || _first_time;
      _was_pattern_found = pattern_found;
      _first_time = false;

      if (_ic.Views.Count == 0 && status_changed) {
        if (pattern_found) {
          this.Logger.Info("Pattern found");
        } else {
          this.Logger.Warn("Pattern not found");
        }
      }
    }

    void DrawCoordinateFrame(Emgu.CV.Image<Emgu.CV.Structure.Bgr, byte> img) {
      Emgu.CV.IntrinsicCameraParameters icp = _icp;
      if (_ec != null && _pattern.PatternFound && icp != null) {
        Emgu.CV.ExtrinsicCameraParameters ecp = _ec.Calibrate(_pattern.ImagePoints);
        _pattern.DrawCoordinateFrame(img, ecp, icp);
      }
    }

    void HandleTakeImageRequest() {
      Core.CalibrationPattern pattern = _pattern;
      if (_take_image_request) {
        if (pattern.PatternFound) {
          _ic.AddView(pattern.ImagePoints);
          this.Logger.Info(String.Format("You have successfully acquired {0} calibration images.", _ic.Views.Count));
          this.Invoke((MethodInvoker)delegate {  
            _btn_calibrate.Enabled = _ic.Views.Count > 2 && !_cb_auto_take.Checked;
          });
        }
      }
      _take_image_request = false;
    }

    void HandleCalibrateRequest() {
      Core.CalibrationPattern pattern = _pattern;
      if (_calibrate_request) {
        _icp = _ic.Calibrate();
        _ec = new Parsley.Core.ExtrinsicCalibration(pattern.ObjectPoints, _icp);
        this.Logger.Info("Calibration succeeded");
        this.Invoke((MethodInvoker)delegate {
          _btn_calibrate.Enabled = false;
          _btn_take_image.Enabled = true;
          _cb_auto_take.Enabled = true;
          _cb_auto_take.Checked = false;
        });
      }
      _calibrate_request = false;
    }

    private void btn_take_image_Click(object sender, EventArgs e) {
      _take_image_request = true;
    }

    private void btn_calibrate_Click(object sender, EventArgs e) {
      _take_image_request = false;
      _timer_auto.Enabled = false;
      _cb_auto_take.Enabled = false;
      _btn_take_image.Enabled = false;
      _btn_calibrate.Enabled = false;
      _calibrate_request = true;
    }

    private void _cb_auto_take_CheckedChanged(object sender, EventArgs e) {
      _btn_take_image.Enabled = !_cb_auto_take.Checked;
      _btn_calibrate.Enabled = !_cb_auto_take.Checked && _ic.Views.Count > 2;
      if (_cb_auto_take.Checked) {
        this.Logger.Info("Auto-taking calibration images every three seconds.");
      }
      _timer_auto.Enabled = _cb_auto_take.Checked;
    }

    private void _btn_load_pattern_Click(object sender, EventArgs e) {
      if (openFileDialog1.ShowDialog() == DialogResult.OK) {
        using (Stream s = File.Open(openFileDialog1.FileName, FileMode.Open)) {
          if (s != null) {
            IFormatter formatter = new BinaryFormatter();
            _pattern = formatter.Deserialize(s) as Core.CalibrationPattern;
            _ic = new Parsley.Core.IntrinsicCalibration(_pattern.ObjectPoints, Context.Setup.Camera.FrameSize);
            s.Close();
            this.Reset();
            _logger.Info(String.Format("Calibration pattern {0} successfully loaded.", new FileInfo(openFileDialog1.FileName).Name));
          }
        }
      }
    }

    private void _btn_save_calibration_Click(object sender, EventArgs e) {
      if (_icp != null && saveFileDialog1.ShowDialog() == DialogResult.OK) {
        using (Stream s = File.OpenWrite(saveFileDialog1.FileName)) {
          if (s != null) {
            IFormatter formatter = new BinaryFormatter();
            formatter.Serialize(s, _icp);
            s.Close();
            _logger.Info("Intrinsics successfully saved.");
          }
        }
      }
    }
  }
}
