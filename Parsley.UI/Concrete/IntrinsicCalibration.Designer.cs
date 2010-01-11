namespace Parsley.UI.Concrete {
  partial class IntrinsicCalibration {
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
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IntrinsicCalibration));
      this._picture_box = new Emgu.CV.UI.ImageBox();
      this._button_start = new Parsley.UI.ParsleyButton();
      this.statusStrip1 = new System.Windows.Forms.StatusStrip();
      this._progress_bar = new System.Windows.Forms.ToolStripProgressBar();
      this._progress_label = new System.Windows.Forms.ToolStripStatusLabel();
      ((System.ComponentModel.ISupportInitialize)(this._picture_box)).BeginInit();
      this.statusStrip1.SuspendLayout();
      this.SuspendLayout();
      // 
      // _picture_box
      // 
      this._picture_box.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this._picture_box.Location = new System.Drawing.Point(38, 3);
      this._picture_box.Name = "_picture_box";
      this._picture_box.Size = new System.Drawing.Size(640, 480);
      this._picture_box.TabIndex = 11;
      this._picture_box.TabStop = false;
      // 
      // _button_start
      // 
      this._button_start.BackColor = System.Drawing.Color.White;
      this._button_start.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
      this._button_start.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this._button_start.Font = new System.Drawing.Font("Calibri", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this._button_start.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
      this._button_start.Image = ((System.Drawing.Image)(resources.GetObject("_button_start.Image")));
      this._button_start.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this._button_start.Location = new System.Drawing.Point(378, 489);
      this._button_start.Name = "_button_start";
      this._button_start.Size = new System.Drawing.Size(300, 60);
      this._button_start.TabIndex = 12;
      this._button_start.Text = "Start Calibration";
      this._button_start.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this._button_start.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this._button_start.UseVisualStyleBackColor = true;
      this._button_start.Click += new System.EventHandler(this._button_start_Click);
      // 
      // statusStrip1
      // 
      this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._progress_bar,
            this._progress_label});
      this.statusStrip1.Location = new System.Drawing.Point(0, 559);
      this.statusStrip1.Name = "statusStrip1";
      this.statusStrip1.Size = new System.Drawing.Size(716, 22);
      this.statusStrip1.TabIndex = 13;
      this.statusStrip1.Text = "statusStrip1";
      // 
      // _progress_bar
      // 
      this._progress_bar.Name = "_progress_bar";
      this._progress_bar.Size = new System.Drawing.Size(100, 16);
      // 
      // _progress_label
      // 
      this._progress_label.Name = "_progress_label";
      this._progress_label.Size = new System.Drawing.Size(106, 17);
      this._progress_label.Text = "0 Images Captured";
      // 
      // IntrinsicCalibration
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.statusStrip1);
      this.Controls.Add(this._button_start);
      this.Controls.Add(this._picture_box);
      this.Name = "IntrinsicCalibration";
      this.Size = new System.Drawing.Size(716, 581);
      ((System.ComponentModel.ISupportInitialize)(this._picture_box)).EndInit();
      this.statusStrip1.ResumeLayout(false);
      this.statusStrip1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private Emgu.CV.UI.ImageBox _picture_box;
    private ParsleyButton _button_start;
    private System.Windows.Forms.StatusStrip statusStrip1;
    private System.Windows.Forms.ToolStripProgressBar _progress_bar;
    private System.Windows.Forms.ToolStripStatusLabel _progress_label;
  }
}
