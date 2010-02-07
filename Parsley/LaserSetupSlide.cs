using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Parsley {
  public partial class LaserSetupSlide : FrameGrabberSlide {

    private bool _filter_enabled;
    private bool _save_laser_data;

    public LaserSetupSlide(Context c) : base(c) 
    {
      InitializeComponent();
    }

    public LaserSetupSlide() :base(null) {
      InitializeComponent();
    }

    protected override void OnSlidingIn() {
      base.OnSlidingIn();
    }

    protected override void OnFrame(Parsley.Core.BuildingBlocks.FrameGrabber fp, Emgu.CV.Image<Emgu.CV.Structure.Bgr, byte> img) 
    {
      if (_filter_enabled) {
        Context.World.Laser.AddThresholdImage(img);
      } else {
        Context.World.Laser.FindLaserLine(img);
        if (_save_laser_data) {
          _save_laser_data = false;
          using (TextWriter tw = new StreamWriter(string.Format("laser_{0}.txt", Guid.NewGuid()))) {
            foreach (System.Drawing.PointF p in Context.World.Laser.ValidLaserPoints) {
              tw.WriteLine(String.Format("{0};{1}",p.X, p.Y));
            }
          }
        }

        foreach (System.Drawing.PointF p in Context.World.Laser.ValidLaserPoints) {
          img[(int)p.Y, (int)p.X] = new Emgu.CV.Structure.Bgr(System.Drawing.Color.Green);
        }
      }
    }

    private void _timer_take_image_Tick(object sender, EventArgs e) {
      _filter_enabled = false;
      _btn_filter_noise.Enabled = true;
    }

    private void _btn_filter_noise_Click(object sender, EventArgs e) {
      _btn_filter_noise.Enabled = false;
      _filter_enabled = true;
      _timer_take_image.Enabled = true;
    }

    private void parsleyButtonSmall1_Click(object sender, EventArgs e) {
      _save_laser_data = true;
    }
  }
}
