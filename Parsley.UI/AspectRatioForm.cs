using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Parsley.UI {
  public partial class AspectRatioForm : Form {
    private double _aspect_ratio;
    private bool _maintain_aspect_ratio;

    public AspectRatioForm() {
      InitializeComponent();
      _aspect_ratio = ((double)this.Width) / this.Height;
      _maintain_aspect_ratio = false;
    }

    /// <summary>
    /// Aspect ratio defined as width by height
    /// </summary>
    [Description("Aspect ratio defined as width by height")]
    public double AspectRatio {
      get { return _aspect_ratio;}
      set { _aspect_ratio = value;}
    }

    /// <summary>
    /// True to maintain aspect ratio on resize, false otherwise.
    /// </summary>
    public bool IsMaintainingAspectRatio {
      get { return _maintain_aspect_ratio; }
      set { _maintain_aspect_ratio = value; }
    }

    /// <summary>
    /// Call to set height using aspect ratio and width
    /// </summary>
    protected void MaintainAspectRatio() {
      if (_maintain_aspect_ratio) {
        this.Height = (int)Math.Ceiling(this.Width / _aspect_ratio);
      }
    }

    protected override void OnResizeEnd(EventArgs e) {
      this.MaintainAspectRatio();
      base.OnResizeEnd(e);
    }
  }
}
