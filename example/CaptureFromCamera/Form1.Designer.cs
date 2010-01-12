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
      this._display = new Parsley.UI.Concrete.EmbeddableStream();
      this.SuspendLayout();
      // 
      // _display
      // 
      this._display.Dock = System.Windows.Forms.DockStyle.Fill;
      this._display.Location = new System.Drawing.Point(0, 0);
      this._display.Name = "_display";
      this._display.Size = new System.Drawing.Size(994, 599);
      this._display.TabIndex = 0;
      // 
      // Form1
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(994, 599);
      this.Controls.Add(this._display);
      this.Name = "Form1";
      this.Text = "Form1";
      this.ResumeLayout(false);

    }

    #endregion

    private Parsley.UI.Concrete.EmbeddableStream _display;


  }
}

