/*
 * Parsley http://parsley.googlecode.com
 * Copyright (c) 2010, Christoph Heindl
 * Copyright (c) 2010, Matthias Plasch
 * All rights reserved.
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
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using log4net;
using Emgu.CV;
using Emgu.CV.Structure;

namespace Parsley {
  public partial class PatternDesignerSlide : FrameGrabberSlide {
    private readonly ILog _logger = LogManager.GetLogger(typeof(PatternDesignerSlide));

    private Core.CalibrationPattern _pattern;

    public PatternDesignerSlide(Context c)
      : base(c) 
    {
      InitializeComponent();
      foreach (Core.Addins.AddinInfo info in Core.Addins.AddinStore.FindAddins(typeof(Core.CalibrationPattern))) {
        _cmb_patterns.Items.Add(info);
      }
    }

    private PatternDesignerSlide() : base(null) {
      InitializeComponent();
    }

    protected override void OnFrame(Parsley.Core.BuildingBlocks.FrameGrabber fp, Emgu.CV.Image<Emgu.CV.Structure.Bgr, byte> img) {
      Parsley.Core.ExtrinsicCalibration ec;
      ExtrinsicCameraParameters ecp;
      bool pattern_found = false;
      Core.CalibrationPattern p = _pattern;
      if (p != null) {
        Image<Gray, Byte> gray = img.Convert<Gray, Byte>();
        pattern_found = p.FindPattern(gray);
        p.DrawPattern(img, p.ImagePoints, p.PatternFound);

        // if pattern has been found ==> find extrinsics and draw the corresponding coordinate frame 
        if (pattern_found == true && Context.Setup.Camera.Intrinsics != null)
        {
          ec = new Parsley.Core.ExtrinsicCalibration(p.ObjectPoints, Context.Setup.Camera.Intrinsics);
          ecp = ec.Calibrate(p.ImagePoints);
          
          if(ecp != null)
            Core.Drawing.DrawCoordinateFrame(img, ecp, Context.Setup.Camera.Intrinsics);
        }
      }

      base.OnFrame(fp, img);
    }

    private void _cmb_patterns_SelectedIndexChanged(object sender, EventArgs e) {
      if (_cmb_patterns.SelectedIndex >= 0) {
        // Instance a new object from selected type
        Core.Addins.AddinInfo info = _cmb_patterns.SelectedItem as Core.Addins.AddinInfo;
        if (info != null) {
          _pattern = Core.Addins.AddinStore.CreateInstance(info) as Core.CalibrationPattern;
          _pg.SelectedObject = _pattern;
        }
      }
    }

    private void _btn_save_pattern_Click(object sender, EventArgs e) {
      if (saveFileDialog1.ShowDialog() == DialogResult.OK) {
        using (Stream s = File.OpenWrite(saveFileDialog1.FileName)) {
          if (s != null) {
            IFormatter formatter = new BinaryFormatter();
            formatter.Serialize(s, _pattern);
            s.Close();
            _logger.Info("Pattern successfully saved.");
          }
        }
      }
    }


  }
}
