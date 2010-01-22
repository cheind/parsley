namespace Parsley {
  partial class CameraParameterSlide {
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
      this._property_grid = new System.Windows.Forms.PropertyGrid();
      this.SuspendLayout();
      // 
      // _property_grid
      // 
      this._property_grid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this._property_grid.HelpVisible = false;
      this._property_grid.Location = new System.Drawing.Point(25, 19);
      this._property_grid.Name = "_property_grid";
      this._property_grid.PropertySort = System.Windows.Forms.PropertySort.Alphabetical;
      this._property_grid.Size = new System.Drawing.Size(381, 418);
      this._property_grid.TabIndex = 0;
      this._property_grid.ToolbarVisible = false;
      this._property_grid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this._property_grid_PropertyValueChanged);
      // 
      // CameraParameterSlide
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this._property_grid);
      this.Name = "CameraParameterSlide";
      this.Size = new System.Drawing.Size(431, 457);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.PropertyGrid _property_grid;
  }
}
