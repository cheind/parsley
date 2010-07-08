namespace Parsley {
  partial class WelcomeSlide {
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WelcomeSlide));
      this.richTextBox1 = new System.Windows.Forms.RichTextBox();
      this.pictureBox1 = new System.Windows.Forms.PictureBox();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
      this.SuspendLayout();
      // 
      // growLabel1
      // 
      this.growLabel1.Text = "Welcome to Parsley";
      // 
      // richTextBox1
      // 
      this.richTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.richTextBox1.Cursor = System.Windows.Forms.Cursors.Default;
      this.richTextBox1.Location = new System.Drawing.Point(4, 33);
      this.richTextBox1.Name = "richTextBox1";
      this.richTextBox1.Size = new System.Drawing.Size(546, 124);
      this.richTextBox1.TabIndex = 14;
      this.richTextBox1.Text = resources.GetString("richTextBox1.Text");
      // 
      // pictureBox1
      // 
      this.pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
      this.pictureBox1.Image = global::Parsley.Properties.Resources.parsley64;
      this.pictureBox1.Location = new System.Drawing.Point(240, 178);
      this.pictureBox1.Name = "pictureBox1";
      this.pictureBox1.Size = new System.Drawing.Size(67, 69);
      this.pictureBox1.TabIndex = 15;
      this.pictureBox1.TabStop = false;
      // 
      // WelcomeSlide
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.pictureBox1);
      this.Controls.Add(this.richTextBox1);
      this.Name = "WelcomeSlide";
      this.Size = new System.Drawing.Size(550, 367);
      this.Controls.SetChildIndex(this.richTextBox1, 0);
      this.Controls.SetChildIndex(this.pictureBox1, 0);
      this.Controls.SetChildIndex(this.growLabel1, 0);
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.RichTextBox richTextBox1;
    private System.Windows.Forms.PictureBox pictureBox1;
  }
}
