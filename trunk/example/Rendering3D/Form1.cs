using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Rendering3D {
  public partial class Form1 : Form {
    private Parsley.Draw3D.Viewer _viewer;
    private BackgroundWorker _bw;
    private Random _rnd;

    public Form1() {
      InitializeComponent();
      _viewer = new Parsley.Draw3D.Viewer(_render_target);
      _rnd = new Random();
      _bw = new BackgroundWorker();
      _bw.DoWork += new DoWorkEventHandler(Render);
      _bw.RunWorkerAsync();
    }

    void Render(object sender, DoWorkEventArgs e) {
      BackgroundWorker bw = sender as BackgroundWorker;
      while (!bw.CancellationPending) {
        System.Threading.Monitor.Enter(_viewer);
        _viewer.Frame();
        System.Threading.Monitor.Exit(_viewer);
      }
      e.Cancel = true;
    }

    private void _button_add_capsule_Click(object sender, EventArgs e) {
      System.Threading.Monitor.Enter(_viewer);
      _viewer.AddCapsule(_rnd.Next(-10, 10), _rnd.Next(-10, 10), _rnd.Next(-10, 10));
      System.Threading.Monitor.Exit(_viewer);
    }
  }
}
