using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Parsley.UI {

  public class GrowLabel : Label {
    private bool mGrowing;
    
    public GrowLabel() {
      this.AutoSize = false;
    }
    private void resizeLabel() {
      if (mGrowing) return;
      try {
        mGrowing = true;
        Size sz = new Size(this.Width, Int32.MaxValue);
        sz = TextRenderer.MeasureText(this.Text, this.Font, sz, TextFormatFlags.WordBreak);
        this.Height = sz.Height;
      } finally {
        mGrowing = false;
      }
    }
    protected override void OnTextChanged(EventArgs e) {
      base.OnTextChanged(e);
      resizeLabel();
    }
    protected override void OnFontChanged(EventArgs e) {
      base.OnFontChanged(e);
      resizeLabel();
    }
    protected override void OnSizeChanged(EventArgs e) {
      base.OnSizeChanged(e);
      resizeLabel();
    }

    private void InitializeComponent() {
      this.SuspendLayout();
      // 
      // GrowLabel
      // 
      this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
      this.ResumeLayout(false);

    }
  }
}
