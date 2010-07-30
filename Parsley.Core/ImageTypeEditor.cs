using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Design;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;
using System.ComponentModel;

namespace Parsley.Core {

  [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
  public class ImageTypeEditor : UITypeEditor {

    public ImageTypeEditor() {
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
        ofd.Filter = "Image Files|*.bmp;*.png";
        if (ofd.ShowDialog() == DialogResult.OK) {
          Image<Bgr, byte> image = new Image<Bgr, byte>(ofd.FileName);
          return image;
        } else {
          return value;
        }
      }
    }
  }
}
