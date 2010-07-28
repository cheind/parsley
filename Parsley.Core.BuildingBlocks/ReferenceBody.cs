using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace Parsley.Core.BuildingBlocks {

  [Serializable]
  public class ReferenceBody {
    List<Plane> _planes;
    List<string> _plane_filenames;

    public ReferenceBody() {
      _planes = new List<Plane>();
      _planes.Add(null);
      _planes.Add(null);
      _plane_filenames = new List<string>();
      _plane_filenames.Add("not-set");
      _plane_filenames.Add("not-set");
    }

    /// <summary>
    /// Get the recorded reference planes
    /// </summary>
    [Browsable(false)]
    public List<Plane> Planes {
      get { return _planes; }
    }

    [Editor(typeof(ExtrinsicsFileNameEditor),
            typeof(System.Drawing.Design.UITypeEditor))]
    public string LeftPlane {
      get {
        return _plane_filenames[0];
      }
      set {
        Emgu.CV.ExtrinsicCameraParameters ecp = LoadExtrinsics(value);
        if (ecp != null) {
          _plane_filenames[0] = value;
          _planes[0] = new Plane(ecp);
        }
      }
    }

    [Editor(typeof(ExtrinsicsFileNameEditor),
        typeof(System.Drawing.Design.UITypeEditor))]
    public string RightPlane {
      get {
        return _plane_filenames[1];
      }
      set {
        Emgu.CV.ExtrinsicCameraParameters ecp = LoadExtrinsics(value);
        if (ecp != null) {
          _plane_filenames[1] = value;
          _planes[1] = new Plane(ecp);
        }
      }
    }

    private Emgu.CV.ExtrinsicCameraParameters LoadExtrinsics(string filename) {
      Emgu.CV.ExtrinsicCameraParameters ecp = null;
      try {
        using (Stream s = File.Open(filename, FileMode.Open)) {
          if (s != null) {
            IFormatter formatter = new BinaryFormatter();
            ecp = formatter.Deserialize(s) as Emgu.CV.ExtrinsicCameraParameters;
            s.Close();
          }
        }
      } catch (Exception) { }
      return ecp;
    }

    internal class ExtrinsicsFileNameEditor : System.Windows.Forms.Design.FileNameEditor {
      protected override void InitializeDialog(OpenFileDialog openFileDialog) {
        base.InitializeDialog(openFileDialog);
        openFileDialog.Filter = "Extrinsic Files|*.ecp";
      }
    }
  }
}
