using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Parsley {
  public partial class SetupSlide : ContextSlide {

    public SetupSlide(Context c) : base(c) {
      InitializeComponent();
      _open_dlg.InitialDirectory = System.Environment.CurrentDirectory;
      _save_dialog.InitialDirectory = System.Environment.CurrentDirectory;
    }

    public SetupSlide()
      : base(null) 
    {
      InitializeComponent();
    }

    protected override void OnSlidingIn() {
      _numeric_device_id.Value = Context.World.Camera.DeviceIndex;
      if (_numeric_device_id.Value == -1) {
        _error_provider.SetError(_numeric_device_id, "No such camera");
      }
      _btn_save_calibration.Enabled = Context.World.Camera.HasIntrinsics;
      base.OnSlidingIn();
    }

    protected override void OnSlidingOut(CancelEventArgs args) {
      bool has_errors = _error_provider.GetError(_numeric_device_id) != "";
      
      if (has_errors) {
        args.Cancel = true;
        UI.ParsleyMessage.Show("Correct Errors", "One or more controls has errors.");
      }
      base.OnSlidingOut(args);
    }

    private void _numeric_device_id_ValueChanged(object sender, EventArgs e) {
      Context.World.Camera.DeviceIndex = (int)_numeric_device_id.Value;
      if (Context.World.Camera.DeviceIndex == -1) {
        _error_provider.SetError(_numeric_device_id, "No such camera");
      } else {
        _error_provider.SetError(_numeric_device_id, "");
      }
    }

    private void _btn_intrinsic_calibration_Click(object sender, EventArgs e) {
      this.SlideControl.ForwardTo<IntrinsicCalibrationSlide>();
    }

    private void _btn_extrinsic_calibration_Click(object sender, EventArgs e) {
      this.SlideControl.ForwardTo<ExtrinsicCalibrationSlide>();
    }

    private void _btn_save_calibration_Click(object sender, EventArgs e) {
      if (_save_dialog.ShowDialog(this) == DialogResult.OK) {
        Context.World.Camera.SaveCalibration(_save_dialog.FileName);
      }
    }

    private void _btn_load_calibration_Click(object sender, EventArgs e) {
      if (_open_dlg.ShowDialog(this) == DialogResult.OK) {
        Context.World.Camera.LoadCalibration(_open_dlg.FileName);
      }
    }

    private void _btn_laser_setup_Click(object sender, EventArgs e) {
      this.SlideControl.ForwardTo<LaserSetupSlide>();
    }
  }
}
