using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using log4net;

namespace Parsley {
  public partial class PatternDesignerSlide : UI.ParsleySlide {
    private readonly ILog _logger = LogManager.GetLogger(typeof(PatternDesignerSlide));

    Core.CalibrationPattern _pattern;

    public PatternDesignerSlide() {
      InitializeComponent();

      foreach (Core.Addins.AddinInfo info in Core.Addins.AddinStore.FindAddins(typeof(Core.CalibrationPattern))) {
        _cmb_patterns.Items.Add(info);
      }

    }

    private void _cmb_patterns_SelectedIndexChanged(object sender, EventArgs e) {
      if (_cmb_patterns.SelectedIndex >= 0) {
        // Instance a new object from selected type
        Core.Addins.AddinInfo info = _cmb_patterns.SelectedItem as Core.Addins.AddinInfo;
        if (info != null) {
          _pattern = Core.Addins.AddinStore.CreateInstance(info) as Core.CalibrationPattern;
          _pg.SelectedObject = _pattern;
        }
      }
    }

    private void _btn_save_pattern_Click(object sender, EventArgs e) {
      if (saveFileDialog1.ShowDialog() == DialogResult.OK) {
        using (Stream s = File.OpenWrite(saveFileDialog1.FileName)) {
          if (s != null) {
            IFormatter formatter = new BinaryFormatter();
            formatter.Serialize(s, _pattern);
            s.Close();
            _logger.Info("Pattern successfully saved.");
          }
        }
      }
    }


  }
}
