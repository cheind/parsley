namespace CaptureFromCamera {
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
      this._picturebox = new Emgu.CV.UI.ImageBox();
      this._property_grid = new System.Windows.Forms.PropertyGrid();
      ((System.ComponentModel.ISupportInitialize)(this._picturebox)).BeginInit();
      this.SuspendLayout();
      // 
      // _picturebox
      // 
      this._picturebox.FunctionalMode = Emgu.CV.UI.ImageBox.FunctionalModeOption.Minimum;
      this._picturebox.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Default;
      this._picturebox.Location = new System.Drawing.Point(25, 28);
      this._picturebox.Name = "_picturebox";
      this._picturebox.Size = new System.Drawing.Size(640, 480);
      this._picturebox.TabIndex = 2;
      this._picturebox.TabStop = false;
      // 
      // _property_grid
      // 
      this._property_grid.Location = new System.Drawing.Point(706, 28);
      this._property_grid.Name = "_property_grid";
      this._property_grid.Size = new System.Drawing.Size(251, 480);
      this._property_grid.TabIndex = 3;
      // 
      // Form1
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(994, 599);
      this.Controls.Add(this._property_grid);
      this.Controls.Add(this._picturebox);
      this.Name = "Form1";
      this.Text = "Form1";
      ((System.ComponentModel.ISupportInitialize)(this._picturebox)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private Emgu.CV.UI.ImageBox _picturebox;
    private System.Windows.Forms.PropertyGrid _property_grid;

  }
}

