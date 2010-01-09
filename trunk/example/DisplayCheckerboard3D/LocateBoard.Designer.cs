namespace DisplayCheckerboard3D {
  partial class LocateBoard {
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
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LocateBoard));
      this._picture_box = new Emgu.CV.UI.ImageBox();
      this._render_target = new System.Windows.Forms.Panel();
      this.growLabel2 = new Parsley.UI.GrowLabel();
      this.growLabel1 = new Parsley.UI.GrowLabel();
      this._label_distance = new Parsley.UI.GrowLabel();
      ((System.ComponentModel.ISupportInitialize)(this._picture_box)).BeginInit();
      this.SuspendLayout();
      // 
      // _picture_box
      // 
      this._picture_box.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this._picture_box.Location = new System.Drawing.Point(14, 18);
      this._picture_box.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
      this._picture_box.Name = "_picture_box";
      this._picture_box.Size = new System.Drawing.Size(320, 240);
      this._picture_box.TabIndex = 2;
      this._picture_box.TabStop = false;
      // 
      // _render_target
      // 
      this._render_target.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this._render_target.Location = new System.Drawing.Point(342, 18);
      this._render_target.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
      this._render_target.Name = "_render_target";
      this._render_target.Size = new System.Drawing.Size(320, 240);
      this._render_target.TabIndex = 3;
      // 
      // growLabel2
      // 
      this.growLabel2.AutoSize = true;
      this.growLabel2.Location = new System.Drawing.Point(473, 262);
      this.growLabel2.Name = "growLabel2";
      this.growLabel2.Size = new System.Drawing.Size(89, 19);
      this.growLabel2.TabIndex = 5;
      this.growLabel2.Text = "3D Location";
      // 
      // growLabel1
      // 
      this.growLabel1.AutoSize = true;
      this.growLabel1.Location = new System.Drawing.Point(141, 262);
      this.growLabel1.Name = "growLabel1";
      this.growLabel1.Size = new System.Drawing.Size(72, 19);
      this.growLabel1.TabIndex = 4;
      this.growLabel1.Text = "Live Feed";
      // 
      // _label_distance
      // 
      this._label_distance.AutoSize = true;
      this._label_distance.Location = new System.Drawing.Point(473, 291);
      this._label_distance.Name = "_label_distance";
      this._label_distance.Size = new System.Drawing.Size(66, 19);
      this._label_distance.TabIndex = 6;
      this._label_distance.Text = "distance";
      // 
      // LocateBoard
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.Color.White;
      this.ClientSize = new System.Drawing.Size(677, 336);
      this.Controls.Add(this._label_distance);
      this.Controls.Add(this.growLabel2);
      this.Controls.Add(this.growLabel1);
      this.Controls.Add(this._render_target);
      this.Controls.Add(this._picture_box);
      this.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
      this.Name = "LocateBoard";
      this.Text = "Locate Board in 3D";
      ((System.ComponentModel.ISupportInitialize)(this._picture_box)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private Emgu.CV.UI.ImageBox _picture_box;
    private System.Windows.Forms.Panel _render_target;
    private Parsley.UI.GrowLabel growLabel1;
    private Parsley.UI.GrowLabel growLabel2;
    private Parsley.UI.GrowLabel _label_distance;
  }
}