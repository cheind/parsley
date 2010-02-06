using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Parsley.Core.Extensions;

namespace Parsley {
  public partial class Main : Form {

    private Context _context;
    private UI.Concrete.StreamViewer _live_feed;
    private UI.Concrete.Draw3DViewer _3d_viewer;

    private IStatusDisplay _status_display;
    

    private MainSlide _slide_main;
    private ExamplesSlide _slide_examples;
    //private Examples.ExtractLaserLineSlide _slide_extract_laser_line;
    //private Examples.TrackCheckerboard3D _slide_track_checkerboard;
    //private Examples.ROISlide _slide_roi;
    private Examples.ScanningAttempt _slide_scanning;
    private IntrinsicCalibrationSlide _slide_intrinsic_calib;
    private ExtrinsicCalibrationSlide _slide_extrinsic_calib;
    private LaserSetupSlide _slide_laser_setup;
    private SetupSlide _slide_setup;
    private WelcomeSlide _slide_welcome;
    
    private ConfigurationSlide _slide_configuration;
    
    public Main() {
      InitializeComponent();

      // Try connect to default cam
      Core.BuildingBlocks.World world = new Parsley.Core.BuildingBlocks.World();
      Core.BuildingBlocks.FrameGrabber fg = new Parsley.Core.BuildingBlocks.FrameGrabber(world.Camera);
      

      _live_feed = new Parsley.UI.Concrete.StreamViewer();
      _live_feed.Interpolation = Emgu.CV.CvEnum.INTER.CV_INTER_LINEAR;
      _live_feed.FunctionalMode = Emgu.CV.UI.ImageBox.FunctionalModeOption.RightClickMenu;
      _live_feed.FrameGrabber = fg;
      _live_feed.FrameGrabber.FPS = 30;
      _live_feed.FormClosing += new FormClosingEventHandler(_live_feed_FormClosing);
      //_live_feed.Show();
      fg.Start();

      _3d_viewer = new Parsley.UI.Concrete.Draw3DViewer();
      _3d_viewer.FormClosing += new FormClosingEventHandler(_3d_viewer_FormClosing);
      _3d_viewer.RenderLoop.FPS = 30;
      _3d_viewer.AspectRatio = world.Camera.FrameAspectRatio;
      _3d_viewer.IsMaintainingAspectRatio = true;
      _3d_viewer.RenderLoop.Start();
      //_3d_viewer.Show();

      _status_display = new StatusStripStatusDisplay(_status_strip, _status_label);
       _context = new Context(world, fg , _3d_viewer.RenderLoop, _live_feed.ROIHandler, _status_display);
       _properties.Context = _context;
       //_property_pane = new PropertyPane(_context);
       //this.Controls.Add(_property_pane);

       _slide_welcome = new WelcomeSlide();

      _slide_main = new MainSlide();
      _slide_setup = new SetupSlide(_context);
      _slide_examples = new ExamplesSlide();
      _slide_scanning = new Parsley.Examples.ScanningAttempt(_context);
      
      _slide_intrinsic_calib = new IntrinsicCalibrationSlide(_context);
      _slide_extrinsic_calib = new ExtrinsicCalibrationSlide(_context);
      _slide_laser_setup = new LaserSetupSlide(_context);

      _slide_configuration = new ConfigurationSlide(_context);

      _slide_control.AddSlide(_slide_welcome);
      _slide_control.AddSlide(_slide_configuration);
      _slide_control.AddSlide(_slide_main);
      _slide_control.AddSlide(_slide_setup);
      _slide_control.AddSlide(_slide_examples);
      _slide_control.AddSlide(_slide_scanning);
      _slide_control.AddSlide(_slide_intrinsic_calib);
      _slide_control.AddSlide(_slide_extrinsic_calib);
      _slide_control.AddSlide(_slide_laser_setup);

      _slide_control.SlideChanged += new EventHandler<SlickInterface.SlideChangedArgs>(_slide_control_SlideChanged);
      _slide_control.Selected = _slide_welcome;
    }

    void _3d_viewer_FormClosing(object sender, FormClosingEventArgs e) {
      e.Cancel = true;
      _3d_viewer.Hide();
    }

    void _live_feed_FormClosing(object sender, FormClosingEventArgs e) {
      e.Cancel = true;
      _live_feed.Hide();
    }


    private void Main_FormClosing(object sender, FormClosingEventArgs e) {
      _context.FrameGrabber.Dispose();
      _context.World.Camera.Dispose();
      _context.RenderLoop.Dispose();
      _context.Viewer.Dispose();
    }

    private void _btn_back_Click(object sender, EventArgs e) {
      _slide_control.Backward();
    }

    void _slide_control_SlideChanged(object sender, SlickInterface.SlideChangedArgs e) {
      _btn_back.Enabled = e.Now != _slide_main;
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

    private void _btn_setup_Click(object sender, EventArgs e) {
      _slide_control.ForwardTo<ConfigurationSlide>();
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

    
  }
}
