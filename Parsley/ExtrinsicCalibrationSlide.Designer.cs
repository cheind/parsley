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
      this._btn_load_pattern = new Parsley.UI.ParsleyButtonSmall();
      this._btn_save_extrinsics = new Parsley.UI.ParsleyButtonSmall();
      this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
      this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
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
      this.richTextBox1.Size = new System.Drawing.Size(410, 106);
      this.richTextBox1.TabIndex = 14;
      this.richTextBox1.Text = resources.GetString("richTextBox1.Text");
      // 
      // _btn_load_pattern
      // 
      this._btn_load_pattern.BackColor = System.Drawing.Color.White;
      this._btn_load_pattern.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
      this._btn_load_pattern.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this._btn_load_pattern.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this._btn_load_pattern.ForeColor = System.Drawing.Color.Black;
      this._btn_load_pattern.Image = ((System.Drawing.Image)(resources.GetObject("_btn_load_pattern.Image")));
      this._btn_load_pattern.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this._btn_load_pattern.Location = new System.Drawing.Point(4, 145);
      this._btn_load_pattern.Name = "_btn_load_pattern";
      this._btn_load_pattern.Size = new System.Drawing.Size(117, 27);
      this._btn_load_pattern.TabIndex = 16;
      this._btn_load_pattern.Text = "Load Pattern";
      this._btn_load_pattern.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this._btn_load_pattern.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this._btn_load_pattern.UseVisualStyleBackColor = true;
      this._btn_load_pattern.Click += new System.EventHandler(this._btn_load_pattern_Click);
      // 
      // _btn_save_extrinsics
      // 
      this._btn_save_extrinsics.BackColor = System.Drawing.Color.White;
      this._btn_save_extrinsics.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
      this._btn_save_extrinsics.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this._btn_save_extrinsics.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this._btn_save_extrinsics.ForeColor = System.Drawing.Color.Black;
      this._btn_save_extrinsics.Image = ((System.Drawing.Image)(resources.GetObject("_btn_save_extrinsics.Image")));
      this._btn_save_extrinsics.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this._btn_save_extrinsics.Location = new System.Drawing.Point(4, 178);
      this._btn_save_extrinsics.Name = "_btn_save_extrinsics";
      this._btn_save_extrinsics.Size = new System.Drawing.Size(117, 27);
      this._btn_save_extrinsics.TabIndex = 17;
      this._btn_save_extrinsics.Text = "Save Extrinsics";
      this._btn_save_extrinsics.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this._btn_save_extrinsics.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this._btn_save_extrinsics.UseVisualStyleBackColor = true;
      this._btn_save_extrinsics.Click += new System.EventHandler(this._btn_save_extrinsics_Click);
      // 
      // openFileDialog1
      // 
      this.openFileDialog1.FileName = "openFileDialog1";
      this.openFileDialog1.Filter = "Pattern Files|*.pattern";
      // 
      // saveFileDialog1
      // 
      this.saveFileDialog1.Filter = "Extrinsic Files|*.ecp";
      // 
      // ExtrinsicCalibrationSlide
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this._btn_save_extrinsics);
      this.Controls.Add(this._btn_load_pattern);
      this.Controls.Add(this.richTextBox1);
      this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.Name = "ExtrinsicCalibrationSlide";
      this.Size = new System.Drawing.Size(414, 407);
      this.Controls.SetChildIndex(this.richTextBox1, 0);
      this.Controls.SetChildIndex(this.growLabel1, 0);
      this.Controls.SetChildIndex(this._btn_load_pattern, 0);
      this.Controls.SetChildIndex(this._btn_save_extrinsics, 0);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.RichTextBox richTextBox1;
    private Parsley.UI.ParsleyButtonSmall _btn_load_pattern;
    private Parsley.UI.ParsleyButtonSmall _btn_save_extrinsics;
    private System.Windows.Forms.OpenFileDialog openFileDialog1;
    private System.Windows.Forms.SaveFileDialog saveFileDialog1;
  }
}
