namespace Parsley {
  partial class LaserSetupSlide {
    /// <summary> 
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary> 
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing) {
      if (disposing && (components != null)) {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent() {
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LaserSetupSlide));
      this.parsleyButtonSmall1 = new Parsley.UI.ParsleyButtonSmall();
      this.richTextBox1 = new System.Windows.Forms.RichTextBox();
      this.SuspendLayout();
      // 
      // growLabel1
      // 
      this.growLabel1.Size = new System.Drawing.Size(418, 30);
      this.growLabel1.Text = "Laser Configuration";
      // 
      // parsleyButtonSmall1
      // 
      this.parsleyButtonSmall1.Anchor = System.Windows.Forms.AnchorStyles.None;
      this.parsleyButtonSmall1.BackColor = System.Drawing.Color.White;
      this.parsleyButtonSmall1.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
      this.parsleyButtonSmall1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.parsleyButtonSmall1.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.parsleyButtonSmall1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
      this.parsleyButtonSmall1.Image = ((System.Drawing.Image)(resources.GetObject("parsleyButtonSmall1.Image")));
      this.parsleyButtonSmall1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.parsleyButtonSmall1.Location = new System.Drawing.Point(171, 324);
      this.parsleyButtonSmall1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
      this.parsleyButtonSmall1.Name = "parsleyButtonSmall1";
      this.parsleyButtonSmall1.Size = new System.Drawing.Size(166, 28);
      this.parsleyButtonSmall1.TabIndex = 6;
      this.parsleyButtonSmall1.Text = "Save Laser Data";
      this.parsleyButtonSmall1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.parsleyButtonSmall1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this.parsleyButtonSmall1.UseVisualStyleBackColor = true;
      this.parsleyButtonSmall1.Click += new System.EventHandler(this.parsleyButtonSmall1_Click);
      // 
      // richTextBox1
      // 
      this.richTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.richTextBox1.Cursor = System.Windows.Forms.Cursors.Default;
      this.richTextBox1.Location = new System.Drawing.Point(4, 33);
      this.richTextBox1.Name = "richTextBox1";
      this.richTextBox1.Size = new System.Drawing.Size(414, 171);
      this.richTextBox1.TabIndex = 18;
      this.richTextBox1.Text = resources.GetString("richTextBox1.Text");
      // 
      // LaserSetupSlide
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.richTextBox1);
      this.Controls.Add(this.parsleyButtonSmall1);
      this.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
      this.Name = "LaserSetupSlide";
      this.Size = new System.Drawing.Size(418, 384);
      this.Controls.SetChildIndex(this.parsleyButtonSmall1, 0);
      this.Controls.SetChildIndex(this.growLabel1, 0);
      this.Controls.SetChildIndex(this.richTextBox1, 0);
      this.ResumeLayout(false);

    }

    #endregion

    private Parsley.UI.ParsleyButtonSmall parsleyButtonSmall1;
    private System.Windows.Forms.RichTextBox richTextBox1;
  }
}
