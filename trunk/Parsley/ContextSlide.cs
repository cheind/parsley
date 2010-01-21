using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Parsley {

  /// <summary>
  /// Slide that provides access to the parsley main objects
  /// </summary>
  public partial class ContextSlide : UI.ParsleySlide {
    private Context _c;

    /// <summary>
    /// Initialize with context
    /// </summary>
    /// <param name="context">context</param>
    public ContextSlide(Context context) {
      InitializeComponent();
      _c = context;
    }

    /// <summary>
    /// Access the context
    /// </summary>
    public Context Context {
      get { return _c; }
    }
  }
}
