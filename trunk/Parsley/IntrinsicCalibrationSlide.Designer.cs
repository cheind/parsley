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
      this._cb_auto_take = new System.Windows.Forms.CheckBox();
      this._lbl_info1 = new Parsley.UI.GrowLabel();
      this.growLabel2 = new Parsley.UI.GrowLabel();
      this._btn_calibrate = new Parsley.UI.ParsleyButtonSmall();
      this._btn_take_image = new Parsley.UI.ParsleyButtonSmall();
      this.growLabel3 = new Parsley.UI.GrowLabel();
      this._lbl_info = new Parsley.UI.GrowLabel();
      this.SuspendLayout();
      // 
      // _cb_auto_take
      // 
      this._cb_auto_take.AutoSize = true;
      this._cb_auto_take.Location = new System.Drawing.Point(148, 188);
      this._cb_auto_take.Name = "_cb_auto_take";
      this._cb_auto_take.Size = new System.Drawing.Size(118, 19);
      this._cb_auto_take.TabIndex = 9;
      this._cb_auto_take.Text = "Auto take images";
      this._cb_auto_take.UseVisualStyleBackColor = true;
      this._cb_auto_take.CheckedChanged += new System.EventHandler(this._cb_auto_take_CheckedChanged);
      // 
      // _lbl_info1
      // 
      this._lbl_info1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this._lbl_info1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this._lbl_info1.Location = new System.Drawing.Point(-3, 88);
      this._lbl_info1.Name = "_lbl_info1";
      this._lbl_info1.Size = new System.Drawing.Size(392, 45);
      this._lbl_info1.TabIndex = 4;
      this._lbl_info1.Text = "To start the calibration, show the calibration pattern to the camera using differ" +
          "ent angles and distances. When the pattern is detected, take an image or use the" +
          " auto-take image mechanism.";
      this._lbl_info1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // growLabel2
      // 
      this.growLabel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.growLabel2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.growLabel2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.growLabel2.Location = new System.Drawing.Point(0, 33);
      this.growLabel2.Name = "growLabel2";
      this.growLabel2.Size = new System.Drawing.Size(392, 45);
      this.growLabel2.TabIndex = 13;
      this.growLabel2.Text = "The instrinsic calibration uses a known calibration pattern to calculate camera p" +
          "arameters. The calibration pattern can be adjusted using the configuration prope" +
          "rties on the right.";
      this.growLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // _btn_calibrate
      // 
      this._btn_calibrate.BackColor = System.Drawing.Color.White;
      this._btn_calibrate.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
      this._btn_calibrate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this._btn_calibrate.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this._btn_calibrate.ForeColor = System.Drawing.Color.Black;
      this._btn_calibrate.Image = ((System.Drawing.Image)(resources.GetObject("_btn_calibrate.Image")));
      this._btn_calibrate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this._btn_calibrate.Location = new System.Drawing.Point(0, 249);
      this._btn_calibrate.Name = "_btn_calibrate";
      this._btn_calibrate.Size = new System.Drawing.Size(142, 27);
      this._btn_calibrate.TabIndex = 14;
      this._btn_calibrate.Text = "Calibrate";
      this._btn_calibrate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this._btn_calibrate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this._btn_calibrate.UseVisualStyleBackColor = true;
      this._btn_calibrate.Click += new System.EventHandler(this.x_btn_calibrate_Click);
      // 
      // _btn_take_image
      // 
      this._btn_take_image.BackColor = System.Drawing.Color.White;
      this._btn_take_image.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
      this._btn_take_image.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this._btn_take_image.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this._btn_take_image.ForeColor = System.Drawing.Color.Black;
      this._btn_take_image.Image = ((System.Drawing.Image)(resources.GetObject("_btn_take_image.Image")));
      this._btn_take_image.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this._btn_take_image.Location = new System.Drawing.Point(0, 183);
      this._btn_take_image.Name = "_btn_take_image";
      this._btn_take_image.Size = new System.Drawing.Size(142, 27);
      this._btn_take_image.TabIndex = 15;
      this._btn_take_image.Text = "Take Image";
      this._btn_take_image.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this._btn_take_image.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this._btn_take_image.UseVisualStyleBackColor = true;
      this._btn_take_image.Click += new System.EventHandler(this.x_btn_take_image_Click);
      // 
      // growLabel3
      // 
      this.growLabel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.growLabel3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.growLabel3.Location = new System.Drawing.Point(-3, 231);
      this.growLabel3.Name = "growLabel3";
      this.growLabel3.Size = new System.Drawing.Size(392, 15);
      this.growLabel3.TabIndex = 16;
      this.growLabel3.Text = "Perform the calibration, once you have acquired enough images.";
      this.growLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // _lbl_info
      // 
      this._lbl_info.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this._lbl_info.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this._lbl_info.Location = new System.Drawing.Point(-3, 144);
      this._lbl_info.Name = "_lbl_info";
      this._lbl_info.Size = new System.Drawing.Size(392, 15);
      this._lbl_info.TabIndex = 17;
      this._lbl_info.Text = "Start by taking images.";
      this._lbl_info.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // IntrinsicCalibrationSlide
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this._lbl_info);
      this.Controls.Add(this.growLabel3);
      this.Controls.Add(this._btn_take_image);
      this.Controls.Add(this._btn_calibrate);
      this.Controls.Add(this.growLabel2);
      this.Controls.Add(this._lbl_info1);
      this.Controls.Add(this._cb_auto_take);
      this.Name = "IntrinsicCalibrationSlide";
      this.Size = new System.Drawing.Size(392, 376);
      this.Controls.SetChildIndex(this._cb_auto_take, 0);
      this.Controls.SetChildIndex(this._lbl_info1, 0);
      this.Controls.SetChildIndex(this.growLabel2, 0);
      this.Controls.SetChildIndex(this._btn_calibrate, 0);
      this.Controls.SetChildIndex(this._btn_take_image, 0);
      this.Controls.SetChildIndex(this.growLabel3, 0);
      this.Controls.SetChildIndex(this._lbl_info, 0);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private Parsley.UI.GrowLabel _lbl_info1;
    private System.Windows.Forms.CheckBox _cb_auto_take;
    private Parsley.UI.GrowLabel growLabel2;
    private Parsley.UI.ParsleyButtonSmall _btn_calibrate;
    private Parsley.UI.ParsleyButtonSmall _btn_take_image;
    private Parsley.UI.GrowLabel growLabel3;
    private Parsley.UI.GrowLabel _lbl_info;
  }
}
