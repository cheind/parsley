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

    private Core.FrameGrabber _fg;
    private Core.Camera _camera;
    private Context _context;
    private Core.CheckerBoard _calibration_pattern;
    private UI.Concrete.StreamViewer _live_feed;
    private UI.Concrete.Draw3DViewer _3d_viewer;

    private MainSlide _slide_main;
    private ExamplesSlide _slide_examples;
    private Examples.ExtractLaserLineSlide _slide_extract_laser_line;
    private Examples.TrackCheckerboard3D _slide_track_checkerboard;
    private Examples.ROISlide _slide_roi;
    private IntrinsicCalibrationSlide _slide_intrinsic_calib;
    private CameraParameterSlide _slide_cam_parameters;
    
    public Main() {
      InitializeComponent();

      // Try connect to default cam
      _camera = new Parsley.Core.Camera(0);
      _fg = new Parsley.Core.FrameGrabber(_camera);
      _calibration_pattern = new Parsley.Core.CheckerBoard(9, 6, 25.0f);
      _live_feed = new Parsley.UI.Concrete.StreamViewer();
      _live_feed.Interpolation = Emgu.CV.CvEnum.INTER.CV_INTER_LINEAR;
      _live_feed.FunctionalMode = Emgu.CV.UI.ImageBox.FunctionalModeOption.RightClickMenu;
      _live_feed.FrameGrabber = _fg;
      _live_feed.FrameGrabber.FPS = 30;
      _live_feed.FormClosing += new FormClosingEventHandler(_live_feed_FormClosing);
      _live_feed.Shown += new EventHandler(_live_feed_Shown);

      _3d_viewer = new Parsley.UI.Concrete.Draw3DViewer();
      _3d_viewer.FormClosing += new FormClosingEventHandler(_3d_viewer_FormClosing);
      _3d_viewer.Shown += new EventHandler(_3d_viewer_Shown);
      _3d_viewer.RenderLoop.FPS = 30;
      _3d_viewer.AspectRatio = _camera.FrameAspectRatio;
      _3d_viewer.IsMaintainingAspectRatio = true;
      _context = new Context(_fg, _3d_viewer.RenderLoop, _calibration_pattern, _live_feed.EmbeddableStream);

      _slide_main = new MainSlide();
      _slide_examples = new ExamplesSlide();
      _slide_cam_parameters = new CameraParameterSlide(_context);
      _slide_extract_laser_line = new Parsley.Examples.ExtractLaserLineSlide(_context);
      _slide_track_checkerboard = new Parsley.Examples.TrackCheckerboard3D(_context);
      _slide_roi = new Parsley.Examples.ROISlide(_context);
      _slide_intrinsic_calib = new IntrinsicCalibrationSlide(_context);
      _slide_intrinsic_calib.OnCalibrationSucceeded += new EventHandler<EventArgs>(_slide_intrinsic_calib_OnCalibrationSucceeded);

      _slide_control.AddSlide(_slide_main);
      _slide_control.AddSlide(_slide_examples);
      _slide_control.AddSlide(_slide_extract_laser_line);
      _slide_control.AddSlide(_slide_track_checkerboard);
      _slide_control.AddSlide(_slide_roi);
      _slide_control.AddSlide(_slide_intrinsic_calib);
      _slide_control.AddSlide(_slide_cam_parameters);

      _slide_control.SlideChanged += new EventHandler<SlickInterface.SlideChangedArgs>(_slide_control_SlideChanged);
      _slide_control.Selected = _slide_main;
    }

    void _slide_intrinsic_calib_OnCalibrationSucceeded(object sender, EventArgs e) {
      lock (_context.Viewer) {
        _context.Viewer.SetupPerspectiveProjection(
          Core.Perspective.FromCamera(_context.Camera, 1.0, 5000).ToInterop()
        );
      }
    }

    void _3d_viewer_Shown(object sender, EventArgs e) {
      this.mnu_3d_viewer.Checked = true;
    }

    void _3d_viewer_FormClosing(object sender, FormClosingEventArgs e) {
      this.mnu_3d_viewer.Checked = false;
      e.Cancel = true;
      _3d_viewer.Hide();
    }

    void _slide_control_SlideChanged(object sender, SlickInterface.SlideChangedArgs e) {
      _btn_back.Enabled = e.Now != _slide_main;
    }

    void _live_feed_Shown(object sender, EventArgs e) {
      this.mnu_live_feed.Checked = true;
    }

    void _live_feed_FormClosing(object sender, FormClosingEventArgs e) {
      this.mnu_live_feed.Checked = false;
      e.Cancel = true;
      _live_feed.Hide();
    }

    private void mnu_live_feed_Click(object sender, EventArgs e) {
      if (this.mnu_live_feed.Checked) {
        _fg.Start();
        _live_feed.Show();
      } else {
        _live_feed.Hide();
      }
    }

    private void mnu_3d_viewer_Click(object sender, EventArgs e) {
      if (this.mnu_3d_viewer.Checked) {
        _context.RenderLoop.Start();
        _3d_viewer.Show();
      } else {
        _3d_viewer.Hide();
      }
    }

    private void Main_FormClosing(object sender, FormClosingEventArgs e) {
      _context.FrameGrabber.Dispose();
      _context.Camera.Dispose();
      _context.RenderLoop.Dispose();
      _context.Viewer.Dispose();
    }

    private void _btn_back_Click(object sender, EventArgs e) {
      _slide_control.Backward();
    }

    private void _mnu_show_camera_properties_Click(object sender, EventArgs e) {
      _slide_control.ForwardTo<CameraParameterSlide>();
    }
  }
}
