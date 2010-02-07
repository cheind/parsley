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
    /// Empty constructor for designer
    /// </summary>
    public ContextSlide() {
      InitializeComponent();
    }

    /// <summary>
    /// Initialize with context
    /// </summary>
    /// <param name="context">context</param>
    public ContextSlide(Context context) {
      InitializeComponent();
      _c = context;
      if (_c != null) {
        _c.OnConfigurationLoaded += new EventHandler<EventArgs>(OnConfigurationLoaded);
      }
    }

    /// <summary>
    /// When the configuration was updated
    /// </summary>
    protected virtual void OnConfigurationLoaded(object sender, EventArgs e) {
    }

    /// <summary>
    /// Access the context
    /// </summary>
    public Context Context {
      get { return _c; }
      set { _c = value; }
    }
  }
}
