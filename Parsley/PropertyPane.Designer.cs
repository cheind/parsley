namespace Parsley {
  partial class PropertyPane {
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
      this._pg_config = new System.Windows.Forms.PropertyGrid();
      this.growLabel1 = new Parsley.UI.GrowLabel();
      this.SuspendLayout();
      // 
      // _pg_config
      // 
      this._pg_config.Dock = System.Windows.Forms.DockStyle.Fill;
      this._pg_config.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this._pg_config.Location = new System.Drawing.Point(0, 21);
      this._pg_config.Name = "_pg_config";
      this._pg_config.PropertySort = System.Windows.Forms.PropertySort.NoSort;
      this._pg_config.Size = new System.Drawing.Size(308, 406);
      this._pg_config.TabIndex = 3;
      this._pg_config.ToolbarVisible = false;
      // 
      // growLabel1
      // 
      this.growLabel1.Dock = System.Windows.Forms.DockStyle.Top;
      this.growLabel1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.growLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(129)))), ((int)(((byte)(41)))));
      this.growLabel1.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.growLabel1.Location = new System.Drawing.Point(0, 0);
      this.growLabel1.MinimumSize = new System.Drawing.Size(0, 20);
      this.growLabel1.Name = "growLabel1";
      this.growLabel1.Size = new System.Drawing.Size(308, 21);
      this.growLabel1.TabIndex = 14;
      this.growLabel1.Text = "Properties";
      this.growLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // PropertyPane
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this._pg_config);
      this.Controls.Add(this.growLabel1);
      this.Name = "PropertyPane";
      this.Size = new System.Drawing.Size(308, 427);
      this.VisibleChanged += new System.EventHandler(this.PropertyPane_VisibleChanged);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.PropertyGrid _pg_config;
    private Parsley.UI.GrowLabel growLabel1;
  }
}
