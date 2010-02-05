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
      c.OnConfigurationLoaded += new EventHandler<EventArgs>(c_OnConfigurationLoaded);
    }

    void c_OnConfigurationLoaded(object sender, EventArgs e) {
      _pg.SelectedObject = Context.World;
      _pg.Refresh();
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
