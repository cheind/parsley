namespace Parsley.UI {
  partial class ParsleySlide {
    /// <summary> 
    /// Erforderliche Designervariable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary> 
    /// Verwendete Ressourcen bereinigen.
    /// </summary>
    /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
    protected override void Dispose(bool disposing) {
      if (disposing && (components != null)) {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Vom Komponenten-Designer generierter Code

    /// <summary> 
    /// Erforderliche Methode für die Designerunterstützung. 
    /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
    /// </summary>
    private void InitializeComponent() {
      this.growLabel1 = new Parsley.UI.GrowLabel();
      this.SuspendLayout();
      // 
      // growLabel1
      // 
      this.growLabel1.Dock = System.Windows.Forms.DockStyle.Top;
      this.growLabel1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.growLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(129)))), ((int)(((byte)(41)))));
      this.growLabel1.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.growLabel1.Location = new System.Drawing.Point(0, 0);
      this.growLabel1.MinimumSize = new System.Drawing.Size(0, 30);
      this.growLabel1.Name = "growLabel1";
      this.growLabel1.Size = new System.Drawing.Size(550, 30);
      this.growLabel1.TabIndex = 13;
      this.growLabel1.Text = "Intrinsic Camera Calibration";
      // 
      // ParsleySlide
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.Color.White;
      this.Controls.Add(this.growLabel1);
      this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ForeColor = System.Drawing.Color.Black;
      this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
      this.Name = "ParsleySlide";
      this.Size = new System.Drawing.Size(550, 173);
      this.ResumeLayout(false);

    }

    #endregion

    protected GrowLabel growLabel1;

  }
}
