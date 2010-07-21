using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Parsley {
  public partial class Settings : Form {

    public Settings(Context c) {
      InitializeComponent();
      _properties.Context = c;
    }

    private Settings() {
      InitializeComponent();
    }

    public PropertyGrid PropertyGrid {
      get { return _properties.PropertyGrid; }
    }
  }
}
