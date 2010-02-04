namespace Parsley {
  partial class WorldSetupSlide {
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
      this._pg = new System.Windows.Forms.PropertyGrid();
      this.SuspendLayout();
      // 
      // _pg
      // 
      this._pg.BackColor = System.Drawing.Color.White;
      this._pg.CategoryForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
      this._pg.CommandsDisabledLinkColor = System.Drawing.Color.Silver;
      this._pg.Dock = System.Windows.Forms.DockStyle.Fill;
      this._pg.HelpForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
      this._pg.Location = new System.Drawing.Point(0, 0);
      this._pg.Name = "_pg";
      this._pg.PropertySort = System.Windows.Forms.PropertySort.NoSort;
      this._pg.Size = new System.Drawing.Size(419, 372);
      this._pg.TabIndex = 0;
      this._pg.ToolbarVisible = false;
      this._pg.ViewForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
      // 
      // WorldSetupSlide
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this._pg);
      this.Name = "WorldSetupSlide";
      this.Size = new System.Drawing.Size(419, 372);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.PropertyGrid _pg;
  }
}
