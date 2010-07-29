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
    List<Emgu.CV.ExtrinsicCameraParameters> _ecps;

    public ReferenceBody() {
      _planes = new List<Plane>();
      _planes.Add(null);
      _planes.Add(null);
      _ecps = new List<Emgu.CV.ExtrinsicCameraParameters>();
      _ecps.Add(null);
      _ecps.Add(null);
    }

    /// <summary>
    /// Get the recorded reference planes
    /// </summary>
    [Browsable(false)]
    public List<Plane> Planes {
      get { return _planes; }
    }

    [Editor(typeof(ExtrinsicTypeEditor),
            typeof(System.Drawing.Design.UITypeEditor))]
    public Emgu.CV.ExtrinsicCameraParameters LeftPlane {
      get {
        return _ecps[0];
      }
      set {
        if (value != null) {
          _ecps[0] = value;
          _planes[0] = new Plane(value);
        }
      }
    }

    [Editor(typeof(ExtrinsicTypeEditor),
            typeof(System.Drawing.Design.UITypeEditor))]
    public Emgu.CV.ExtrinsicCameraParameters RightPlane {
      get {
        return _ecps[1];
      }
      set {
        if (value != null) {
          _ecps[1] = value;
          _planes[1] = new Plane(value);
        }
      }
    }
  }
}
