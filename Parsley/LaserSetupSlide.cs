using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Parsley {
  public partial class LaserSetupSlide : FrameGrabberSlide {

    private bool _filter_enabled;

    public LaserSetupSlide(Context c) : base(c) 
    {
      InitializeComponent();
    }

    public LaserSetupSlide() :base(null) {
      InitializeComponent();
    }

    protected override void OnSlidingIn() {
      _pg_algorithm_settings.SelectedObject = Context.Laser.LaserLineAlgorithm;
      _cmb_laser_color.SelectedIndex = Context.Laser.Channel;
      base.OnSlidingIn();
    }

    protected override void OnFrame(Parsley.Core.BuildingBlocks.FrameGrabber fp, Emgu.CV.Image<Emgu.CV.Structure.Bgr, byte> img) 
    {
      if (_filter_enabled) {
        Context.Laser.AddThresholdImage(img);
      } else {
        Context.Laser.FindLaserLine(img);
        foreach (System.Drawing.PointF p in Context.Laser.ValidLaserPoints) {
          img[(int)p.Y, (int)p.X] = new Emgu.CV.Structure.Bgr(System.Drawing.Color.Green);
        }
      }
    }

    private void _timer_take_image_Tick(object sender, EventArgs e) {
      _filter_enabled = false;
      _btn_filter_noise.Enabled = true;
    }

    private void _cmb_laser_color_SelectedIndexChanged(object sender, EventArgs e) {
      Context.Laser.ClearThresholdImage();
      Context.Laser.Channel = _cmb_laser_color.SelectedIndex;
    }

    private void _btn_filter_noise_Click(object sender, EventArgs e) {
      _btn_filter_noise.Enabled = false;
      _filter_enabled = true;
      _timer_take_image.Enabled = true;
    }
  }
}
