using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Emgu.CV.Structure;

namespace Rendering3D {
  public partial class Form1 : Form {
    private Parsley.Draw3D.Viewer _viewer;
    private BackgroundWorker _bw;
    private Random _rnd;

    public Form1() {
      InitializeComponent();
      _viewer = new Parsley.Draw3D.Viewer(_render_target);
      _viewer.LookAt(new MCvPoint3D32f(50.0f, 50.0f, 50.0f), new MCvPoint3D32f(0, 0, 0), new MCvPoint3D32f(0.0f, 1.0f, 0.0f));
      
      Parsley.Draw3D.Axis a = new Parsley.Draw3D.Axis(10);
      _viewer.Add(a);

      _rnd = new Random();
      _bw = new BackgroundWorker();
      _bw.WorkerSupportsCancellation = true;
      _bw.DoWork += new DoWorkEventHandler(Render);
      _bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_bw_RunWorkerCompleted);
      _bw.RunWorkerAsync();
    }

    void _bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
      if (e.Cancelled) {
        // Cancelled through form closing
        _viewer.Dispose();
        this.Close();
      }
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

    protected override void OnFormClosing(FormClosingEventArgs e) {
      if (_bw.IsBusy) {
        _bw.CancelAsync();
        e.Cancel = true;
        this.Enabled = false;
      }
    }

    private void _button_add_capsule_Click(object sender, EventArgs e) {
      System.Threading.Monitor.Enter(_viewer);
      _viewer.AddCapsule(new MCvPoint3D32f(_rnd.Next(-10, 10), _rnd.Next(-10, 10), _rnd.Next(-10, 10)));
      System.Threading.Monitor.Exit(_viewer);
    }
  }
}
