using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Design;
using System.Windows.Forms.Design;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace Parsley.Core.BuildingBlocks {

  /// <summary>
  /// Type editor to load intrinsics from file and assign them to properties.
  /// </summary>
  [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
  public class IntrinsicTypeEditor : UITypeEditor {

    public IntrinsicTypeEditor() {
    }

    public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context) {
      // Indicates that this editor can display a Form-based interface.
      return UITypeEditorEditStyle.Modal;
    }

    public override object EditValue(
        ITypeDescriptorContext context,
        IServiceProvider provider,
        object value) {

      if (context == null) {
        return null;
      }

      
      using (OpenFileDialog ofd = new OpenFileDialog()) {
        ofd.Filter = "Intrinsic Files|*.icp";
        if (ofd.ShowDialog() == DialogResult.OK) {
          Emgu.CV.IntrinsicCameraParameters icp = null;
          try {
            using (Stream s = File.Open(ofd.FileName, FileMode.Open)) {
              if (s != null) {
                IFormatter formatter = new BinaryFormatter();
                icp = formatter.Deserialize(s) as Emgu.CV.IntrinsicCameraParameters;
                s.Close();
              }
            }
          } catch (Exception) {}
          return icp;
        } else {
          return value;
        }
      } 
    }
  }

}
