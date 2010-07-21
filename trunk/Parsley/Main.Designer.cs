namespace Parsley {
  partial class Main {
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

    #region Vom Windows Form-Designer generierter Code

    /// <summary>
    /// Erforderliche Methode für die Designerunterstützung.
    /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
    /// </summary>
    private void InitializeComponent() {
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
      this._save_dialog = new System.Windows.Forms.SaveFileDialog();
      this._open_dlg = new System.Windows.Forms.OpenFileDialog();
      this._ts_container = new System.Windows.Forms.ToolStripContainer();
      this.toolStrip1 = new System.Windows.Forms.ToolStrip();
      this.toolStrip2 = new System.Windows.Forms.ToolStrip();
      this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
      this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this._status_strip = new System.Windows.Forms.StatusStrip();
      this._btn_back = new System.Windows.Forms.ToolStripButton();
      this._btn_load_configuration = new System.Windows.Forms.ToolStripButton();
      this._btn_save_configuration = new System.Windows.Forms.ToolStripButton();
      this.x = new System.Windows.Forms.ToolStripDropDownButton();
      this._btn_intrinsic_calibration = new System.Windows.Forms.ToolStripMenuItem();
      this._btn_extrinsic_calibration = new System.Windows.Forms.ToolStripMenuItem();
      this._btn_laser_configuration = new System.Windows.Forms.ToolStripMenuItem();
      this._btn_image_algorithm_test = new System.Windows.Forms.ToolStripMenuItem();
      this._btn_scanning = new System.Windows.Forms.ToolStripButton();
      this._btn_show_camera_live_feed = new System.Windows.Forms.ToolStripButton();
      this._btn_show_3d_visualization = new System.Windows.Forms.ToolStripButton();
      this._status_label = new System.Windows.Forms.ToolStripStatusLabel();
      this._btn_options = new System.Windows.Forms.ToolStripMenuItem();
      this._slide_control = new SlickInterface.SlideControl();
      this._ts_container.ContentPanel.SuspendLayout();
      this._ts_container.RightToolStripPanel.SuspendLayout();
      this._ts_container.TopToolStripPanel.SuspendLayout();
      this._ts_container.SuspendLayout();
      this.toolStrip2.SuspendLayout();
      this._status_strip.SuspendLayout();
      this.SuspendLayout();
      // 
      // _save_dialog
      // 
      this._save_dialog.FileName = "Parsley.cfg";
      // 
      // _open_dlg
      // 
      this._open_dlg.FileName = "Parsley.cfg";
      // 
      // _ts_container
      // 
      this._ts_container.BottomToolStripPanelVisible = false;
      // 
      // _ts_container.ContentPanel
      // 
      this._ts_container.ContentPanel.BackColor = System.Drawing.Color.White;
      this._ts_container.ContentPanel.Controls.Add(this._slide_control);
      this._ts_container.ContentPanel.Padding = new System.Windows.Forms.Padding(10, 10, 0, 10);
      this._ts_container.ContentPanel.Size = new System.Drawing.Size(513, 373);
      this._ts_container.Dock = System.Windows.Forms.DockStyle.Fill;
      this._ts_container.LeftToolStripPanelVisible = false;
      this._ts_container.Location = new System.Drawing.Point(0, 0);
      this._ts_container.Name = "_ts_container";
      // 
      // _ts_container.RightToolStripPanel
      // 
      this._ts_container.RightToolStripPanel.Controls.Add(this.toolStrip1);
      this._ts_container.RightToolStripPanelVisible = false;
      this._ts_container.Size = new System.Drawing.Size(513, 411);
      this._ts_container.TabIndex = 3;
      this._ts_container.Text = "toolStripContainer1";
      // 
      // _ts_container.TopToolStripPanel
      // 
      this._ts_container.TopToolStripPanel.BackColor = System.Drawing.Color.White;
      this._ts_container.TopToolStripPanel.Controls.Add(this.toolStrip2);
      this._ts_container.TopToolStripPanel.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
      // 
      // toolStrip1
      // 
      this.toolStrip1.Anchor = System.Windows.Forms.AnchorStyles.None;
      this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
      this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
      this.toolStrip1.Location = new System.Drawing.Point(0, 43);
      this.toolStrip1.Name = "toolStrip1";
      this.toolStrip1.Size = new System.Drawing.Size(26, 102);
      this.toolStrip1.TabIndex = 0;
      // 
      // toolStrip2
      // 
      this.toolStrip2.Dock = System.Windows.Forms.DockStyle.None;
      this.toolStrip2.GripMargin = new System.Windows.Forms.Padding(0);
      this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
      this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._btn_back,
            this._btn_load_configuration,
            this._btn_save_configuration,
            this.toolStripSeparator2,
            this.x,
            this._btn_scanning,
            this.toolStripSeparator1,
            this._btn_show_camera_live_feed,
            this._btn_show_3d_visualization});
      this.toolStrip2.Location = new System.Drawing.Point(0, 0);
      this.toolStrip2.Name = "toolStrip2";
      this.toolStrip2.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
      this.toolStrip2.Size = new System.Drawing.Size(513, 38);
      this.toolStrip2.Stretch = true;
      this.toolStrip2.TabIndex = 5;
      // 
      // toolStripSeparator2
      // 
      this.toolStripSeparator2.Name = "toolStripSeparator2";
      this.toolStripSeparator2.Size = new System.Drawing.Size(6, 38);
      // 
      // toolStripSeparator1
      // 
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new System.Drawing.Size(6, 38);
      // 
      // _status_strip
      // 
      this._status_strip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._status_label});
      this._status_strip.Location = new System.Drawing.Point(0, 411);
      this._status_strip.Name = "_status_strip";
      this._status_strip.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
      this._status_strip.ShowItemToolTips = true;
      this._status_strip.Size = new System.Drawing.Size(513, 22);
      this._status_strip.SizingGrip = false;
      this._status_strip.TabIndex = 4;
      // 
      // _btn_back
      // 
      this._btn_back.Image = global::Parsley.Properties.Resources.previous;
      this._btn_back.ImageTransparentColor = System.Drawing.Color.Magenta;
      this._btn_back.Name = "_btn_back";
      this._btn_back.Size = new System.Drawing.Size(60, 35);
      this._btn_back.Text = "Last Slide";
      this._btn_back.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
      this._btn_back.ToolTipText = "Go back to previous slide";
      this._btn_back.Click += new System.EventHandler(this._btn_back_Click);
      // 
      // _btn_load_configuration
      // 
      this._btn_load_configuration.Image = ((System.Drawing.Image)(resources.GetObject("_btn_load_configuration.Image")));
      this._btn_load_configuration.ImageTransparentColor = System.Drawing.Color.Magenta;
      this._btn_load_configuration.Name = "_btn_load_configuration";
      this._btn_load_configuration.Size = new System.Drawing.Size(40, 35);
      this._btn_load_configuration.Text = "&Open";
      this._btn_load_configuration.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
      this._btn_load_configuration.ToolTipText = "Load Parsley configuration";
      this._btn_load_configuration.Click += new System.EventHandler(this._btn_load_configuration_Click);
      // 
      // _btn_save_configuration
      // 
      this._btn_save_configuration.Image = ((System.Drawing.Image)(resources.GetObject("_btn_save_configuration.Image")));
      this._btn_save_configuration.ImageTransparentColor = System.Drawing.Color.Magenta;
      this._btn_save_configuration.Name = "_btn_save_configuration";
      this._btn_save_configuration.Size = new System.Drawing.Size(35, 35);
      this._btn_save_configuration.Text = "&Save";
      this._btn_save_configuration.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
      this._btn_save_configuration.ToolTipText = "Save current Parsley configuration";
      this._btn_save_configuration.Click += new System.EventHandler(this._btn_save_configuration_Click);
      // 
      // x
      // 
      this.x.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._btn_intrinsic_calibration,
            this._btn_extrinsic_calibration,
            this._btn_laser_configuration,
            this._btn_image_algorithm_test,
            this._btn_options});
      this.x.Image = global::Parsley.Properties.Resources.package_graphics;
      this.x.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.x.Name = "x";
      this.x.ShowDropDownArrow = false;
      this.x.Size = new System.Drawing.Size(41, 35);
      this.x.Text = "Setup";
      this.x.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
      // 
      // _btn_intrinsic_calibration
      // 
      this._btn_intrinsic_calibration.Image = global::Parsley.Properties.Resources.package_games_board;
      this._btn_intrinsic_calibration.Name = "_btn_intrinsic_calibration";
      this._btn_intrinsic_calibration.Size = new System.Drawing.Size(222, 22);
      this._btn_intrinsic_calibration.Text = "Intrinsic Camera Calibration";
      this._btn_intrinsic_calibration.Click += new System.EventHandler(this._btn_intrinsic_calibration_Click);
      // 
      // _btn_extrinsic_calibration
      // 
      this._btn_extrinsic_calibration.Image = global::Parsley.Properties.Resources.aisles;
      this._btn_extrinsic_calibration.Name = "_btn_extrinsic_calibration";
      this._btn_extrinsic_calibration.Size = new System.Drawing.Size(222, 22);
      this._btn_extrinsic_calibration.Text = "Extrinsic Camera Calibration";
      this._btn_extrinsic_calibration.Click += new System.EventHandler(this._btn_extrinsic_calibration_Click);
      // 
      // _btn_laser_configuration
      // 
      this._btn_laser_configuration.Image = global::Parsley.Properties.Resources.highlight;
      this._btn_laser_configuration.Name = "_btn_laser_configuration";
      this._btn_laser_configuration.Size = new System.Drawing.Size(222, 22);
      this._btn_laser_configuration.Text = "Laser Configuration";
      this._btn_laser_configuration.Click += new System.EventHandler(this._btn_laser_configuration_Click);
      // 
      // _btn_image_algorithm_test
      // 
      this._btn_image_algorithm_test.Name = "_btn_image_algorithm_test";
      this._btn_image_algorithm_test.Size = new System.Drawing.Size(222, 22);
      this._btn_image_algorithm_test.Text = "Image Algorithm Test";
      this._btn_image_algorithm_test.Click += new System.EventHandler(this._btn_image_algorithm_test_Click);
      // 
      // _btn_scanning
      // 
      this._btn_scanning.Image = global::Parsley.Properties.Resources.record;
      this._btn_scanning.ImageTransparentColor = System.Drawing.Color.Magenta;
      this._btn_scanning.Name = "_btn_scanning";
      this._btn_scanning.Size = new System.Drawing.Size(48, 35);
      this._btn_scanning.Text = "Record";
      this._btn_scanning.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
      this._btn_scanning.Click += new System.EventHandler(this._btn_scanning_Click);
      // 
      // _btn_show_camera_live_feed
      // 
      this._btn_show_camera_live_feed.CheckOnClick = true;
      this._btn_show_camera_live_feed.Image = global::Parsley.Properties.Resources.xine;
      this._btn_show_camera_live_feed.ImageTransparentColor = System.Drawing.Color.Magenta;
      this._btn_show_camera_live_feed.Margin = new System.Windows.Forms.Padding(0, 1, 3, 2);
      this._btn_show_camera_live_feed.Name = "_btn_show_camera_live_feed";
      this._btn_show_camera_live_feed.Size = new System.Drawing.Size(68, 35);
      this._btn_show_camera_live_feed.Text = "Live Image";
      this._btn_show_camera_live_feed.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
      this._btn_show_camera_live_feed.Click += new System.EventHandler(this._btn_show_camera_live_feed_Click);
      // 
      // _btn_show_3d_visualization
      // 
      this._btn_show_3d_visualization.CheckOnClick = true;
      this._btn_show_3d_visualization.Image = global::Parsley.Properties.Resources._3d;
      this._btn_show_3d_visualization.ImageTransparentColor = System.Drawing.Color.Magenta;
      this._btn_show_3d_visualization.Margin = new System.Windows.Forms.Padding(0, 1, 3, 2);
      this._btn_show_3d_visualization.Name = "_btn_show_3d_visualization";
      this._btn_show_3d_visualization.Size = new System.Drawing.Size(61, 35);
      this._btn_show_3d_visualization.Text = "3D Image";
      this._btn_show_3d_visualization.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
      this._btn_show_3d_visualization.Click += new System.EventHandler(this._btn_show_3d_visualization_Click);
      // 
      // _status_label
      // 
      this._status_label.AutoToolTip = true;
      this._status_label.Image = global::Parsley.Properties.Resources.ok;
      this._status_label.Name = "_status_label";
      this._status_label.Size = new System.Drawing.Size(127, 17);
      this._status_label.Text = "Welcome to Parsley";
      this._status_label.ToolTipText = "Click to show complete log";
      this._status_label.Click += new System.EventHandler(this._status_label_Click);
      // 
      // _btn_options
      // 
      this._btn_options.Name = "_btn_options";
      this._btn_options.Size = new System.Drawing.Size(222, 22);
      this._btn_options.Text = "Options";
      this._btn_options.Click += new System.EventHandler(this._btn_options_Click);
      // 
      // _slide_control
      // 
      this._slide_control.BackColor = System.Drawing.Color.White;
      this._slide_control.Dock = System.Windows.Forms.DockStyle.Fill;
      this._slide_control.Location = new System.Drawing.Point(10, 10);
      this._slide_control.Margin = new System.Windows.Forms.Padding(0);
      this._slide_control.Name = "_slide_control";
      this._slide_control.Selected = null;
      this._slide_control.Size = new System.Drawing.Size(503, 353);
      this._slide_control.SlideSpeed = 0;
      this._slide_control.TabIndex = 1;
      // 
      // Main
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(513, 433);
      this.Controls.Add(this._ts_container);
      this.Controls.Add(this._status_strip);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "Main";
      this.Text = "Welcome to Parsely!";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
      this._ts_container.ContentPanel.ResumeLayout(false);
      this._ts_container.RightToolStripPanel.ResumeLayout(false);
      this._ts_container.RightToolStripPanel.PerformLayout();
      this._ts_container.TopToolStripPanel.ResumeLayout(false);
      this._ts_container.TopToolStripPanel.PerformLayout();
      this._ts_container.ResumeLayout(false);
      this._ts_container.PerformLayout();
      this.toolStrip2.ResumeLayout(false);
      this.toolStrip2.PerformLayout();
      this._status_strip.ResumeLayout(false);
      this._status_strip.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private SlickInterface.SlideControl _slide_control;
    private System.Windows.Forms.SaveFileDialog _save_dialog;
    private System.Windows.Forms.OpenFileDialog _open_dlg;
    private System.Windows.Forms.ToolStripContainer _ts_container;
    private System.Windows.Forms.ToolStrip toolStrip2;
    private System.Windows.Forms.ToolStripButton _btn_back;
    private System.Windows.Forms.ToolStripButton _btn_load_configuration;
    private System.Windows.Forms.ToolStripButton _btn_save_configuration;
    private System.Windows.Forms.ToolStripDropDownButton x;
    private System.Windows.Forms.ToolStripMenuItem _btn_intrinsic_calibration;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    private System.Windows.Forms.ToolStripButton _btn_show_camera_live_feed;
    private System.Windows.Forms.ToolStripButton _btn_show_3d_visualization;
    private System.Windows.Forms.ToolStrip toolStrip1;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    private System.Windows.Forms.StatusStrip _status_strip;
    private System.Windows.Forms.ToolStripStatusLabel _status_label;
    private System.Windows.Forms.ToolStripMenuItem _btn_extrinsic_calibration;
    private System.Windows.Forms.ToolStripMenuItem _btn_laser_configuration;
    private System.Windows.Forms.ToolStripButton _btn_scanning;
    private System.Windows.Forms.ToolStripMenuItem _btn_image_algorithm_test;
    private System.Windows.Forms.ToolStripMenuItem _btn_options;
  }
}

