namespace DisplayCheckerboard3D {
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
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
      this._picture_box = new Emgu.CV.UI.ImageBox();
      this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
      this._ogl = new Tao.Platform.Windows.SimpleOpenGlControl();
      this.statusStrip1 = new System.Windows.Forms.StatusStrip();
      this._status_label = new System.Windows.Forms.ToolStripStatusLabel();
      ((System.ComponentModel.ISupportInitialize)(this._picture_box)).BeginInit();
      this.tableLayoutPanel1.SuspendLayout();
      this.statusStrip1.SuspendLayout();
      this.SuspendLayout();
      // 
      // _picture_box
      // 
      this._picture_box.Dock = System.Windows.Forms.DockStyle.Fill;
      this._picture_box.Location = new System.Drawing.Point(3, 3);
      this._picture_box.Name = "_picture_box";
      this._picture_box.Size = new System.Drawing.Size(382, 292);
      this._picture_box.TabIndex = 2;
      this._picture_box.TabStop = false;
      // 
      // tableLayoutPanel1
      // 
      this.tableLayoutPanel1.ColumnCount = 2;
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
      this.tableLayoutPanel1.Controls.Add(this._picture_box, 0, 0);
      this.tableLayoutPanel1.Controls.Add(this._ogl, 1, 0);
      this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      this.tableLayoutPanel1.RowCount = 1;
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
      this.tableLayoutPanel1.Size = new System.Drawing.Size(777, 298);
      this.tableLayoutPanel1.TabIndex = 3;
      // 
      // _ogl
      // 
      this._ogl.AccumBits = ((byte)(0));
      this._ogl.AutoCheckErrors = false;
      this._ogl.AutoFinish = false;
      this._ogl.AutoMakeCurrent = true;
      this._ogl.AutoSwapBuffers = true;
      this._ogl.BackColor = System.Drawing.Color.Gray;
      this._ogl.ColorBits = ((byte)(32));
      this._ogl.DepthBits = ((byte)(16));
      this._ogl.Dock = System.Windows.Forms.DockStyle.Fill;
      this._ogl.Location = new System.Drawing.Point(391, 3);
      this._ogl.Name = "_ogl";
      this._ogl.Size = new System.Drawing.Size(383, 292);
      this._ogl.StencilBits = ((byte)(0));
      this._ogl.TabIndex = 3;
      // 
      // statusStrip1
      // 
      this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._status_label});
      this.statusStrip1.Location = new System.Drawing.Point(0, 276);
      this.statusStrip1.Name = "statusStrip1";
      this.statusStrip1.Size = new System.Drawing.Size(777, 22);
      this.statusStrip1.TabIndex = 4;
      this.statusStrip1.Text = "statusStrip1";
      // 
      // _status_label
      // 
      this._status_label.Name = "_status_label";
      this._status_label.Size = new System.Drawing.Size(66, 17);
      this._status_label.Text = "Set on start";
      // 
      // Form1
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(777, 298);
      this.Controls.Add(this.statusStrip1);
      this.Controls.Add(this.tableLayoutPanel1);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "Form1";
      this.Text = "DisplayCheckerboard3D";
      ((System.ComponentModel.ISupportInitialize)(this._picture_box)).EndInit();
      this.tableLayoutPanel1.ResumeLayout(false);
      this.statusStrip1.ResumeLayout(false);
      this.statusStrip1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private Emgu.CV.UI.ImageBox _picture_box;
    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    private Tao.Platform.Windows.SimpleOpenGlControl _ogl;
    private System.Windows.Forms.StatusStrip statusStrip1;
    private System.Windows.Forms.ToolStripStatusLabel _status_label;
  }
}

