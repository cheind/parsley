using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Parsley {
  public partial class CameraParameterSlide : Parsley.ContextSlide {

    public CameraParameterSlide(Parsley.Context c)
      : base(c) 
    {
      InitializeComponent();
    }

    public CameraParameterSlide() : this(null) {
    }

    protected override void OnSlidingIn() {
      _property_grid.SelectedObject = Context.Camera;
      base.OnSlidingIn();

    }

    private void _property_grid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e) {
      _property_grid.Refresh();
    }
  }
}
