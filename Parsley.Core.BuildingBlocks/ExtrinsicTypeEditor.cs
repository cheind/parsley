/*
 * Parsley http://parsley.googlecode.com
 * Copyright (c) 2010, Christoph Heindl
 * Copyright (c) 2010, Matthias Plasch
 * All rights reserved.
 * Code license:	New BSD License
 */
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
  /// Type editor to load extrinsics from file and assign them to properties.
  /// </summary>
  [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
  public class ExtrinsicTypeEditor : UITypeEditor {

    public ExtrinsicTypeEditor() {
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
        ofd.Filter = "Extrinsic Files|*.ecp";
        if (ofd.ShowDialog() == DialogResult.OK) {
          Emgu.CV.ExtrinsicCameraParameters ecp = null;
          try {
            using (Stream s = File.Open(ofd.FileName, FileMode.Open)) {
              if (s != null) {
                IFormatter formatter = new BinaryFormatter();
                ecp = formatter.Deserialize(s) as Emgu.CV.ExtrinsicCameraParameters;
                s.Close();
              }
            }
          } catch (Exception) { }
          return ecp;
        } else {
          return value;
        }
      } 
    }
  }

}
