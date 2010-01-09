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
      this._button_3d = new Parsley.UI.ParsleyButton();
      this._button_calibrate = new Parsley.UI.ParsleyButton();
      this.SuspendLayout();
      // 
      // _button_3d
      // 
      this._button_3d.BackColor = System.Drawing.Color.White;
      this._button_3d.Enabled = false;
      this._button_3d.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
      this._button_3d.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this._button_3d.Font = new System.Drawing.Font("Calibri", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this._button_3d.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
      this._button_3d.Image = ((System.Drawing.Image)(resources.GetObject("_button_3d.Image")));
      this._button_3d.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this._button_3d.Location = new System.Drawing.Point(154, 200);
      this._button_3d.Name = "_button_3d";
      this._button_3d.Size = new System.Drawing.Size(300, 60);
      this._button_3d.TabIndex = 4;
      this._button_3d.Text = "Locate in 3D";
      this._button_3d.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this._button_3d.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this._button_3d.UseVisualStyleBackColor = true;
      this._button_3d.Click += new System.EventHandler(this._button_3d_Click);
      // 
      // _button_calibrate
      // 
      this._button_calibrate.BackColor = System.Drawing.Color.White;
      this._button_calibrate.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
      this._button_calibrate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this._button_calibrate.Font = new System.Drawing.Font("Calibri", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this._button_calibrate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
      this._button_calibrate.Image = ((System.Drawing.Image)(resources.GetObject("_button_calibrate.Image")));
      this._button_calibrate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this._button_calibrate.Location = new System.Drawing.Point(154, 134);
      this._button_calibrate.Name = "_button_calibrate";
      this._button_calibrate.Size = new System.Drawing.Size(300, 60);
      this._button_calibrate.TabIndex = 3;
      this._button_calibrate.Text = "Calibrate Camera";
      this._button_calibrate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this._button_calibrate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this._button_calibrate.UseVisualStyleBackColor = true;
      this._button_calibrate.Click += new System.EventHandler(this._button_calibrate_Click);
      // 
      // Form1
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.Color.White;
      this.ClientSize = new System.Drawing.Size(608, 395);
      this.Controls.Add(this._button_3d);
      this.Controls.Add(this._button_calibrate);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "Form1";
      this.Text = "DisplayCheckerboard3D";
      this.ResumeLayout(false);

    }

    #endregion

    private Parsley.UI.ParsleyButton _button_calibrate;
    private Parsley.UI.ParsleyButton _button_3d;

  }
}

