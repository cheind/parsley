namespace Parsley {
  partial class MainSlide {
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainSlide));
      this._btn_extrinsic_calibration = new Parsley.UI.ParsleyButton();
      this._btn_intrinsic_calibration = new Parsley.UI.ParsleyButton();
      this._btn_example = new Parsley.UI.ParsleyButton();
      this._btn_setup = new Parsley.UI.ParsleyButton();
      this.SuspendLayout();
      // 
      // _btn_extrinsic_calibration
      // 
      this._btn_extrinsic_calibration.Anchor = System.Windows.Forms.AnchorStyles.None;
      this._btn_extrinsic_calibration.BackColor = System.Drawing.Color.White;
      this._btn_extrinsic_calibration.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
      this._btn_extrinsic_calibration.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this._btn_extrinsic_calibration.Font = new System.Drawing.Font("Calibri", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this._btn_extrinsic_calibration.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
      this._btn_extrinsic_calibration.Image = ((System.Drawing.Image)(resources.GetObject("_btn_extrinsic_calibration.Image")));
      this._btn_extrinsic_calibration.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this._btn_extrinsic_calibration.Location = new System.Drawing.Point(73, 249);
      this._btn_extrinsic_calibration.Name = "_btn_extrinsic_calibration";
      this._btn_extrinsic_calibration.Size = new System.Drawing.Size(325, 60);
      this._btn_extrinsic_calibration.TabIndex = 4;
      this._btn_extrinsic_calibration.Text = "Extrinsic Calibration";
      this._btn_extrinsic_calibration.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this._btn_extrinsic_calibration.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this._btn_extrinsic_calibration.UseVisualStyleBackColor = true;
      this._btn_extrinsic_calibration.Click += new System.EventHandler(this._btn_extrinsic_calibration_Click);
      // 
      // _btn_intrinsic_calibration
      // 
      this._btn_intrinsic_calibration.Anchor = System.Windows.Forms.AnchorStyles.None;
      this._btn_intrinsic_calibration.BackColor = System.Drawing.Color.White;
      this._btn_intrinsic_calibration.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
      this._btn_intrinsic_calibration.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this._btn_intrinsic_calibration.Font = new System.Drawing.Font("Calibri", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this._btn_intrinsic_calibration.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
      this._btn_intrinsic_calibration.Image = ((System.Drawing.Image)(resources.GetObject("_btn_intrinsic_calibration.Image")));
      this._btn_intrinsic_calibration.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this._btn_intrinsic_calibration.Location = new System.Drawing.Point(73, 183);
      this._btn_intrinsic_calibration.Name = "_btn_intrinsic_calibration";
      this._btn_intrinsic_calibration.Size = new System.Drawing.Size(325, 60);
      this._btn_intrinsic_calibration.TabIndex = 3;
      this._btn_intrinsic_calibration.Text = "Intrinsic Calibration";
      this._btn_intrinsic_calibration.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this._btn_intrinsic_calibration.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this._btn_intrinsic_calibration.UseVisualStyleBackColor = true;
      this._btn_intrinsic_calibration.Click += new System.EventHandler(this._btn_intrinsic_calibration_Click);
      // 
      // _btn_example
      // 
      this._btn_example.Anchor = System.Windows.Forms.AnchorStyles.None;
      this._btn_example.BackColor = System.Drawing.Color.White;
      this._btn_example.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
      this._btn_example.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this._btn_example.Font = new System.Drawing.Font("Calibri", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this._btn_example.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
      this._btn_example.Image = ((System.Drawing.Image)(resources.GetObject("_btn_example.Image")));
      this._btn_example.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this._btn_example.Location = new System.Drawing.Point(73, 117);
      this._btn_example.Name = "_btn_example";
      this._btn_example.Size = new System.Drawing.Size(325, 60);
      this._btn_example.TabIndex = 2;
      this._btn_example.Text = "Run Examples";
      this._btn_example.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this._btn_example.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this._btn_example.UseVisualStyleBackColor = true;
      this._btn_example.Click += new System.EventHandler(this._btn_example_Click);
      // 
      // _btn_setup
      // 
      this._btn_setup.Anchor = System.Windows.Forms.AnchorStyles.None;
      this._btn_setup.BackColor = System.Drawing.Color.White;
      this._btn_setup.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
      this._btn_setup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this._btn_setup.Font = new System.Drawing.Font("Calibri", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this._btn_setup.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
      this._btn_setup.Image = ((System.Drawing.Image)(resources.GetObject("_btn_setup.Image")));
      this._btn_setup.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this._btn_setup.Location = new System.Drawing.Point(73, 51);
      this._btn_setup.Name = "_btn_setup";
      this._btn_setup.Size = new System.Drawing.Size(325, 60);
      this._btn_setup.TabIndex = 5;
      this._btn_setup.Text = "Setup";
      this._btn_setup.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this._btn_setup.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this._btn_setup.UseVisualStyleBackColor = true;
      // 
      // MainSlide
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this._btn_setup);
      this.Controls.Add(this._btn_extrinsic_calibration);
      this.Controls.Add(this._btn_intrinsic_calibration);
      this.Controls.Add(this._btn_example);
      this.Name = "MainSlide";
      this.Size = new System.Drawing.Size(477, 350);
      this.ResumeLayout(false);

    }

    #endregion

    private Parsley.UI.ParsleyButton _btn_example;
    private Parsley.UI.ParsleyButton _btn_intrinsic_calibration;
    private Parsley.UI.ParsleyButton _btn_extrinsic_calibration;
    private Parsley.UI.ParsleyButton _btn_setup;

  }
}
