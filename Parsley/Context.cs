using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;

namespace Parsley {

  /// <summary>
  /// Provides access to Parsley objects
  /// </summary>
  public class Context : INotifyPropertyChanged {
    private Core.BuildingBlocks.Setup _setup;
    private Core.BuildingBlocks.FrameGrabber _fg;
    private Core.BuildingBlocks.RenderLoop _rl;
    private UI.Concrete.EmbeddableStream _es;

    public Context(
      Core.BuildingBlocks.Setup setup,
      Core.BuildingBlocks.FrameGrabber fg,
      Core.BuildingBlocks.RenderLoop rl, 
      UI.Concrete.EmbeddableStream es) 
    {
      _es = es;
      _setup = setup;
      _fg = fg;
      _rl = rl;
    }

    /// <summary>
    /// Get/set the setup
    /// </summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public Core.BuildingBlocks.Setup Setup {
      get { return _setup; }
      set { 
        _setup = value;
        this.OnPropertyChanged("Setup");
      }
    }

    /// <summary>
    /// Used instead of DefaultValueAttribute
    /// </summary>
    public bool ShouldSerializeSetup() {
      return false;
    }

    /// <summary>
    /// Access the frame grabber
    /// </summary>
    [Browsable(false)]
    public Core.BuildingBlocks.FrameGrabber FrameGrabber {
      get { return _fg; }
    }


    /// <summary>
    /// Access the render-loop
    /// </summary>
    [Browsable(false)]
    public Core.BuildingBlocks.RenderLoop RenderLoop {
      get { return _rl; }
    }

    /// <summary>
    /// Access the viewer
    /// </summary>
    [Browsable(false)]
    public Draw3D.Viewer Viewer {
      get { return _rl.Viewer; }
    }

    /// <summary>
    /// Get the embeddable stream
    /// </summary>
    [Browsable(false)]
    public UI.Concrete.EmbeddableStream EmbeddableStream {
      get { return _es; }
    }

    /// <summary>
    /// Triggered when a property changes
    /// </summary>
    public event PropertyChangedEventHandler PropertyChanged;


    /// <summary>
    /// Trigger PropertyChanged event
    /// </summary>
    /// <param name="prop_name"></param>
    protected void OnPropertyChanged(string prop_name) {
      if (PropertyChanged != null) {
        PropertyChanged(this, new PropertyChangedEventArgs(prop_name));
      }
    }
  }
}
