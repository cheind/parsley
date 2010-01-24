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
      this.ReleaseGrab(_grabber);
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
      this._picture_box.FunctionalMode = Emgu.CV.UI.ImageBox.FunctionalModeOption.Minimum;
      this._picture_box.Location = new System.Drawing.Point(0, 0);
      this._picture_box.Name = "_picture_box";
      this._picture_box.Size = new System.Drawing.Size(320, 200);
      this._picture_box.TabIndex = 2;
      this._picture_box.TabStop = false;
      this._picture_box.MouseMove += new System.Windows.Forms.MouseEventHandler(this._picture_box_MouseMove);
      this._picture_box.MouseDown += new System.Windows.Forms.MouseEventHandler(this._picture_box_MouseDown);
      this._picture_box.MouseUp += new System.Windows.Forms.MouseEventHandler(this._picture_box_MouseUp);
      // 
      // EmbeddableStream
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this._picture_box);
      this.Name = "EmbeddableStream";
      this.Size = new System.Drawing.Size(320, 200);
      ((System.ComponentModel.ISupportInitialize)(this._picture_box)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private Emgu.CV.UI.ImageBox _picture_box;
  }
}
