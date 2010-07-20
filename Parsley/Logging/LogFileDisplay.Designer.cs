namespace Parsley.Logging {
  partial class LogFileDisplay {
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LogFileDisplay));
      this._cb_auto_update = new System.Windows.Forms.CheckBox();
      this._txt_content = new System.Windows.Forms.TextBox();
      this.SuspendLayout();
      // 
      // _cb_auto_update
      // 
      this._cb_auto_update.AutoSize = true;
      this._cb_auto_update.Checked = true;
      this._cb_auto_update.CheckState = System.Windows.Forms.CheckState.Checked;
      this._cb_auto_update.Dock = System.Windows.Forms.DockStyle.Top;
      this._cb_auto_update.Location = new System.Drawing.Point(0, 0);
      this._cb_auto_update.Name = "_cb_auto_update";
      this._cb_auto_update.Size = new System.Drawing.Size(779, 17);
      this._cb_auto_update.TabIndex = 0;
      this._cb_auto_update.Text = "Auto Update Content";
      this._cb_auto_update.UseVisualStyleBackColor = true;
      this._cb_auto_update.CheckedChanged += new System.EventHandler(this._cb_auto_update_CheckedChanged);
      // 
      // _txt_content
      // 
      this._txt_content.Dock = System.Windows.Forms.DockStyle.Fill;
      this._txt_content.Location = new System.Drawing.Point(0, 17);
      this._txt_content.Multiline = true;
      this._txt_content.Name = "_txt_content";
      this._txt_content.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this._txt_content.Size = new System.Drawing.Size(779, 273);
      this._txt_content.TabIndex = 1;
      // 
      // LogFileDisplay
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(779, 290);
      this.Controls.Add(this._txt_content);
      this.Controls.Add(this._cb_auto_update);
      this.Font = new System.Drawing.Font("Segoe UI", 8.25F);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
      this.Name = "LogFileDisplay";
      this.Text = "Log File Content";
      this.Shown += new System.EventHandler(this.LogFileDisplay_Shown);
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LogFileDisplay_FormClosing);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.CheckBox _cb_auto_update;
    private System.Windows.Forms.TextBox _txt_content;
  }
}