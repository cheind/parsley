namespace Parsley {
  partial class ScanningSlide {
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScanningSlide));
      this._btn_take_texture_image = new Parsley.UI.ParsleyButtonSmall();
      this._btn_clear_points = new Parsley.UI.ParsleyButtonSmall();
      this._btn_save_points = new Parsley.UI.ParsleyButtonSmall();
      this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
      this.btn_update_transformation = new Parsley.UI.ParsleyButtonSmall();
      this.SuspendLayout();
      // 
      // growLabel1
      // 
      this.growLabel1.Text = "3D Scanning";
      // 
      // _btn_take_texture_image
      // 
      this._btn_take_texture_image.BackColor = System.Drawing.Color.White;
      this._btn_take_texture_image.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
      this._btn_take_texture_image.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this._btn_take_texture_image.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this._btn_take_texture_image.ForeColor = System.Drawing.Color.Black;
      this._btn_take_texture_image.Image = ((System.Drawing.Image)(resources.GetObject("_btn_take_texture_image.Image")));
      this._btn_take_texture_image.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this._btn_take_texture_image.Location = new System.Drawing.Point(4, 148);
      this._btn_take_texture_image.Name = "_btn_take_texture_image";
      this._btn_take_texture_image.Size = new System.Drawing.Size(142, 27);
      this._btn_take_texture_image.TabIndex = 14;
      this._btn_take_texture_image.Text = "Take Texture Image";
      this._btn_take_texture_image.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this._btn_take_texture_image.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this._btn_take_texture_image.UseVisualStyleBackColor = true;
      this._btn_take_texture_image.Click += new System.EventHandler(this._btn_take_texture_image_Click);
      // 
      // _btn_clear_points
      // 
      this._btn_clear_points.BackColor = System.Drawing.Color.White;
      this._btn_clear_points.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
      this._btn_clear_points.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this._btn_clear_points.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this._btn_clear_points.ForeColor = System.Drawing.Color.Black;
      this._btn_clear_points.Image = ((System.Drawing.Image)(resources.GetObject("_btn_clear_points.Image")));
      this._btn_clear_points.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this._btn_clear_points.Location = new System.Drawing.Point(4, 181);
      this._btn_clear_points.Name = "_btn_clear_points";
      this._btn_clear_points.Size = new System.Drawing.Size(142, 27);
      this._btn_clear_points.TabIndex = 15;
      this._btn_clear_points.Text = "Clear Points";
      this._btn_clear_points.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this._btn_clear_points.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this._btn_clear_points.UseVisualStyleBackColor = true;
      this._btn_clear_points.Click += new System.EventHandler(this._btn_clear_points_Click);
      // 
      // _btn_save_points
      // 
      this._btn_save_points.BackColor = System.Drawing.Color.White;
      this._btn_save_points.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
      this._btn_save_points.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this._btn_save_points.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this._btn_save_points.ForeColor = System.Drawing.Color.Black;
      this._btn_save_points.Image = ((System.Drawing.Image)(resources.GetObject("_btn_save_points.Image")));
      this._btn_save_points.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this._btn_save_points.Location = new System.Drawing.Point(4, 214);
      this._btn_save_points.Name = "_btn_save_points";
      this._btn_save_points.Size = new System.Drawing.Size(142, 27);
      this._btn_save_points.TabIndex = 16;
      this._btn_save_points.Text = "Save Points";
      this._btn_save_points.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this._btn_save_points.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this._btn_save_points.UseVisualStyleBackColor = true;
      this._btn_save_points.Click += new System.EventHandler(this._btn_save_points_Click);
      // 
      // saveFileDialog1
      // 
      this.saveFileDialog1.Filter = "CSV files|*.csv";
      this.saveFileDialog1.Title = "Select CSV File Destination";
      this.saveFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog1_FileOk);
      // 
      // btn_update_transformation
      // 
      this.btn_update_transformation.BackColor = System.Drawing.Color.White;
      this.btn_update_transformation.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
      this.btn_update_transformation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btn_update_transformation.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btn_update_transformation.ForeColor = System.Drawing.Color.Black;
      this.btn_update_transformation.Image = ((System.Drawing.Image)(resources.GetObject("btn_update_transformation.Image")));
      this.btn_update_transformation.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.btn_update_transformation.Location = new System.Drawing.Point(4, 102);
      this.btn_update_transformation.Name = "btn_update_transformation";
      this.btn_update_transformation.Size = new System.Drawing.Size(142, 40);
      this.btn_update_transformation.TabIndex = 17;
      this.btn_update_transformation.Text = "Update Positioner Transformation";
      this.btn_update_transformation.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.btn_update_transformation.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.btn_update_transformation.UseVisualStyleBackColor = true;
      this.btn_update_transformation.Click += new System.EventHandler(this.btn_update_transformation_Click);
      // 
      // ScanningSlide
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.btn_update_transformation);
      this.Controls.Add(this._btn_save_points);
      this.Controls.Add(this._btn_clear_points);
      this.Controls.Add(this._btn_take_texture_image);
      this.Name = "ScanningSlide";
      this.Size = new System.Drawing.Size(550, 435);
      this.Controls.SetChildIndex(this._btn_take_texture_image, 0);
      this.Controls.SetChildIndex(this.growLabel1, 0);
      this.Controls.SetChildIndex(this._btn_clear_points, 0);
      this.Controls.SetChildIndex(this._btn_save_points, 0);
      this.Controls.SetChildIndex(this.btn_update_transformation, 0);
      this.ResumeLayout(false);

    }

    #endregion

    private Parsley.UI.ParsleyButtonSmall _btn_take_texture_image;
    private Parsley.UI.ParsleyButtonSmall _btn_clear_points;
    private Parsley.UI.ParsleyButtonSmall _btn_save_points;
    private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    private Parsley.UI.ParsleyButtonSmall btn_update_transformation;
  }
}
