namespace Parsley.Examples {
  partial class ExtractLaserLineSlide {
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExtractLaserLineSlide));
      this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
      this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
      this.growLabel2 = new Parsley.UI.GrowLabel();
      this.growLabel1 = new Parsley.UI.GrowLabel();
      this._num_threshold = new System.Windows.Forms.NumericUpDown();
      this._cmb_channel = new System.Windows.Forms.ComboBox();
      this._btn_take_reference = new Parsley.UI.ParsleyButton();
      this.tableLayoutPanel1.SuspendLayout();
      this.tableLayoutPanel2.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this._num_threshold)).BeginInit();
      this.SuspendLayout();
      // 
      // tableLayoutPanel1
      // 
      this.tableLayoutPanel1.ColumnCount = 1;
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
      this.tableLayoutPanel1.Controls.Add(this._btn_take_reference, 0, 0);
      this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      this.tableLayoutPanel1.RowCount = 2;
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
      this.tableLayoutPanel1.Size = new System.Drawing.Size(509, 457);
      this.tableLayoutPanel1.TabIndex = 0;
      // 
      // tableLayoutPanel2
      // 
      this.tableLayoutPanel2.ColumnCount = 2;
      this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
      this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
      this.tableLayoutPanel2.Controls.Add(this.growLabel2, 0, 1);
      this.tableLayoutPanel2.Controls.Add(this.growLabel1, 0, 0);
      this.tableLayoutPanel2.Controls.Add(this._num_threshold, 1, 0);
      this.tableLayoutPanel2.Controls.Add(this._cmb_channel, 1, 1);
      this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 231);
      this.tableLayoutPanel2.Name = "tableLayoutPanel2";
      this.tableLayoutPanel2.RowCount = 2;
      this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
      this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
      this.tableLayoutPanel2.Size = new System.Drawing.Size(503, 223);
      this.tableLayoutPanel2.TabIndex = 2;
      // 
      // growLabel2
      // 
      this.growLabel2.Anchor = System.Windows.Forms.AnchorStyles.Right;
      this.growLabel2.AutoSize = true;
      this.growLabel2.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.growLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
      this.growLabel2.Location = new System.Drawing.Point(91, 157);
      this.growLabel2.Name = "growLabel2";
      this.growLabel2.Size = new System.Drawing.Size(157, 19);
      this.growLabel2.TabIndex = 4;
      this.growLabel2.Text = "Choose Color Channel";
      // 
      // growLabel1
      // 
      this.growLabel1.Anchor = System.Windows.Forms.AnchorStyles.Right;
      this.growLabel1.AutoSize = true;
      this.growLabel1.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.growLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
      this.growLabel1.Location = new System.Drawing.Point(118, 46);
      this.growLabel1.Name = "growLabel1";
      this.growLabel1.Size = new System.Drawing.Size(130, 19);
      this.growLabel1.TabIndex = 2;
      this.growLabel1.Text = "Choose Threshold";
      // 
      // _num_threshold
      // 
      this._num_threshold.Anchor = System.Windows.Forms.AnchorStyles.Left;
      this._num_threshold.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this._num_threshold.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
      this._num_threshold.Location = new System.Drawing.Point(254, 42);
      this._num_threshold.Name = "_num_threshold";
      this._num_threshold.Size = new System.Drawing.Size(47, 27);
      this._num_threshold.TabIndex = 3;
      this._num_threshold.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
      this._num_threshold.ValueChanged += new System.EventHandler(this._num_threshold_ValueChanged_1);
      // 
      // _cmb_channel
      // 
      this._cmb_channel.Anchor = System.Windows.Forms.AnchorStyles.Left;
      this._cmb_channel.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this._cmb_channel.FormattingEnabled = true;
      this._cmb_channel.Items.AddRange(new object[] {
            "Blue",
            "Green",
            "Red"});
      this._cmb_channel.Location = new System.Drawing.Point(254, 153);
      this._cmb_channel.Name = "_cmb_channel";
      this._cmb_channel.Size = new System.Drawing.Size(121, 27);
      this._cmb_channel.TabIndex = 5;
      this._cmb_channel.Text = "Select Channel";
      this._cmb_channel.SelectedIndexChanged += new System.EventHandler(this._cmb_channel_SelectedIndexChanged);
      // 
      // _btn_take_reference
      // 
      this._btn_take_reference.Anchor = System.Windows.Forms.AnchorStyles.None;
      this._btn_take_reference.BackColor = System.Drawing.Color.White;
      this._btn_take_reference.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
      this._btn_take_reference.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this._btn_take_reference.Font = new System.Drawing.Font("Calibri", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this._btn_take_reference.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
      this._btn_take_reference.Image = ((System.Drawing.Image)(resources.GetObject("_btn_take_reference.Image")));
      this._btn_take_reference.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this._btn_take_reference.Location = new System.Drawing.Point(79, 84);
      this._btn_take_reference.Name = "_btn_take_reference";
      this._btn_take_reference.Size = new System.Drawing.Size(350, 60);
      this._btn_take_reference.TabIndex = 0;
      this._btn_take_reference.Text = "Take Reference Image";
      this._btn_take_reference.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this._btn_take_reference.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this._btn_take_reference.UseVisualStyleBackColor = true;
      this._btn_take_reference.Click += new System.EventHandler(this._btn_take_reference_Click);
      // 
      // ExtractLaserLineSlide
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.tableLayoutPanel1);
      this.Name = "ExtractLaserLineSlide";
      this.Size = new System.Drawing.Size(509, 457);
      this.tableLayoutPanel1.ResumeLayout(false);
      this.tableLayoutPanel2.ResumeLayout(false);
      this.tableLayoutPanel2.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this._num_threshold)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    private Parsley.UI.ParsleyButton _btn_take_reference;
    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
    private Parsley.UI.GrowLabel growLabel2;
    private Parsley.UI.GrowLabel growLabel1;
    private System.Windows.Forms.NumericUpDown _num_threshold;
    private System.Windows.Forms.ComboBox _cmb_channel;

  }
}
