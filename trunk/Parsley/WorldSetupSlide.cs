using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Parsley {
  public partial class WorldSetupSlide : UI.ParsleySlide {
    private Core.BuildingBlocks.World _world;

    public WorldSetupSlide() {
      InitializeComponent();
      _world = new Parsley.Core.BuildingBlocks.World();
      _pg.SelectedObject = _world;
    }
  }
}
