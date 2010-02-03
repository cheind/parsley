namespace Parsley.Examples {
  partial class ScanningAttempt {
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScanningAttempt));
      this._nrc_consensus = new System.Windows.Forms.NumericUpDown();
      this._nrc_distance = new System.Windows.Forms.NumericUpDown();
      this._btn_take_ref_image = new Parsley.UI.ParsleyButtonSmall();
      this._btn_restart = new Parsley.UI.ParsleyButtonSmall();
      ((System.ComponentModel.ISupportInitialize)(this._nrc_consensus)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this._nrc_distance)).BeginInit();
      this.SuspendLayout();
      // 
      // _nrc_consensus
      // 
      this._nrc_consensus.DecimalPlaces = 2;
      this._nrc_consensus.Increment = new decimal(new int[] {
            5,
            0,
            0,
            131072});
      this._nrc_consensus.Location = new System.Drawing.Point(89, 55);
      this._nrc_consensus.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this._nrc_consensus.Name = "_nrc_consensus";
      this._nrc_consensus.Size = new System.Drawing.Size(120, 27);
      this._nrc_consensus.TabIndex = 0;
      this._nrc_consensus.Value = new decimal(new int[] {
            3,
            0,
            0,
            65536});
      // 
      // _nrc_distance
      // 
      this._nrc_distance.DecimalPlaces = 2;
      this._nrc_distance.Increment = new decimal(new int[] {
            5,
            0,
            0,
            131072});
      this._nrc_distance.Location = new System.Drawing.Point(89, 88);
      this._nrc_distance.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
      this._nrc_distance.Name = "_nrc_distance";
      this._nrc_distance.Size = new System.Drawing.Size(120, 27);
      this._nrc_distance.TabIndex = 1;
      this._nrc_distance.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
      // 
      // _btn_take_ref_image
      // 
      this._btn_take_ref_image.BackColor = System.Drawing.Color.White;
      this._btn_take_ref_image.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
      this._btn_take_ref_image.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this._btn_take_ref_image.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this._btn_take_ref_image.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
      this._btn_take_ref_image.Image = ((System.Drawing.Image)(resources.GetObject("_btn_take_ref_image.Image")));
      this._btn_take_ref_image.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this._btn_take_ref_image.Location = new System.Drawing.Point(89, 121);
      this._btn_take_ref_image.Name = "_btn_take_ref_image";
      this._btn_take_ref_image.Size = new System.Drawing.Size(190, 35);
      this._btn_take_ref_image.TabIndex = 2;
      this._btn_take_ref_image.Text = "Take reference image";
      this._btn_take_ref_image.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this._btn_take_ref_image.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this._btn_take_ref_image.UseVisualStyleBackColor = true;
      this._btn_take_ref_image.Click += new System.EventHandler(this._btn_take_ref_image_Click);
      // 
      // _btn_restart
      // 
      this._btn_restart.BackColor = System.Drawing.Color.White;
      this._btn_restart.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
      this._btn_restart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this._btn_restart.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this._btn_restart.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
      this._btn_restart.Image = ((System.Drawing.Image)(resources.GetObject("_btn_restart.Image")));
      this._btn_restart.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this._btn_restart.Location = new System.Drawing.Point(89, 162);
      this._btn_restart.Name = "_btn_restart";
      this._btn_restart.Size = new System.Drawing.Size(190, 35);
      this._btn_restart.TabIndex = 3;
      this._btn_restart.Text = "Restart";
      this._btn_restart.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this._btn_restart.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this._btn_restart.UseVisualStyleBackColor = true;
      this._btn_restart.Click += new System.EventHandler(this._btn_restart_Click);
      // 
      // ScanningAttempt
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this._btn_restart);
      this.Controls.Add(this._btn_take_ref_image);
      this.Controls.Add(this._nrc_distance);
      this.Controls.Add(this._nrc_consensus);
      this.Name = "ScanningAttempt";
      this.Size = new System.Drawing.Size(485, 315);
      ((System.ComponentModel.ISupportInitialize)(this._nrc_consensus)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this._nrc_distance)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.NumericUpDown _nrc_consensus;
    private System.Windows.Forms.NumericUpDown _nrc_distance;
    private Parsley.UI.ParsleyButtonSmall _btn_take_ref_image;
    private Parsley.UI.ParsleyButtonSmall _btn_restart;
  }
}
