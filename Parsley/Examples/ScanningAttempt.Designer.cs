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
      this._nrc_threshold = new System.Windows.Forms.NumericUpDown();
      this._nrc_distance = new System.Windows.Forms.NumericUpDown();
      ((System.ComponentModel.ISupportInitialize)(this._nrc_threshold)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this._nrc_distance)).BeginInit();
      this.SuspendLayout();
      // 
      // _nrc_threshold
      // 
      this._nrc_threshold.Location = new System.Drawing.Point(89, 55);
      this._nrc_threshold.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
      this._nrc_threshold.Name = "_nrc_threshold";
      this._nrc_threshold.Size = new System.Drawing.Size(120, 27);
      this._nrc_threshold.TabIndex = 0;
      this._nrc_threshold.Value = new decimal(new int[] {
            120,
            0,
            0,
            0});
      this._nrc_threshold.ValueChanged += new System.EventHandler(this._nrc_threshold_ValueChanged);
      // 
      // _nrc_distance
      // 
      this._nrc_distance.DecimalPlaces = 2;
      this._nrc_distance.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
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
      this._nrc_distance.ValueChanged += new System.EventHandler(this._nrc_distance_ValueChanged);
      // 
      // ScanningAttempt
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this._nrc_distance);
      this.Controls.Add(this._nrc_threshold);
      this.Name = "ScanningAttempt";
      this.Size = new System.Drawing.Size(485, 315);
      ((System.ComponentModel.ISupportInitialize)(this._nrc_threshold)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this._nrc_distance)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.NumericUpDown _nrc_threshold;
    private System.Windows.Forms.NumericUpDown _nrc_distance;
  }
}
