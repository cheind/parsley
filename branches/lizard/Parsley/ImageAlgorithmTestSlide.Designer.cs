﻿namespace Parsley {
  partial class ImageAlgorithmTestSlide {
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
      this._cmb_algorithm = new System.Windows.Forms.ComboBox();
      this.SuspendLayout();
      // 
      // growLabel1
      // 
      this.growLabel1.Text = "Image Algorithm Test";
      // 
      // _cmb_algorithm
      // 
      this._cmb_algorithm.FormattingEnabled = true;
      this._cmb_algorithm.Location = new System.Drawing.Point(4, 56);
      this._cmb_algorithm.Name = "_cmb_algorithm";
      this._cmb_algorithm.Size = new System.Drawing.Size(182, 23);
      this._cmb_algorithm.TabIndex = 14;
      this._cmb_algorithm.SelectedIndexChanged += new System.EventHandler(this._cmb_algorithm_SelectedIndexChanged);
      // 
      // ImageAlgorithmTestSlide
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this._cmb_algorithm);
      this.Name = "ImageAlgorithmTestSlide";
      this.Size = new System.Drawing.Size(550, 429);
      this.Load += new System.EventHandler(this.ImageAlgorithmTestSlide_Load);
      this.Controls.SetChildIndex(this._cmb_algorithm, 0);
      this.Controls.SetChildIndex(this.growLabel1, 0);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.ComboBox _cmb_algorithm;
  }
}
