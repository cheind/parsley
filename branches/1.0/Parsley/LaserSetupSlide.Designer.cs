namespace Parsley {
  partial class LaserSetupSlide {
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LaserSetupSlide));
      this.richTextBox1 = new System.Windows.Forms.RichTextBox();
      this._btn_save_laser_data = new Parsley.UI.ParsleyButtonSmall();
      this.SuspendLayout();
      // 
      // growLabel1
      // 
      this.growLabel1.Size = new System.Drawing.Size(418, 30);
      this.growLabel1.Text = "Laser Configuration";
      // 
      // richTextBox1
      // 
      this.richTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.richTextBox1.Cursor = System.Windows.Forms.Cursors.Default;
      this.richTextBox1.Location = new System.Drawing.Point(4, 33);
      this.richTextBox1.Name = "richTextBox1";
      this.richTextBox1.Size = new System.Drawing.Size(414, 100);
      this.richTextBox1.TabIndex = 18;
      this.richTextBox1.Text = resources.GetString("richTextBox1.Text");
      // 
      // _btn_save_laser_data
      // 
      this._btn_save_laser_data.BackColor = System.Drawing.Color.White;
      this._btn_save_laser_data.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
      this._btn_save_laser_data.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this._btn_save_laser_data.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this._btn_save_laser_data.ForeColor = System.Drawing.Color.Black;
      this._btn_save_laser_data.Image = ((System.Drawing.Image)(resources.GetObject("_btn_save_laser_data.Image")));
      this._btn_save_laser_data.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this._btn_save_laser_data.Location = new System.Drawing.Point(4, 139);
      this._btn_save_laser_data.Name = "_btn_save_laser_data";
      this._btn_save_laser_data.Size = new System.Drawing.Size(142, 27);
      this._btn_save_laser_data.TabIndex = 19;
      this._btn_save_laser_data.Text = "Save Laser Data";
      this._btn_save_laser_data.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this._btn_save_laser_data.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this._btn_save_laser_data.UseVisualStyleBackColor = true;
      this._btn_save_laser_data.Click += new System.EventHandler(this._btn_save_laser_data_Click);
      // 
      // LaserSetupSlide
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this._btn_save_laser_data);
      this.Controls.Add(this.richTextBox1);
      this.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
      this.Name = "LaserSetupSlide";
      this.Size = new System.Drawing.Size(418, 384);
      this.Controls.SetChildIndex(this.growLabel1, 0);
      this.Controls.SetChildIndex(this.richTextBox1, 0);
      this.Controls.SetChildIndex(this._btn_save_laser_data, 0);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.RichTextBox richTextBox1;
    private Parsley.UI.ParsleyButtonSmall _btn_save_laser_data;
  }
}
