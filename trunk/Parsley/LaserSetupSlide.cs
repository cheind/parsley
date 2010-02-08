using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace Parsley {
  public partial class LaserSetupSlide : FrameGrabberSlide {
    private bool _save_laser_data;

    public LaserSetupSlide(Context c) : base(c) 
    {
      InitializeComponent();
#if !DEBUG
      this._btn_save_laser_data.Visible = false;
#endif
    }

    public LaserSetupSlide() :base(null) {
      InitializeComponent();
    }

    protected override void OnSlidingIn() {
      base.OnSlidingIn();
    }

    protected override void OnFrame(Parsley.Core.BuildingBlocks.FrameGrabber fp, Emgu.CV.Image<Emgu.CV.Structure.Bgr, byte> img) 
    {
      Context.World.Laser.FindLaserLine(img);
      SaveLaserData();
      foreach (System.Drawing.PointF p in Context.World.Laser.ValidLaserPoints) {
        img[(int)p.Y, (int)p.X] = new Emgu.CV.Structure.Bgr(System.Drawing.Color.Green);
      }
    }

    [Conditional("DEBUG")]
    void SaveLaserData() {
      if (_save_laser_data) {
        _save_laser_data = false;
        using (TextWriter tw = new StreamWriter(string.Format("laser_{0}.txt", Guid.NewGuid()))) {
          foreach (System.Drawing.PointF p in Context.World.Laser.ValidLaserPoints) {
            tw.WriteLine(String.Format("{0};{1}", p.X, p.Y));
          }
        }
      }
    }

    private void _btn_save_laser_data_Click(object sender, EventArgs e) {
      _save_laser_data = true;
    }
  }
}
