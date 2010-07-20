namespace Parsley.UI.Concrete {
  partial class Draw3DViewer {
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
      this._render_target = new System.Windows.Forms.Panel();
      this.SuspendLayout();
      // 
      // _render_target
      // 
      this._render_target.Dock = System.Windows.Forms.DockStyle.Fill;
      this._render_target.Location = new System.Drawing.Point(0, 0);
      this._render_target.Name = "_render_target";
      this._render_target.Size = new System.Drawing.Size(304, 202);
      this._render_target.TabIndex = 0;
      // 
      // Draw3DViewer
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(304, 202);
      this.Controls.Add(this._render_target);
      this.Name = "Draw3DViewer";
      this.Text = "Displaying 3D Scene";
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Panel _render_target;
  }
}