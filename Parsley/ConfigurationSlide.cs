using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Parsley {
  public partial class ConfigurationSlide : ContextSlide {

    public ConfigurationSlide(Context c)
      : base(c) 
    {
      InitializeComponent();
    }

    private ConfigurationSlide() {
      InitializeComponent();
    }

    private void _btn_load_Click(object sender, EventArgs e) {
      if (_open_dlg.ShowDialog(this) == DialogResult.OK) {
        Context.LoadBinary(_open_dlg.FileName);
      }
    }

    private void _btn_save_Click(object sender, EventArgs e) {
      if (_save_dialog.ShowDialog(this) == DialogResult.OK) {
        Context.SaveBinary(_save_dialog.FileName);
      }
    }
  }
}
