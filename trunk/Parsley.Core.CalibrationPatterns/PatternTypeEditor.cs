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

namespace Parsley.Core.CalibrationPatterns
{
  /// <summary>
  /// Type editor to load pattern from file and assign them to properties.
  /// </summary>
  [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
  public class PatternTypeEditor : UITypeEditor
  {
    public PatternTypeEditor()
    {
    }

    public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
    {
      // Indicates that this editor can display a Form-based interface.
      return UITypeEditorEditStyle.Modal;
    }

    public override object EditValue(
        ITypeDescriptorContext context,
        IServiceProvider provider,
        object value)
    {

      if (context == null)
      {
        return null;
      }

      using (OpenFileDialog ofd = new OpenFileDialog())
      {
        ofd.Filter = "Pattern Files|*.pattern";
        if (ofd.ShowDialog() == DialogResult.OK)
        {
          Parsley.Core.CalibrationPattern pattern = null;
          try
          {
            using (Stream s = File.Open(ofd.FileName, FileMode.Open))
            {
              if (s != null)
              {
                IFormatter formatter = new BinaryFormatter();
                pattern = formatter.Deserialize(s) as Parsley.Core.CalibrationPattern;
                s.Close();
              }
            }
          }
          catch (Exception) { }
          return pattern;
        }
        else
        {
          return value;
        }
      }
    }
  }
}
