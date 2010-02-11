using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using log4net;

namespace Parsley {

  /// <summary>
  /// Slide that provides access to the parsley main objects
  /// </summary>
  public partial class ContextSlide : UI.ParsleySlide {
    private Context _c;
    private readonly ILog _logger;

    /// <summary>
    /// Empty constructor for designer
    /// </summary>
    public ContextSlide() {
      InitializeComponent();
      _logger = LogManager.GetLogger(typeof(ContextSlide));
    }

    /// <summary>
    /// Initialize with context
    /// </summary>
    /// <param name="context">context</param>
    public ContextSlide(Context context) {
      InitializeComponent();
      _c = context;
      _logger = LogManager.GetLogger(typeof(ContextSlide));
    }

    /// <summary>
    /// Default logger for slides
    /// </summary>
    public ILog Logger {
      get { return _logger; }
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
