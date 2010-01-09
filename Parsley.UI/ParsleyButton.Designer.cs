namespace Parsley.UI {
  partial class ParsleyButton {
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
      this.SuspendLayout();
      // 
      // ParsleyButton
      // 
      this.BackColor = System.Drawing.Color.White;
      this.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
      this.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.Font = new System.Drawing.Font("Calibri", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
      this.Image = global::Parsley.UI.Properties.Resources.parsley48;
      this.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.Size = new System.Drawing.Size(300, 60);
      this.Text = "Parsley Me!";
      this.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.UseVisualStyleBackColor = false;
      this.ResumeLayout(false);

    }

    #endregion
  }
}
