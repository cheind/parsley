namespace Parsley.UI {
  partial class ParsleyMessage {
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ParsleyMessage));
      this._button_ok = new Parsley.UI.ParsleyButton();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this._text = new Parsley.UI.GrowLabel();
      this.groupBox1.SuspendLayout();
      this.SuspendLayout();
      // 
      // _button_ok
      // 
      this._button_ok.BackColor = System.Drawing.Color.White;
      this._button_ok.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
      this._button_ok.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this._button_ok.Font = new System.Drawing.Font("Calibri", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this._button_ok.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
      this._button_ok.Image = ((System.Drawing.Image)(resources.GetObject("_button_ok.Image")));
      this._button_ok.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this._button_ok.Location = new System.Drawing.Point(88, 190);
      this._button_ok.Name = "_button_ok";
      this._button_ok.Size = new System.Drawing.Size(230, 60);
      this._button_ok.TabIndex = 0;
      this._button_ok.Text = "Point Taken!";
      this._button_ok.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this._button_ok.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this._button_ok.UseVisualStyleBackColor = true;
      this._button_ok.Click += new System.EventHandler(this._button_ok_Click);
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this._text);
      this.groupBox1.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.groupBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
      this.groupBox1.Location = new System.Drawing.Point(12, 12);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Padding = new System.Windows.Forms.Padding(10);
      this.groupBox1.Size = new System.Drawing.Size(382, 152);
      this.groupBox1.TabIndex = 1;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Parsley Says";
      // 
      // _text
      // 
      this._text.Dock = System.Windows.Forms.DockStyle.Fill;
      this._text.Location = new System.Drawing.Point(10, 30);
      this._text.Name = "_text";
      this._text.Size = new System.Drawing.Size(362, 38);
      this._text.TabIndex = 1;
      this._text.Text = "adasddasdsd asdad asd d sa dasd sda sd asd a dadadada dasdad asdda";
      // 
      // ParsleyMessage
      // 
      this.AcceptButton = this._button_ok;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.Color.White;
      this.ClientSize = new System.Drawing.Size(406, 262);
      this.Controls.Add(this.groupBox1);
      this.Controls.Add(this._button_ok);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "ParsleyMessage";
      this.Text = "Message";
      this.groupBox1.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private ParsleyButton _button_ok;
    private System.Windows.Forms.GroupBox groupBox1;
    private GrowLabel _text;
  }
}