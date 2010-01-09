namespace DisplayCheckerboard3D {
  partial class Form1 {
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

    #region Vom Windows Form-Designer generierter Code

    /// <summary>
    /// Erforderliche Methode für die Designerunterstützung.
    /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
    /// </summary>
    private void InitializeComponent() {
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
      this.statusStrip1 = new System.Windows.Forms.StatusStrip();
      this._status_label = new System.Windows.Forms.ToolStripStatusLabel();
      this._picture_box = new Emgu.CV.UI.ImageBox();
      this.statusStrip1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this._picture_box)).BeginInit();
      this.SuspendLayout();
      // 
      // statusStrip1
      // 
      this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._status_label});
      this.statusStrip1.Location = new System.Drawing.Point(0, 373);
      this.statusStrip1.Name = "statusStrip1";
      this.statusStrip1.Size = new System.Drawing.Size(608, 22);
      this.statusStrip1.TabIndex = 4;
      this.statusStrip1.Text = "statusStrip1";
      // 
      // _status_label
      // 
      this._status_label.Name = "_status_label";
      this._status_label.Size = new System.Drawing.Size(66, 17);
      this._status_label.Text = "Set on start";
      // 
      // _picture_box
      // 
      this._picture_box.Dock = System.Windows.Forms.DockStyle.Fill;
      this._picture_box.Location = new System.Drawing.Point(0, 0);
      this._picture_box.Name = "_picture_box";
      this._picture_box.Size = new System.Drawing.Size(608, 373);
      this._picture_box.TabIndex = 2;
      this._picture_box.TabStop = false;
      // 
      // Form1
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(608, 395);
      this.Controls.Add(this._picture_box);
      this.Controls.Add(this.statusStrip1);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "Form1";
      this.Text = "DisplayCheckerboard3D";
      this.statusStrip1.ResumeLayout(false);
      this.statusStrip1.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this._picture_box)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.StatusStrip statusStrip1;
    private System.Windows.Forms.ToolStripStatusLabel _status_label;
    private Emgu.CV.UI.ImageBox _picture_box;
  }
}

