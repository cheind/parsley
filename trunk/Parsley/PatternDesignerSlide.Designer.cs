namespace Parsley {
  partial class PatternDesignerSlide {
    /// <summary> 
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary> 
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing) {
      if (disposing && (components != null)) {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent() {
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PatternDesignerSlide));
      this._cmb_patterns = new System.Windows.Forms.ComboBox();
      this.label1 = new System.Windows.Forms.Label();
      this._pg = new System.Windows.Forms.PropertyGrid();
      this._btn_save_pattern = new Parsley.UI.ParsleyButtonSmall();
      this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
      this.SuspendLayout();
      // 
      // growLabel1
      // 
      this.growLabel1.Text = "Pattern Designer";
      // 
      // _cmb_patterns
      // 
      this._cmb_patterns.DropDownWidth = 350;
      this._cmb_patterns.FormattingEnabled = true;
      this._cmb_patterns.Location = new System.Drawing.Point(4, 48);
      this._cmb_patterns.Name = "_cmb_patterns";
      this._cmb_patterns.Size = new System.Drawing.Size(245, 23);
      this._cmb_patterns.TabIndex = 14;
      this._cmb_patterns.SelectedIndexChanged += new System.EventHandler(this._cmb_patterns_SelectedIndexChanged);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(3, 30);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(88, 15);
      this.label1.TabIndex = 15;
      this.label1.Text = "Choose Pattern";
      // 
      // _pg
      // 
      this._pg.Location = new System.Drawing.Point(4, 77);
      this._pg.Name = "_pg";
      this._pg.Size = new System.Drawing.Size(391, 248);
      this._pg.TabIndex = 16;
      this._pg.ToolbarVisible = false;
      // 
      // _btn_save_pattern
      // 
      this._btn_save_pattern.BackColor = System.Drawing.Color.White;
      this._btn_save_pattern.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
      this._btn_save_pattern.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this._btn_save_pattern.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this._btn_save_pattern.ForeColor = System.Drawing.Color.Black;
      this._btn_save_pattern.Image = ((System.Drawing.Image)(resources.GetObject("_btn_save_pattern.Image")));
      this._btn_save_pattern.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this._btn_save_pattern.Location = new System.Drawing.Point(4, 331);
      this._btn_save_pattern.Name = "_btn_save_pattern";
      this._btn_save_pattern.Size = new System.Drawing.Size(99, 27);
      this._btn_save_pattern.TabIndex = 17;
      this._btn_save_pattern.Text = "Save Pattern";
      this._btn_save_pattern.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this._btn_save_pattern.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this._btn_save_pattern.UseVisualStyleBackColor = true;
      this._btn_save_pattern.Click += new System.EventHandler(this._btn_save_pattern_Click);
      // 
      // saveFileDialog1
      // 
      this.saveFileDialog1.Filter = "Pattern Files|*.pattern";
      // 
      // PatternDesignerSlide
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.label1);
      this.Controls.Add(this._cmb_patterns);
      this.Controls.Add(this._pg);
      this.Controls.Add(this._btn_save_pattern);
      this.Name = "PatternDesignerSlide";
      this.Size = new System.Drawing.Size(550, 422);
      this.Controls.SetChildIndex(this._btn_save_pattern, 0);
      this.Controls.SetChildIndex(this._pg, 0);
      this.Controls.SetChildIndex(this._cmb_patterns, 0);
      this.Controls.SetChildIndex(this.label1, 0);
      this.Controls.SetChildIndex(this.growLabel1, 0);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.ComboBox _cmb_patterns;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.PropertyGrid _pg;
    private Parsley.UI.ParsleyButtonSmall _btn_save_pattern;
    private System.Windows.Forms.SaveFileDialog saveFileDialog1;
  }
}
