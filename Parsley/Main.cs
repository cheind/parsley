using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Parsley {
  public partial class Main : Form {

    private Core.FrameGrabber _fg;
    private Core.Camera _camera;
    private UI.Concrete.StreamViewer _live_feed;

    private MainSlide _slide_main;
    private ExamplesSlide _slide_examples;
    private Examples.ExtractLaserLineSlide _slide_extract_laser_line;

    public Main() {
      InitializeComponent();

      try {
        _camera = new Parsley.Core.Camera(0);
        _fg = new Parsley.Core.FrameGrabber(_camera);
        _live_feed = new Parsley.UI.Concrete.StreamViewer();
        _live_feed.FrameGrabber = _fg;
        _live_feed.ShowInTaskbar = true;
        _live_feed.FormClosing += new FormClosingEventHandler(_live_feed_FormClosing);
        _live_feed.Shown += new EventHandler(_live_feed_Shown);
      } catch (ArgumentException) {
        UI.ParsleyMessage.Show("No Camera found!", "Could not connect to camera. Make sure a camera is attached.");
      }

      _slide_main = new MainSlide();
      _slide_examples = new ExamplesSlide();
      _slide_extract_laser_line = new Parsley.Examples.ExtractLaserLineSlide(_fg);

      _slide_control.AddSlide(_slide_main);
      _slide_control.AddSlide(_slide_examples);
      _slide_control.AddSlide(_slide_extract_laser_line);
      

      _slide_control.Selected = _slide_main;

      
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

    private void Main_FormClosing(object sender, FormClosingEventArgs e) {
      _fg.Dispose();
      _camera.Dispose();
    }

    private void _btn_back_Click(object sender, EventArgs e) {
      _slide_control.Backward();
    }
  }
}
