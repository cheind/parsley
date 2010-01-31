using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Parsley.UI.Concrete {
  public partial class Draw3DViewer : AspectRatioForm {
    private Core.BuildingBlocks.RenderLoop _rl;

    public Draw3DViewer() {
      InitializeComponent();
      _rl = new Core.BuildingBlocks.RenderLoop(_render_target);
    }

    /// <summary>
    /// Access the Core.RenderLoop
    /// </summary>
    public Core.BuildingBlocks.RenderLoop RenderLoop {
      get { return _rl; }
    }

    /// <summary>
    /// Access the Draw3D.Viewer
    /// </summary>
    public Draw3D.Viewer Viewer {
      get { return _rl.Viewer; }
    }
  }
}
