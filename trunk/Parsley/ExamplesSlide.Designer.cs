namespace Parsley {
  partial class ExamplesSlide {
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExamplesSlide));
      this._btn_extract_laser_line = new Parsley.UI.ParsleyButton();
      this.SuspendLayout();
      // 
      // _btn_extract_laser_line
      // 
      this._btn_extract_laser_line.Anchor = System.Windows.Forms.AnchorStyles.None;
      this._btn_extract_laser_line.BackColor = System.Drawing.Color.White;
      this._btn_extract_laser_line.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
      this._btn_extract_laser_line.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this._btn_extract_laser_line.Font = new System.Drawing.Font("Calibri", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this._btn_extract_laser_line.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
      this._btn_extract_laser_line.Image = ((System.Drawing.Image)(resources.GetObject("_btn_extract_laser_line.Image")));
      this._btn_extract_laser_line.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this._btn_extract_laser_line.Location = new System.Drawing.Point(46, 71);
      this._btn_extract_laser_line.Name = "_btn_extract_laser_line";
      this._btn_extract_laser_line.Size = new System.Drawing.Size(300, 60);
      this._btn_extract_laser_line.TabIndex = 2;
      this._btn_extract_laser_line.Text = "Extract Laser Line";
      this._btn_extract_laser_line.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this._btn_extract_laser_line.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this._btn_extract_laser_line.UseVisualStyleBackColor = true;
      this._btn_extract_laser_line.Click += new System.EventHandler(this._btn_extract_laser_line_Click);
      // 
      // ExamplesSlide
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this._btn_extract_laser_line);
      this.Name = "ExamplesSlide";
      this.Size = new System.Drawing.Size(393, 203);
      this.ResumeLayout(false);

    }

    #endregion

    private Parsley.UI.ParsleyButton _btn_extract_laser_line;

  }
}
