using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Parsley.UI {
  public partial class Slide : UserControl {
    private SlideControl _control;

    public Slide() {
      InitializeComponent();
    }

    public SlideControl SlideControl {
      get { return _control; }
      set { _control = value; }
    }

    public virtual void OnSlidingIn() {}
    public virtual void OnSlidingOut(CancelEventArgs args) {}
  }
}
