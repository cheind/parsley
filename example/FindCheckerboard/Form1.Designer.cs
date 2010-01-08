namespace FindCheckerboard {
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
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
      this._image_box = new Emgu.CV.UI.ImageBox();
      ((System.ComponentModel.ISupportInitialize)(this._image_box)).BeginInit();
      this.SuspendLayout();
      // 
      // _image_box
      // 
      this._image_box.Dock = System.Windows.Forms.DockStyle.Fill;
      this._image_box.FunctionalMode = Emgu.CV.UI.ImageBox.FunctionalModeOption.Minimum;
      this._image_box.Location = new System.Drawing.Point(0, 0);
      this._image_box.Name = "_image_box";
      this._image_box.Size = new System.Drawing.Size(583, 398);
      this._image_box.TabIndex = 2;
      this._image_box.TabStop = false;
      // 
      // Form1
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(583, 398);
      this.Controls.Add(this._image_box);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "Form1";
      this.Text = "Find Checkerboard";
      ((System.ComponentModel.ISupportInitialize)(this._image_box)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private Emgu.CV.UI.ImageBox _image_box;

  }
}

