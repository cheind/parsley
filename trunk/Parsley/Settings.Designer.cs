namespace Parsley {
  partial class Settings {
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

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent() {
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Settings));
      this._properties = new Parsley.PropertyPane();
      this.SuspendLayout();
      // 
      // _properties
      // 
      this._properties.Dock = System.Windows.Forms.DockStyle.Fill;
      this._properties.Location = new System.Drawing.Point(0, 0);
      this._properties.MinimumSize = new System.Drawing.Size(367, 0);
      this._properties.Name = "_properties";
      this._properties.Padding = new System.Windows.Forms.Padding(10);
      this._properties.Size = new System.Drawing.Size(449, 427);
      this._properties.TabIndex = 0;
      // 
      // Settings
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(449, 427);
      this.Controls.Add(this._properties);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "Settings";
      this.Text = "Parsley Options";
      this.ResumeLayout(false);

    }

    #endregion

    private PropertyPane _properties;
  }
}