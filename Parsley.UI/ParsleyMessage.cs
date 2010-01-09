using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Parsley.UI {
  public partial class ParsleyMessage : Form {
    public ParsleyMessage(string caption, string message) {
      InitializeComponent();
      this.Text = caption;
      _text.Text = message;
    }

    private void _button_ok_Click(object sender, EventArgs e) {
      this.Close();
    }

    public static void Show(IWin32Window owner, string caption, string message) {
      ParsleyMessage pm = new ParsleyMessage(caption, message);
      pm.ShowDialog(owner);
    }

    public static void Show(string caption, string message) {
      ParsleyMessage pm = new ParsleyMessage(caption, message);
      pm.ShowDialog();
    }
  }
}
