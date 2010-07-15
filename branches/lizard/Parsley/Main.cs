/*
 * Parsley http://parsley.googlecode.com
 * Copyright (c) 2010, Christoph Heindl. All rights reserved.
 * Code license:	New BSD License
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Parsley.Core.Extensions;
using log4net;

namespace Parsley {
  public partial class Main : Form {

    private Context _context;
    private UI.Concrete.StreamViewer _live_feed;
    private UI.Concrete.Draw3DViewer _3d_viewer;

    private readonly ILog _logger = LogManager.GetLogger(typeof(Main));

    private IntrinsicCalibrationSlide _slide_intrinsic_calib;
    private ExtrinsicCalibrationSlide _slide_extrinsic_calib;
    private LaserSetupSlide _slide_laser_setup;
    private WelcomeSlide _slide_welcome;
    private ScanningSlide _slide_scanning;
    private ImageAlgorithmTestSlide _slide_image_algorithm_test;


    public Main() {
      // Addin
      Core.Addins.AddinStore.Discover();
      Core.Addins.AddinStore.Discover(Environment.CurrentDirectory);
      Core.Addins.AddinStore.Discover(Environment.CurrentDirectory + @"\plugins");

      InitializeComponent();

      // Try connect to default cam
      Core.BuildingBlocks.Setup setup = new Parsley.Core.BuildingBlocks.Setup();
      Core.BuildingBlocks.FrameGrabber fg = new Parsley.Core.BuildingBlocks.FrameGrabber(setup.Camera);

      _live_feed = new Parsley.UI.Concrete.StreamViewer();
      _live_feed.Interpolation = Emgu.CV.CvEnum.INTER.CV_INTER_LINEAR;
      _live_feed.FunctionalMode = Emgu.CV.UI.ImageBox.FunctionalModeOption.RightClickMenu;
      _live_feed.FrameGrabber = fg;
      _live_feed.FrameGrabber.FPS = 30;
      _live_feed.FormClosing += new FormClosingEventHandler(_live_feed_FormClosing);
      this.AddOwnedForm(_live_feed);
      //_live_feed.Show();
      fg.Start();

      _3d_viewer = new Parsley.UI.Concrete.Draw3DViewer();
      _3d_viewer.FormClosing += new FormClosingEventHandler(_3d_viewer_FormClosing);
      _3d_viewer.RenderLoop.FPS = 30;
      _3d_viewer.AspectRatio = setup.Camera.FrameAspectRatio;
      _3d_viewer.IsMaintainingAspectRatio = true;
      _3d_viewer.RenderLoop.Start();
      this.AddOwnedForm(_3d_viewer);
      //_3d_viewer.Show();

      _context = new Context(setup, fg, _3d_viewer.RenderLoop, _live_feed.EmbeddableStream);
      _properties.Context = _context;

      log4net.Appender.IAppender app = 
        LogManager.GetRepository().GetAppenders().FirstOrDefault(x => x is Logging.StatusStripAppender);
      if (app != null) {
        Logging.StatusStripAppender ssa = app as Logging.StatusStripAppender;
        ssa.StatusStrip = _status_strip;
        ssa.ToolStripStatusLabel = _status_label;
      }

      _slide_welcome = new WelcomeSlide();
      _slide_intrinsic_calib = new IntrinsicCalibrationSlide(_context);
      _slide_extrinsic_calib = new ExtrinsicCalibrationSlide(_context);
      _slide_laser_setup = new LaserSetupSlide(_context);
      _slide_scanning = new ScanningSlide(_context);
      _slide_image_algorithm_test = new ImageAlgorithmTestSlide(_context);

      
      _slide_control.AddSlide(_slide_welcome);
      _slide_control.AddSlide(_slide_scanning);
      _slide_control.AddSlide(_slide_intrinsic_calib);
      _slide_control.AddSlide(_slide_extrinsic_calib);
      _slide_control.AddSlide(_slide_laser_setup);
      _slide_control.AddSlide(_slide_image_algorithm_test);

      _slide_control.SlideChanged += new EventHandler<SlickInterface.SlideChangedArgs>(_slide_control_SlideChanged);
      _slide_control.Selected = _slide_welcome;
    }

    void _3d_viewer_FormClosing(object sender, FormClosingEventArgs e) {
      // Just hide, don't close except main-form closes
      if (e.CloseReason != CloseReason.FormOwnerClosing) {
        e.Cancel = true;
        _3d_viewer.Hide();
        _btn_show_3d_visualization.Checked = false;
      }
    }

    void _live_feed_FormClosing(object sender, FormClosingEventArgs e) {
      // Just hide, don't close except main-form closes
      if (e.CloseReason != CloseReason.FormOwnerClosing) {
        e.Cancel = true;
        _live_feed.Hide();
        _btn_show_camera_live_feed.Checked = false;
      }
    }

    private void Main_FormClosing(object sender, FormClosingEventArgs e) {
      _3d_viewer.Close();
      _live_feed.Close();
      _context.FrameGrabber.Dispose();
      _context.Setup.Camera.Dispose();
      _context.RenderLoop.Dispose();
      _context.Viewer.Dispose();
    }

    private void _btn_back_Click(object sender, EventArgs e) {
      _slide_control.Backward();
    }

    void _slide_control_SlideChanged(object sender, SlickInterface.SlideChangedArgs e) {
      _btn_back.Enabled = _slide_control.HasPrevious;
    }

    private void _btn_show_3d_visualization_Click(object sender, EventArgs e) {
      if (this._btn_show_3d_visualization.Checked) {
        _context.RenderLoop.Start();
        _3d_viewer.Show();
      } else {
        _3d_viewer.Hide();
      }
    }

    private void _btn_show_camera_live_feed_Click(object sender, EventArgs e) {
      if (this._btn_show_camera_live_feed.Checked) {
        _live_feed.Show();
      } else {
        _live_feed.Hide();
      }
    }

    private void _btn_camera_setup_Click(object sender, EventArgs e) {
      _slide_control.ForwardTo<IntrinsicCalibrationSlide>();
    }

    private void _btn_intrinsic_calibration_Click(object sender, EventArgs e) {
      _slide_control.ForwardTo<IntrinsicCalibrationSlide>();
    }

    private void _btn_settings_Click(object sender, EventArgs e) {
      if (_btn_settings.Checked) {
        _properties.Show();
        _properties.BringToFront();
      } else {
        _properties.Hide();
      }
    }

    private void Main_Resize(object sender, EventArgs e) {
      _properties.UpdatePosition();
    }

    private void _btn_load_configuration_Click(object sender, EventArgs e) {
      if (_open_dlg.ShowDialog(this) == DialogResult.OK) {
        int device_index = -1;
        try {
          _context.FrameGrabber.Stop();
          device_index = _context.Setup.Camera.DeviceIndex;
          _context.Setup.Camera.Dispose(); // Throw old camera away

          Core.BuildingBlocks.Setup s = Core.BuildingBlocks.Setup.LoadBinary(_open_dlg.FileName);
          _context.FrameGrabber.Camera = s.Camera;
          _context.Setup = s;
          _logger.Info("Loading Parsley configuration succeeded.");
        } catch (Exception) {
          _logger.Error("Loading Parsley configuration failed.");
          _context.Setup.Camera = new Parsley.Core.BuildingBlocks.Camera(device_index);
          _context.FrameGrabber.Camera = _context.Setup.Camera;
        } finally {
          _context.FrameGrabber.Start();
        }
      }
    }

    private void _btn_save_configuration_Click(object sender, EventArgs e) {
      if (_save_dialog.ShowDialog(this) == DialogResult.OK) {
        try {
          Core.BuildingBlocks.Setup.SaveBinary(_save_dialog.FileName, _context.Setup);
          _logger.Info("Sucessfully saved Parsley configuration.");
        } catch (Exception) {
          _logger.Error("Saving Parsley configuration failed.");
        }
      }
    }

    private void _btn_extrinsic_calibration_Click(object sender, EventArgs e) {
      _slide_control.ForwardTo<ExtrinsicCalibrationSlide>();
    }

    private void _btn_laser_configuration_Click(object sender, EventArgs e) {
      _slide_control.ForwardTo<LaserSetupSlide>();
    }

    private void _btn_scanning_Click(object sender, EventArgs e) {
      _slide_control.ForwardTo<ScanningSlide>();
    }

    private void _btn_image_algorithm_test_Click(object sender, EventArgs e) {
      _slide_control.ForwardTo<ImageAlgorithmTestSlide>();
    }


  }
}
