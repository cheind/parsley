using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Parsley.Logging {
  public class StatusStripAppender : log4net.Appender.AppenderSkeleton {
    private ToolStripStatusLabel _status_label;
    private StatusStrip _status_strip;

    public StatusStripAppender() {}

    public StatusStripAppender(StatusStrip parent, ToolStripStatusLabel label) {
      _status_strip = parent;
      _status_label = label;
    }

    public ToolStripStatusLabel ToolStripStatusLabel {
      set { _status_label = value; }
    }

    public StatusStrip StatusStrip {
      set { _status_strip = value; }
    }

    protected override void Append(log4net.Core.LoggingEvent loggingEvent) {
      if (_status_strip != null && _status_label != null) {
        Image i = (loggingEvent.Level > log4net.Core.Level.Info) ? Properties.Resources.error : Properties.Resources.ok;
        if (_status_strip.InvokeRequired) {
          _status_strip.BeginInvoke(new MethodInvoker(delegate {
            _status_label.Text = loggingEvent.RenderedMessage;
            _status_label.Image = i;
          }));
        } else {
          _status_label.Text = loggingEvent.RenderedMessage;
          _status_label.Image = i;
        }
      }
    }
  }
}
