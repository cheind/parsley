namespace Parsley {
  partial class ExtrinsicCalibrationSlide {
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExtrinsicCalibrationSlide));
      this.richTextBox1 = new System.Windows.Forms.RichTextBox();
      this._btn_clear_extrinsics = new Parsley.UI.ParsleyButtonSmall();
      this.SuspendLayout();
      // 
      // growLabel1
      // 
      this.growLabel1.Size = new System.Drawing.Size(414, 30);
      this.growLabel1.Text = "Extrinsic Camera Calibration";
      // 
      // richTextBox1
      // 
      this.richTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.richTextBox1.Cursor = System.Windows.Forms.Cursors.Default;
      this.richTextBox1.Location = new System.Drawing.Point(4, 33);
      this.richTextBox1.Name = "richTextBox1";
      this.richTextBox1.Size = new System.Drawing.Size(410, 178);
      this.richTextBox1.TabIndex = 14;
      this.richTextBox1.Text = resources.GetString("richTextBox1.Text");
      // 
      // _btn_clear_extrinsics
      // 
      this._btn_clear_extrinsics.BackColor = System.Drawing.Color.White;
      this._btn_clear_extrinsics.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
      this._btn_clear_extrinsics.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this._btn_clear_extrinsics.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this._btn_clear_extrinsics.ForeColor = System.Drawing.Color.Black;
      this._btn_clear_extrinsics.Image = ((System.Drawing.Image)(resources.GetObject("_btn_clear_extrinsics.Image")));
      this._btn_clear_extrinsics.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this._btn_clear_extrinsics.Location = new System.Drawing.Point(0, 217);
      this._btn_clear_extrinsics.Name = "_btn_clear_extrinsics";
      this._btn_clear_extrinsics.Size = new System.Drawing.Size(152, 27);
      this._btn_clear_extrinsics.TabIndex = 15;
      this._btn_clear_extrinsics.Text = "Clear Reference Planes";
      this._btn_clear_extrinsics.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this._btn_clear_extrinsics.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this._btn_clear_extrinsics.UseVisualStyleBackColor = true;
      this._btn_clear_extrinsics.Click += new System.EventHandler(this._btn_clear_extrinsics_Click);
      // 
      // ExtrinsicCalibrationSlide
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.richTextBox1);
      this.Controls.Add(this._btn_clear_extrinsics);
      this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.Name = "ExtrinsicCalibrationSlide";
      this.Size = new System.Drawing.Size(414, 407);
      this.Controls.SetChildIndex(this._btn_clear_extrinsics, 0);
      this.Controls.SetChildIndex(this.richTextBox1, 0);
      this.Controls.SetChildIndex(this.growLabel1, 0);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.RichTextBox richTextBox1;
    private Parsley.UI.ParsleyButtonSmall _btn_clear_extrinsics;
  }
}
