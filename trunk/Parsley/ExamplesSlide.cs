using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Parsley {
  public partial class ExamplesSlide : SlickInterface.Slide {
    public ExamplesSlide() {
      InitializeComponent();
    }

    private void _btn_extract_laser_line_Click(object sender, EventArgs e) {
      this.SlideControl.ForwardTo<Examples.ExtractLaserLineSlide>();
    }

    private void _btn_track_calibration_pattern_Click(object sender, EventArgs e) {
      this.SlideControl.ForwardTo<Examples.TrackCheckerboard3D>();
    }

    private void _btn_display_roi_Click(object sender, EventArgs e) {
      this.SlideControl.ForwardTo<Examples.ROISlide>();
    }

    private void _btn_scanning_Click(object sender, EventArgs e) {
      this.SlideControl.ForwardTo<Examples.ScanningAttempt>();
    }
  }
}
