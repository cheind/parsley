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
      this.menuStrip1 = new System.Windows.Forms.MenuStrip();
      this.windowsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.mnu_3d_viewer = new System.Windows.Forms.ToolStripMenuItem();
      this.cameraToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.mnu_live_feed = new System.Windows.Forms.ToolStripMenuItem();
      this._mnu_show_camera_properties = new System.Windows.Forms.ToolStripMenuItem();
      this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
      this._btn_back = new Parsley.UI.ParsleyButtonSmall();
      this._slide_control = new SlickInterface.SlideControl();
      this.menuStrip1.SuspendLayout();
      this.tableLayoutPanel1.SuspendLayout();
      this.SuspendLayout();
      // 
      // menuStrip1
      // 
      this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.windowsToolStripMenuItem,
            this.cameraToolStripMenuItem});
      this.menuStrip1.Location = new System.Drawing.Point(0, 0);
      this.menuStrip1.Name = "menuStrip1";
      this.menuStrip1.Size = new System.Drawing.Size(549, 24);
      this.menuStrip1.TabIndex = 0;
      this.menuStrip1.Text = "menuStrip1";
      // 
      // windowsToolStripMenuItem
      // 
      this.windowsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnu_3d_viewer});
      this.windowsToolStripMenuItem.Name = "windowsToolStripMenuItem";
      this.windowsToolStripMenuItem.Size = new System.Drawing.Size(68, 20);
      this.windowsToolStripMenuItem.Text = "Windows";
      // 
      // mnu_3d_viewer
      // 
      this.mnu_3d_viewer.CheckOnClick = true;
      this.mnu_3d_viewer.Name = "mnu_3d_viewer";
      this.mnu_3d_viewer.Size = new System.Drawing.Size(158, 22);
      this.mnu_3d_viewer.Text = "Show 3D Viewer";
      this.mnu_3d_viewer.Click += new System.EventHandler(this.mnu_3d_viewer_Click);
      // 
      // cameraToolStripMenuItem
      // 
      this.cameraToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnu_live_feed,
            this._mnu_show_camera_properties});
      this.cameraToolStripMenuItem.Name = "cameraToolStripMenuItem";
      this.cameraToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
      this.cameraToolStripMenuItem.Text = "Camera";
      // 
      // mnu_live_feed
      // 
      this.mnu_live_feed.CheckOnClick = true;
      this.mnu_live_feed.Name = "mnu_live_feed";
      this.mnu_live_feed.Size = new System.Drawing.Size(159, 22);
      this.mnu_live_feed.Text = "Show Live Feed";
      this.mnu_live_feed.Click += new System.EventHandler(this.mnu_live_feed_Click);
      // 
      // _mnu_show_camera_properties
      // 
      this._mnu_show_camera_properties.Name = "_mnu_show_camera_properties";
      this._mnu_show_camera_properties.Size = new System.Drawing.Size(159, 22);
      this._mnu_show_camera_properties.Text = "Show Properties";
      this._mnu_show_camera_properties.Click += new System.EventHandler(this._mnu_show_camera_properties_Click);
      // 
      // tableLayoutPanel1
      // 
      this.tableLayoutPanel1.ColumnCount = 1;
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel1.Controls.Add(this._btn_back, 0, 1);
      this.tableLayoutPanel1.Controls.Add(this._slide_control, 0, 0);
      this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 24);
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      this.tableLayoutPanel1.RowCount = 2;
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
      this.tableLayoutPanel1.Size = new System.Drawing.Size(549, 513);
      this.tableLayoutPanel1.TabIndex = 2;
      // 
      // _btn_back
      // 
      this._btn_back.BackColor = System.Drawing.Color.White;
      this._btn_back.Enabled = false;
      this._btn_back.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
      this._btn_back.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this._btn_back.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this._btn_back.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
      this._btn_back.Image = ((System.Drawing.Image)(resources.GetObject("_btn_back.Image")));
      this._btn_back.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this._btn_back.Location = new System.Drawing.Point(3, 475);
      this._btn_back.Name = "_btn_back";
      this._btn_back.Size = new System.Drawing.Size(99, 35);
      this._btn_back.TabIndex = 0;
      this._btn_back.Text = "Go Back";
      this._btn_back.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this._btn_back.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this._btn_back.UseVisualStyleBackColor = true;
      this._btn_back.Click += new System.EventHandler(this._btn_back_Click);
      // 
      // _slide_control
      // 
      this._slide_control.Dock = System.Windows.Forms.DockStyle.Fill;
      this._slide_control.Location = new System.Drawing.Point(3, 3);
      this._slide_control.Name = "_slide_control";
      this._slide_control.Selected = null;
      this._slide_control.Size = new System.Drawing.Size(543, 466);
      this._slide_control.SlideSpeed = 250;
      this._slide_control.TabIndex = 1;
      // 
      // Main
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(549, 537);
      this.Controls.Add(this.tableLayoutPanel1);
      this.Controls.Add(this.menuStrip1);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MainMenuStrip = this.menuStrip1;
      this.Name = "Main";
      this.Text = "Welcome to Parsely!";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
      this.menuStrip1.ResumeLayout(false);
      this.menuStrip1.PerformLayout();
      this.tableLayoutPanel1.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.MenuStrip menuStrip1;
    private System.Windows.Forms.ToolStripMenuItem windowsToolStripMenuItem;
    private SlickInterface.SlideControl _slide_control;
    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    private Parsley.UI.ParsleyButtonSmall _btn_back;
    private System.Windows.Forms.ToolStripMenuItem mnu_3d_viewer;
    private System.Windows.Forms.ToolStripMenuItem cameraToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem mnu_live_feed;
    private System.Windows.Forms.ToolStripMenuItem _mnu_show_camera_properties;
  }
}

