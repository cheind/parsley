using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Parsley {
  public partial class ExamplesSlide : UI.Slide {
    public ExamplesSlide() {
      InitializeComponent();
    }

    private void _btn_extract_laser_line_Click(object sender, EventArgs e) {
      this.SlideControl.ForwardTo<Examples.ExtractLaserLineSlide>();
    }
  }
}
