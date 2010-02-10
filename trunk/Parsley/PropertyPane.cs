using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Parsley {
  public partial class PropertyPane : UserControl {
    private Context _context;

    public PropertyPane() {
      InitializeComponent();
    }

    public PropertyPane(Context c) {
      _context = c;
    }

    private void PropertyPane_VisibleChanged(object sender, EventArgs e) {
      if (this.Visible) {
        if (_context != null) {
          _pg_config.SelectedObject = _context.Setup;
        }
        this.UpdatePosition();
      }
    }

    public Context Context {
      set { _context = value; }
    }

    public void UpdatePosition() {
      Padding p = this.Parent.Padding;
      Size s = this.Parent.Size;

      this.Location = new Point(s.Width - this.Width, p.Top);
      this.Height = s.Height - (p.Top + p.Bottom);
    }
  }
}
