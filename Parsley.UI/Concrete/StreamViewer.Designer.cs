namespace Parsley.UI.Concrete {
  partial class StreamViewer {
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StreamViewer));
      this._display = new Parsley.UI.Concrete.EmbeddableStream();
      this.SuspendLayout();
      // 
      // _display
      // 
      this._display.Dock = System.Windows.Forms.DockStyle.Fill;
      this._display.FrameGrabber = null;
      this._display.FunctionalMode = Emgu.CV.UI.ImageBox.FunctionalModeOption.Minimum;
      this._display.Interpolation = Emgu.CV.CvEnum.INTER.CV_INTER_NN;
      this._display.Location = new System.Drawing.Point(0, 0);
      this._display.Name = "_display";
      this._display.Size = new System.Drawing.Size(304, 202);
      this._display.TabIndex = 0;
      // 
      // StreamViewer
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(304, 202);
      this.Controls.Add(this._display);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "StreamViewer";
      this.Text = "Displaying Live Feed";
      this.ResumeLayout(false);

    }

    #endregion

    private EmbeddableStream _display;
  }
}