namespace Rendering3D {
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
      this._render_target = new System.Windows.Forms.Panel();
      this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
      this._button_add_capsule = new System.Windows.Forms.Button();
      this.tableLayoutPanel1.SuspendLayout();
      this.SuspendLayout();
      // 
      // _render_target
      // 
      this._render_target.Dock = System.Windows.Forms.DockStyle.Fill;
      this._render_target.Location = new System.Drawing.Point(3, 3);
      this._render_target.Name = "_render_target";
      this._render_target.Size = new System.Drawing.Size(493, 285);
      this._render_target.TabIndex = 0;
      // 
      // tableLayoutPanel1
      // 
      this.tableLayoutPanel1.ColumnCount = 1;
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
      this.tableLayoutPanel1.Controls.Add(this._render_target, 0, 0);
      this.tableLayoutPanel1.Controls.Add(this._button_add_capsule, 0, 1);
      this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      this.tableLayoutPanel1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
      this.tableLayoutPanel1.RowCount = 2;
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90.09288F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.907121F));
      this.tableLayoutPanel1.Size = new System.Drawing.Size(499, 323);
      this.tableLayoutPanel1.TabIndex = 1;
      // 
      // _button_add_capsule
      // 
      this._button_add_capsule.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this._button_add_capsule.Location = new System.Drawing.Point(421, 294);
      this._button_add_capsule.Name = "_button_add_capsule";
      this._button_add_capsule.Size = new System.Drawing.Size(75, 23);
      this._button_add_capsule.TabIndex = 1;
      this._button_add_capsule.Text = "add capsule";
      this._button_add_capsule.UseVisualStyleBackColor = true;
      this._button_add_capsule.Click += new System.EventHandler(this._button_add_capsule_Click);
      // 
      // Form1
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.Color.White;
      this.ClientSize = new System.Drawing.Size(499, 323);
      this.Controls.Add(this.tableLayoutPanel1);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "Form1";
      this.Text = "3D Rendering";
      this.tableLayoutPanel1.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Panel _render_target;
    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    private System.Windows.Forms.Button _button_add_capsule;
  }
}

