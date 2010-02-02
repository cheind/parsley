namespace Parsley {
  partial class SetupSlide {
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SetupSlide));
      this._error_provider = new System.Windows.Forms.ErrorProvider(this.components);
      this.label1 = new System.Windows.Forms.Label();
      this._numeric_device_id = new System.Windows.Forms.NumericUpDown();
      this._btn_load_calibration = new Parsley.UI.ParsleyButtonSmall();
      this._btn_intrinsic_calibration = new Parsley.UI.ParsleyButtonSmall();
      this._btn_extrinsic_calibration = new Parsley.UI.ParsleyButtonSmall();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this._btn_save_calibration = new Parsley.UI.ParsleyButtonSmall();
      this.label4 = new System.Windows.Forms.Label();
      this.label3 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.groupBox2 = new System.Windows.Forms.GroupBox();
      this._open_dlg = new System.Windows.Forms.OpenFileDialog();
      this._save_dialog = new System.Windows.Forms.SaveFileDialog();
      this.groupBox3 = new System.Windows.Forms.GroupBox();
      this._btn_laser_setup = new Parsley.UI.ParsleyButtonSmall();
      ((System.ComponentModel.ISupportInitialize)(this._error_provider)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this._numeric_device_id)).BeginInit();
      this.groupBox1.SuspendLayout();
      this.groupBox2.SuspendLayout();
      this.groupBox3.SuspendLayout();
      this.SuspendLayout();
      // 
      // _error_provider
      // 
      this._error_provider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
      this._error_provider.ContainerControl = this;
      // 
      // label1
      // 
      this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(80, 29);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(171, 25);
      this.label1.TabIndex = 0;
      this.label1.Text = "Select Camera Device ID";
      this.label1.UseCompatibleTextRendering = true;
      // 
      // _numeric_device_id
      // 
      this._numeric_device_id.Anchor = System.Windows.Forms.AnchorStyles.None;
      this._numeric_device_id.Location = new System.Drawing.Point(257, 27);
      this._numeric_device_id.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
      this._numeric_device_id.Name = "_numeric_device_id";
      this._numeric_device_id.Size = new System.Drawing.Size(39, 27);
      this._numeric_device_id.TabIndex = 1;
      this._numeric_device_id.Value = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
      this._numeric_device_id.ValueChanged += new System.EventHandler(this._numeric_device_id_ValueChanged);
      // 
      // _btn_load_calibration
      // 
      this._btn_load_calibration.BackColor = System.Drawing.Color.White;
      this._btn_load_calibration.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
      this._btn_load_calibration.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this._btn_load_calibration.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this._btn_load_calibration.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
      this._btn_load_calibration.Image = ((System.Drawing.Image)(resources.GetObject("_btn_load_calibration.Image")));
      this._btn_load_calibration.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this._btn_load_calibration.Location = new System.Drawing.Point(102, 54);
      this._btn_load_calibration.Name = "_btn_load_calibration";
      this._btn_load_calibration.Size = new System.Drawing.Size(190, 35);
      this._btn_load_calibration.TabIndex = 2;
      this._btn_load_calibration.Text = "Load Calibration";
      this._btn_load_calibration.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this._btn_load_calibration.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this._btn_load_calibration.UseVisualStyleBackColor = true;
      this._btn_load_calibration.Click += new System.EventHandler(this._btn_load_calibration_Click);
      // 
      // _btn_intrinsic_calibration
      // 
      this._btn_intrinsic_calibration.BackColor = System.Drawing.Color.White;
      this._btn_intrinsic_calibration.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
      this._btn_intrinsic_calibration.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this._btn_intrinsic_calibration.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this._btn_intrinsic_calibration.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
      this._btn_intrinsic_calibration.Image = ((System.Drawing.Image)(resources.GetObject("_btn_intrinsic_calibration.Image")));
      this._btn_intrinsic_calibration.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this._btn_intrinsic_calibration.Location = new System.Drawing.Point(103, 135);
      this._btn_intrinsic_calibration.Name = "_btn_intrinsic_calibration";
      this._btn_intrinsic_calibration.Size = new System.Drawing.Size(190, 35);
      this._btn_intrinsic_calibration.TabIndex = 3;
      this._btn_intrinsic_calibration.Text = "Intrinsic Calibration";
      this._btn_intrinsic_calibration.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this._btn_intrinsic_calibration.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this._btn_intrinsic_calibration.UseVisualStyleBackColor = true;
      this._btn_intrinsic_calibration.Click += new System.EventHandler(this._btn_intrinsic_calibration_Click);
      // 
      // _btn_extrinsic_calibration
      // 
      this._btn_extrinsic_calibration.BackColor = System.Drawing.Color.White;
      this._btn_extrinsic_calibration.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
      this._btn_extrinsic_calibration.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this._btn_extrinsic_calibration.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this._btn_extrinsic_calibration.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
      this._btn_extrinsic_calibration.Image = ((System.Drawing.Image)(resources.GetObject("_btn_extrinsic_calibration.Image")));
      this._btn_extrinsic_calibration.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this._btn_extrinsic_calibration.Location = new System.Drawing.Point(103, 176);
      this._btn_extrinsic_calibration.Name = "_btn_extrinsic_calibration";
      this._btn_extrinsic_calibration.Size = new System.Drawing.Size(190, 35);
      this._btn_extrinsic_calibration.TabIndex = 4;
      this._btn_extrinsic_calibration.Text = "Extrinsic Calibration";
      this._btn_extrinsic_calibration.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this._btn_extrinsic_calibration.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this._btn_extrinsic_calibration.UseVisualStyleBackColor = true;
      this._btn_extrinsic_calibration.Click += new System.EventHandler(this._btn_extrinsic_calibration_Click);
      // 
      // groupBox1
      // 
      this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
      this.groupBox1.Controls.Add(this._btn_save_calibration);
      this.groupBox1.Controls.Add(this.label4);
      this.groupBox1.Controls.Add(this.label3);
      this.groupBox1.Controls.Add(this._btn_extrinsic_calibration);
      this.groupBox1.Controls.Add(this.label2);
      this.groupBox1.Controls.Add(this._btn_intrinsic_calibration);
      this.groupBox1.Controls.Add(this._btn_load_calibration);
      this.groupBox1.Location = new System.Drawing.Point(101, 143);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(395, 315);
      this.groupBox1.TabIndex = 5;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Camera Calibration";
      // 
      // _btn_save_calibration
      // 
      this._btn_save_calibration.BackColor = System.Drawing.Color.White;
      this._btn_save_calibration.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
      this._btn_save_calibration.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this._btn_save_calibration.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this._btn_save_calibration.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
      this._btn_save_calibration.Image = ((System.Drawing.Image)(resources.GetObject("_btn_save_calibration.Image")));
      this._btn_save_calibration.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this._btn_save_calibration.Location = new System.Drawing.Point(104, 256);
      this._btn_save_calibration.Name = "_btn_save_calibration";
      this._btn_save_calibration.Size = new System.Drawing.Size(190, 35);
      this._btn_save_calibration.TabIndex = 6;
      this._btn_save_calibration.Text = "Save Calibration";
      this._btn_save_calibration.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this._btn_save_calibration.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this._btn_save_calibration.UseVisualStyleBackColor = true;
      this._btn_save_calibration.Click += new System.EventHandler(this._btn_save_calibration_Click);
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(113, 234);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(173, 19);
      this.label4.TabIndex = 5;
      this.label4.Text = "Save current calibration";
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(111, 113);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(175, 19);
      this.label3.TabIndex = 3;
      this.label3.Text = "Create a new calibration";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(66, 32);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(263, 19);
      this.label2.TabIndex = 0;
      this.label2.Text = "Reload a previously saved calibration";
      // 
      // groupBox2
      // 
      this.groupBox2.Anchor = System.Windows.Forms.AnchorStyles.None;
      this.groupBox2.Controls.Add(this.label1);
      this.groupBox2.Controls.Add(this._numeric_device_id);
      this.groupBox2.Location = new System.Drawing.Point(101, 48);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new System.Drawing.Size(395, 75);
      this.groupBox2.TabIndex = 6;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = "Camera Device";
      // 
      // _open_dlg
      // 
      this._open_dlg.FileName = "Calibration.xml";
      // 
      // _save_dialog
      // 
      this._save_dialog.FileName = "Calibration.xml";
      // 
      // groupBox3
      // 
      this.groupBox3.Anchor = System.Windows.Forms.AnchorStyles.None;
      this.groupBox3.Controls.Add(this._btn_laser_setup);
      this.groupBox3.Location = new System.Drawing.Point(101, 464);
      this.groupBox3.Name = "groupBox3";
      this.groupBox3.Size = new System.Drawing.Size(395, 105);
      this.groupBox3.TabIndex = 7;
      this.groupBox3.TabStop = false;
      this.groupBox3.Text = "Laser";
      // 
      // _btn_laser_setup
      // 
      this._btn_laser_setup.BackColor = System.Drawing.Color.White;
      this._btn_laser_setup.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
      this._btn_laser_setup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this._btn_laser_setup.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this._btn_laser_setup.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
      this._btn_laser_setup.Image = ((System.Drawing.Image)(resources.GetObject("_btn_laser_setup.Image")));
      this._btn_laser_setup.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this._btn_laser_setup.Location = new System.Drawing.Point(102, 35);
      this._btn_laser_setup.Name = "_btn_laser_setup";
      this._btn_laser_setup.Size = new System.Drawing.Size(190, 35);
      this._btn_laser_setup.TabIndex = 2;
      this._btn_laser_setup.Text = "Setup Laser";
      this._btn_laser_setup.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this._btn_laser_setup.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this._btn_laser_setup.UseVisualStyleBackColor = true;
      this._btn_laser_setup.Click += new System.EventHandler(this._btn_laser_setup_Click);
      // 
      // SetupSlide
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.groupBox3);
      this.Controls.Add(this.groupBox2);
      this.Controls.Add(this.groupBox1);
      this.Name = "SetupSlide";
      this.Size = new System.Drawing.Size(597, 617);
      ((System.ComponentModel.ISupportInitialize)(this._error_provider)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this._numeric_device_id)).EndInit();
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.groupBox2.ResumeLayout(false);
      this.groupBox2.PerformLayout();
      this.groupBox3.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.ErrorProvider _error_provider;
    private System.Windows.Forms.NumericUpDown _numeric_device_id;
    private System.Windows.Forms.Label label1;
    private Parsley.UI.ParsleyButtonSmall _btn_load_calibration;
    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.Label label3;
    private Parsley.UI.ParsleyButtonSmall _btn_extrinsic_calibration;
    private System.Windows.Forms.Label label2;
    private Parsley.UI.ParsleyButtonSmall _btn_intrinsic_calibration;
    private Parsley.UI.ParsleyButtonSmall _btn_save_calibration;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.OpenFileDialog _open_dlg;
    private System.Windows.Forms.SaveFileDialog _save_dialog;
    private System.Windows.Forms.GroupBox groupBox3;
    private Parsley.UI.ParsleyButtonSmall _btn_laser_setup;
  }
}
