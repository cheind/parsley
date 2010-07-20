using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Parsley.Logging {
  public partial class LogFileDisplay : Form {
    FileSystemWatcher _watcher;
    string _full_path;

    public LogFileDisplay() {
      InitializeComponent();
      _watcher = new FileSystemWatcher();
      _watcher.Path = Path.GetDirectoryName(Application.ExecutablePath);
      _watcher.Filter = "Parsley.log.txt";
      _full_path = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "Parsley.log.txt");
      _watcher.NotifyFilter = NotifyFilters.CreationTime | NotifyFilters.Size | NotifyFilters.LastWrite;
      _watcher.Changed += new FileSystemEventHandler(_watcher_Changed);
      _watcher.Created += new FileSystemEventHandler(_watcher_Created);
      _watcher.EnableRaisingEvents = true;
    }

    private void UpdateContent() {
      using (TextReader tr = new StreamReader(_full_path)) {
        _txt_content.Text = tr.ReadToEnd();
        _txt_content.SelectionStart = _txt_content.Text.Length;
        _txt_content.ScrollToCaret();
        _txt_content.Refresh();
      }
    }

    void _watcher_Created(object sender, FileSystemEventArgs e) {
      if (_txt_content.InvokeRequired) {
        _txt_content.BeginInvoke(new MethodInvoker(delegate { UpdateContent(); }));
      }
    }

    void _watcher_Changed(object sender, FileSystemEventArgs e) {
      if (_txt_content.InvokeRequired) {
        _txt_content.BeginInvoke(new MethodInvoker(delegate { UpdateContent(); }));
      }
    }

    private void _cb_auto_update_CheckedChanged(object sender, EventArgs e) {
      _watcher.EnableRaisingEvents = _cb_auto_update.Checked;
      if (_cb_auto_update.Checked) {
        UpdateContent();
      }
    }

    private void LogFileDisplay_FormClosing(object sender, FormClosingEventArgs e) {
      _watcher.EnableRaisingEvents = false;
    }

    private void LogFileDisplay_Shown(object sender, EventArgs e) {
      UpdateContent();
    }
  }
}
