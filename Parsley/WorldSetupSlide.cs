using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Parsley {
  public partial class WorldSetupSlide : ContextSlide {
    
    public WorldSetupSlide(Context c)
      : base(c) 
    {
      InitializeComponent();
    }

    private WorldSetupSlide(): base(null) {
      InitializeComponent();
    }

    protected override void OnSlidingIn() {
      _pg.SelectedObject = Context.World;
      base.OnSlidingIn();
    }
  }
}
