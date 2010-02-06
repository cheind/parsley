namespace Parsley {
  partial class ConfigurationSlide {
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigurationSlide));
      this._save_dialog = new System.Windows.Forms.SaveFileDialog();
      this._open_dlg = new System.Windows.Forms.OpenFileDialog();
      this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
      this.label1 = new System.Windows.Forms.Label();
      this.parsleyButtonSmall1 = new Parsley.UI.ParsleyButtonSmall();
      this.label2 = new System.Windows.Forms.Label();
      this._btn_save = new Parsley.UI.ParsleyButtonSmall();
      this.label3 = new System.Windows.Forms.Label();
      this.label4 = new System.Windows.Forms.Label();
      this._btn_advanced = new Parsley.UI.ParsleyButtonSmall();
      this.label5 = new System.Windows.Forms.Label();
      this.flowLayoutPanel1.SuspendLayout();
      this.SuspendLayout();
      // 
      // _save_dialog
      // 
      this._save_dialog.FileName = "world_configuration.parsley";
      // 
      // _open_dlg
      // 
      this._open_dlg.FileName = "world_configuration.parsley";
      // 
      // flowLayoutPanel1
      // 
      this.flowLayoutPanel1.Controls.Add(this.label1);
      this.flowLayoutPanel1.Controls.Add(this.parsleyButtonSmall1);
      this.flowLayoutPanel1.Controls.Add(this.label2);
      this.flowLayoutPanel1.Controls.Add(this._btn_save);
      this.flowLayoutPanel1.Controls.Add(this.label3);
      this.flowLayoutPanel1.Controls.Add(this.label4);
      this.flowLayoutPanel1.Controls.Add(this._btn_advanced);
      this.flowLayoutPanel1.Controls.Add(this.label5);
      this.flowLayoutPanel1.Location = new System.Drawing.Point(20, 208);
      this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 20, 3, 3);
      this.flowLayoutPanel1.Name = "flowLayoutPanel1";
      this.flowLayoutPanel1.Size = new System.Drawing.Size(590, 244);
      this.flowLayoutPanel1.TabIndex = 3;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.label1.Location = new System.Drawing.Point(3, 0);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(88, 41);
      this.label1.TabIndex = 0;
      this.label1.Text = "Here you can";
      this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // parsleyButtonSmall1
      // 
      this.parsleyButtonSmall1.BackColor = System.Drawing.Color.White;
      this.parsleyButtonSmall1.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
      this.parsleyButtonSmall1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.parsleyButtonSmall1.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.parsleyButtonSmall1.ForeColor = System.Drawing.Color.Black;
      this.parsleyButtonSmall1.Image = ((System.Drawing.Image)(resources.GetObject("parsleyButtonSmall1.Image")));
      this.parsleyButtonSmall1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.parsleyButtonSmall1.Location = new System.Drawing.Point(97, 3);
      this.parsleyButtonSmall1.Name = "parsleyButtonSmall1";
      this.parsleyButtonSmall1.Size = new System.Drawing.Size(74, 35);
      this.parsleyButtonSmall1.TabIndex = 1;
      this.parsleyButtonSmall1.Text = "Load";
      this.parsleyButtonSmall1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.parsleyButtonSmall1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.parsleyButtonSmall1.UseVisualStyleBackColor = true;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.label2.Location = new System.Drawing.Point(177, 0);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(21, 41);
      this.label2.TabIndex = 2;
      this.label2.Text = "or";
      this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // _btn_save
      // 
      this._btn_save.BackColor = System.Drawing.Color.White;
      this._btn_save.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
      this._btn_save.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this._btn_save.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this._btn_save.ForeColor = System.Drawing.Color.Black;
      this._btn_save.Image = ((System.Drawing.Image)(resources.GetObject("_btn_save.Image")));
      this._btn_save.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this._btn_save.Location = new System.Drawing.Point(204, 3);
      this._btn_save.Name = "_btn_save";
      this._btn_save.Size = new System.Drawing.Size(74, 35);
      this._btn_save.TabIndex = 1;
      this._btn_save.Text = "Save";
      this._btn_save.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this._btn_save.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this._btn_save.UseVisualStyleBackColor = true;
      this._btn_save.Click += new System.EventHandler(this._btn_save_Click);
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
      this.label3.Location = new System.Drawing.Point(284, 0);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(178, 41);
      this.label3.TabIndex = 3;
      this.label3.Text = "your Parsley configurations.";
      this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
      this.label4.Location = new System.Drawing.Point(3, 41);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(136, 41);
      this.label4.TabIndex = 4;
      this.label4.Text = "Additionally you can ";
      this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // _btn_advanced
      // 
      this._btn_advanced.BackColor = System.Drawing.Color.White;
      this._btn_advanced.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
      this._btn_advanced.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this._btn_advanced.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this._btn_advanced.ForeColor = System.Drawing.Color.Black;
      this._btn_advanced.Image = ((System.Drawing.Image)(resources.GetObject("_btn_advanced.Image")));
      this._btn_advanced.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this._btn_advanced.Location = new System.Drawing.Point(145, 44);
      this._btn_advanced.Name = "_btn_advanced";
      this._btn_advanced.Size = new System.Drawing.Size(106, 35);
      this._btn_advanced.TabIndex = 2;
      this._btn_advanced.Text = "Finetune";
      this._btn_advanced.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this._btn_advanced.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this._btn_advanced.UseVisualStyleBackColor = true;
      this._btn_advanced.Click += new System.EventHandler(this._btn_advanced_Click);
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
      this.label5.Location = new System.Drawing.Point(257, 41);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(172, 41);
      this.label5.TabIndex = 5;
      this.label5.Text = "your current configuration.";
      this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // ConfigurationSlide
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.flowLayoutPanel1);
      this.Margin = new System.Windows.Forms.Padding(0);
      this.Name = "ConfigurationSlide";
      this.Padding = new System.Windows.Forms.Padding(20);
      this.Size = new System.Drawing.Size(630, 472);
      this.flowLayoutPanel1.ResumeLayout(false);
      this.flowLayoutPanel1.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.SaveFileDialog _save_dialog;
    private System.Windows.Forms.OpenFileDialog _open_dlg;
    private Parsley.UI.ParsleyButtonSmall _btn_save;
    private Parsley.UI.ParsleyButtonSmall _btn_advanced;
    private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    private System.Windows.Forms.Label label1;
    private Parsley.UI.ParsleyButtonSmall parsleyButtonSmall1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Label label5;

  }
}
