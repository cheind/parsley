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
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LaserSetupSlide));
      this._cmb_laser_color = new System.Windows.Forms.ComboBox();
      this.label1 = new System.Windows.Forms.Label();
      this._pg_algorithm_settings = new System.Windows.Forms.PropertyGrid();
      this._btn_filter_noise = new Parsley.UI.ParsleyButtonSmall();
      this._timer_take_image = new System.Windows.Forms.Timer(this.components);
      this.SuspendLayout();
      // 
      // _cmb_laser_color
      // 
      this._cmb_laser_color.Anchor = System.Windows.Forms.AnchorStyles.None;
      this._cmb_laser_color.FormattingEnabled = true;
      this._cmb_laser_color.Items.AddRange(new object[] {
            "Blue",
            "Green",
            "Red"});
      this._cmb_laser_color.Location = new System.Drawing.Point(264, 82);
      this._cmb_laser_color.Name = "_cmb_laser_color";
      this._cmb_laser_color.Size = new System.Drawing.Size(121, 27);
      this._cmb_laser_color.TabIndex = 0;
      this._cmb_laser_color.SelectedIndexChanged += new System.EventHandler(this._cmb_laser_color_SelectedIndexChanged);
      // 
      // label1
      // 
      this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(174, 85);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(84, 19);
      this.label1.TabIndex = 1;
      this.label1.Text = "Laser Color";
      // 
      // _pg_algorithm_settings
      // 
      this._pg_algorithm_settings.Anchor = System.Windows.Forms.AnchorStyles.None;
      this._pg_algorithm_settings.Location = new System.Drawing.Point(94, 124);
      this._pg_algorithm_settings.Name = "_pg_algorithm_settings";
      this._pg_algorithm_settings.Size = new System.Drawing.Size(291, 230);
      this._pg_algorithm_settings.TabIndex = 0;
      this._pg_algorithm_settings.ToolbarVisible = false;
      // 
      // _btn_filter_noise
      // 
      this._btn_filter_noise.Anchor = System.Windows.Forms.AnchorStyles.None;
      this._btn_filter_noise.BackColor = System.Drawing.Color.White;
      this._btn_filter_noise.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
      this._btn_filter_noise.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this._btn_filter_noise.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this._btn_filter_noise.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
      this._btn_filter_noise.Image = ((System.Drawing.Image)(resources.GetObject("_btn_filter_noise.Image")));
      this._btn_filter_noise.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this._btn_filter_noise.Location = new System.Drawing.Point(195, 369);
      this._btn_filter_noise.Name = "_btn_filter_noise";
      this._btn_filter_noise.Size = new System.Drawing.Size(190, 35);
      this._btn_filter_noise.TabIndex = 5;
      this._btn_filter_noise.Text = "Filter Chip Noise";
      this._btn_filter_noise.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this._btn_filter_noise.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this._btn_filter_noise.UseVisualStyleBackColor = true;
      this._btn_filter_noise.Click += new System.EventHandler(this._btn_filter_noise_Click);
      // 
      // _timer_take_image
      // 
      this._timer_take_image.Interval = 3000;
      this._timer_take_image.Tick += new System.EventHandler(this._timer_take_image_Tick);
      // 
      // LaserSetupSlide
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this._pg_algorithm_settings);
      this.Controls.Add(this._btn_filter_noise);
      this.Controls.Add(this.label1);
      this.Controls.Add(this._cmb_laser_color);
      this.Name = "LaserSetupSlide";
      this.Size = new System.Drawing.Size(478, 486);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.ComboBox _cmb_laser_color;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.PropertyGrid _pg_algorithm_settings;
    private Parsley.UI.ParsleyButtonSmall _btn_filter_noise;
    private System.Windows.Forms.Timer _timer_take_image;
  }
}
