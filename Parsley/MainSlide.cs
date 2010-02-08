using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Parsley {
  public partial class MainSlide : SlickInterface.Slide {
    public MainSlide() {
      InitializeComponent();
    }

    private void _btn_example_Click(object sender, EventArgs e) {
      this.SlideControl.ForwardTo<ExamplesSlide>();
    }

    private void _btn_intrinsic_calibration_Click(object sender, EventArgs e) {
      this.SlideControl.ForwardTo<IntrinsicCalibrationSlide>();
    }

    private void _btn_extrinsic_calibration_Click(object sender, EventArgs e) {
      this.SlideControl.ForwardTo<ExtrinsicCalibrationSlide>();
    }

  }
}
