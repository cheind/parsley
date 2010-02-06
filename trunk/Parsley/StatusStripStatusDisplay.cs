using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace Parsley {
  public class StatusStripStatusDisplay : IStatusDisplay {
    private ToolStripStatusLabel _status_label;
    private StatusStrip _status_strip;

    public StatusStripStatusDisplay(StatusStrip parent, ToolStripStatusLabel label) {
      _status_strip = parent;
      _status_label = label;
    }


    public void UpdateStatus(string message, Status status) {
      Image i = status == Status.Ok ? Properties.Resources.ok : Properties.Resources.error;
      if (_status_strip.InvokeRequired) {
        _status_strip.BeginInvoke(new MethodInvoker(delegate {
          _status_label.Text = message;
          _status_label.Image = i;
        }));
      } else {
        _status_label.Text = message;
        _status_label.Image = i;
      }
    }
  }
}
