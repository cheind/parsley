namespace CaptureFromCamera {
  partial class Form1 {
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
      this._button_show = new Parsley.UI.ParsleyButton();
      this.SuspendLayout();
      // 
      // _button_show
      // 
      this._button_show.BackColor = System.Drawing.Color.White;
      this._button_show.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
      this._button_show.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this._button_show.Font = new System.Drawing.Font("Calibri", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this._button_show.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
      this._button_show.Image = ((System.Drawing.Image)(resources.GetObject("_button_show.Image")));
      this._button_show.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this._button_show.Location = new System.Drawing.Point(32, 71);
      this._button_show.Name = "_button_show";
      this._button_show.Size = new System.Drawing.Size(300, 60);
      this._button_show.TabIndex = 0;
      this._button_show.Text = "Show Me!";
      this._button_show.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this._button_show.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this._button_show.UseVisualStyleBackColor = true;
      this._button_show.Click += new System.EventHandler(this._button_show_Click);
      // 
      // Form1
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.AutoSize = true;
      this.ClientSize = new System.Drawing.Size(364, 202);
      this.Controls.Add(this._button_show);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
      this.Name = "Form1";
      this.Text = "Form1";
      this.ResumeLayout(false);

    }

    #endregion

    private Parsley.UI.ParsleyButton _button_show;







  }
}

