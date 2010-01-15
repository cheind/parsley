namespace Parsley {
  partial class IntrinsicCalibrationSlide {
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IntrinsicCalibrationSlide));
      this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
      this._lbl_info = new Parsley.UI.GrowLabel();
      this.parsleyButton1 = new Parsley.UI.ParsleyButton();
      this._btn_take_image = new Parsley.UI.ParsleyButton();
      this.parsleyButton2 = new Parsley.UI.ParsleyButton();
      this.tableLayoutPanel1.SuspendLayout();
      this.SuspendLayout();
      // 
      // tableLayoutPanel1
      // 
      this.tableLayoutPanel1.ColumnCount = 1;
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel1.Controls.Add(this._lbl_info, 0, 2);
      this.tableLayoutPanel1.Controls.Add(this.parsleyButton1, 0, 1);
      this.tableLayoutPanel1.Controls.Add(this._btn_take_image, 0, 0);
      this.tableLayoutPanel1.Controls.Add(this.parsleyButton2, 0, 3);
      this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      this.tableLayoutPanel1.RowCount = 4;
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
      this.tableLayoutPanel1.Size = new System.Drawing.Size(391, 341);
      this.tableLayoutPanel1.TabIndex = 0;
      // 
      // _lbl_info
      // 
      this._lbl_info.Anchor = System.Windows.Forms.AnchorStyles.None;
      this._lbl_info.Location = new System.Drawing.Point(45, 203);
      this._lbl_info.Name = "_lbl_info";
      this._lbl_info.Size = new System.Drawing.Size(300, 19);
      this._lbl_info.TabIndex = 4;
      this._lbl_info.Text = "Start calibration by taking images";
      // 
      // parsleyButton1
      // 
      this.parsleyButton1.Anchor = System.Windows.Forms.AnchorStyles.None;
      this.parsleyButton1.BackColor = System.Drawing.Color.White;
      this.parsleyButton1.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
      this.parsleyButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.parsleyButton1.Font = new System.Drawing.Font("Calibri", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.parsleyButton1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
      this.parsleyButton1.Image = ((System.Drawing.Image)(resources.GetObject("parsleyButton1.Image")));
      this.parsleyButton1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.parsleyButton1.Location = new System.Drawing.Point(45, 97);
      this.parsleyButton1.Name = "parsleyButton1";
      this.parsleyButton1.Size = new System.Drawing.Size(300, 60);
      this.parsleyButton1.TabIndex = 1;
      this.parsleyButton1.Text = "Take Auto Images";
      this.parsleyButton1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.parsleyButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.parsleyButton1.UseVisualStyleBackColor = true;
      // 
      // _btn_take_image
      // 
      this._btn_take_image.Anchor = System.Windows.Forms.AnchorStyles.None;
      this._btn_take_image.BackColor = System.Drawing.Color.White;
      this._btn_take_image.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
      this._btn_take_image.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this._btn_take_image.Font = new System.Drawing.Font("Calibri", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this._btn_take_image.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
      this._btn_take_image.Image = ((System.Drawing.Image)(resources.GetObject("_btn_take_image.Image")));
      this._btn_take_image.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this._btn_take_image.Location = new System.Drawing.Point(45, 12);
      this._btn_take_image.Name = "_btn_take_image";
      this._btn_take_image.Size = new System.Drawing.Size(300, 60);
      this._btn_take_image.TabIndex = 0;
      this._btn_take_image.Text = "Take Image";
      this._btn_take_image.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this._btn_take_image.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this._btn_take_image.UseVisualStyleBackColor = true;
      this._btn_take_image.Click += new System.EventHandler(this._btn_take_image_Click);
      // 
      // parsleyButton2
      // 
      this.parsleyButton2.Anchor = System.Windows.Forms.AnchorStyles.None;
      this.parsleyButton2.BackColor = System.Drawing.Color.White;
      this.parsleyButton2.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
      this.parsleyButton2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.parsleyButton2.Font = new System.Drawing.Font("Calibri", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.parsleyButton2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
      this.parsleyButton2.Image = ((System.Drawing.Image)(resources.GetObject("parsleyButton2.Image")));
      this.parsleyButton2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.parsleyButton2.Location = new System.Drawing.Point(45, 268);
      this.parsleyButton2.Name = "parsleyButton2";
      this.parsleyButton2.Size = new System.Drawing.Size(300, 60);
      this.parsleyButton2.TabIndex = 2;
      this.parsleyButton2.Text = "Calibrate";
      this.parsleyButton2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.parsleyButton2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.parsleyButton2.UseVisualStyleBackColor = true;
      // 
      // IntrinsicCalibrationSlide
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.tableLayoutPanel1);
      this.Name = "IntrinsicCalibrationSlide";
      this.Size = new System.Drawing.Size(391, 341);
      this.tableLayoutPanel1.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    private Parsley.UI.ParsleyButton parsleyButton2;
    private Parsley.UI.ParsleyButton parsleyButton1;
    private Parsley.UI.ParsleyButton _btn_take_image;
    private Parsley.UI.GrowLabel _lbl_info;
  }
}
