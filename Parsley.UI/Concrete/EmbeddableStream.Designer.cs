namespace Parsley.UI.Concrete {
  partial class EmbeddableStream {
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
      this.components = new System.ComponentModel.Container();
      this._picture_box = new Emgu.CV.UI.ImageBox();
      ((System.ComponentModel.ISupportInitialize)(this._picture_box)).BeginInit();
      this.SuspendLayout();
      // 
      // _picture_box
      // 
      this._picture_box.Dock = System.Windows.Forms.DockStyle.Fill;
      this._picture_box.Location = new System.Drawing.Point(0, 0);
      this._picture_box.Name = "_picture_box";
      this._picture_box.Size = new System.Drawing.Size(544, 381);
      this._picture_box.TabIndex = 2;
      this._picture_box.TabStop = false;
      // 
      // FrameGrabber
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this._picture_box);
      this.Name = "FrameGrabber";
      this.Size = new System.Drawing.Size(544, 381);
      ((System.ComponentModel.ISupportInitialize)(this._picture_box)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private Emgu.CV.UI.ImageBox _picture_box;
  }
}
