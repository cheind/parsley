using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Parsley.Examples {
  public partial class ExtractLaserLineSlide : FrameGrabberSlide {
    private Emgu.CV.Image<Emgu.CV.Structure.Bgr, Byte> _reference;
    private Parsley.Core.BrightestPixelLLE _lle;
    private int _channel;

    public ExtractLaserLineSlide(Core.FrameGrabber fg) : base(fg) {
      InitializeComponent();
      _lle = new Parsley.Core.BrightestPixelLLE();
      _channel = 2;
      _cmb_channel.SelectedIndex = _channel;
      _reference = null;
    }

    override protected void OnFrame(Parsley.Core.FrameProducer fp, Emgu.CV.Image<Emgu.CV.Structure.Bgr, byte> img) {
      Emgu.CV.Image<Emgu.CV.Structure.Bgr, Byte> my_ref = _reference;
      int my_channel = _channel;

      if (my_ref != null) {
        _lle.FindLaserLine(img[my_channel].Sub(my_ref[my_channel]));
      } else {
        _lle.FindLaserLine(img[my_channel]);
      }

      for (int c = 0; c < _lle.LaserPoints.Length; c++) {
        if (_lle.LaserPoints[c] > 0.0f) {
          img[(int)_lle.LaserPoints[c], c] = new Emgu.CV.Structure.Bgr(255, 0, 0);
        }
      }
      
    }

    private void _btn_take_reference_Click(object sender, EventArgs e) {
      _reference = this.FrameGrabber.Camera.Frame().Copy();
    }

    private void _cmb_channel_SelectedIndexChanged(object sender, EventArgs e) {
      _channel = _cmb_channel.SelectedIndex;
    }

    private void _num_threshold_ValueChanged_1(object sender, EventArgs e) {
      _lle.IntensityThreshold = (int)_num_threshold.Value;
    }
  }
}
