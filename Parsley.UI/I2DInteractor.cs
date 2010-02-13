using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Parsley.UI {

  /// <summary>
  /// Passed when an interaction is complete
  /// </summary>
  public class InteractionEventArgs : EventArgs {
    private object _result;

    /// <summary>
    /// Initialize with result
    /// </summary>
    /// <param name="result">Result</param>
    public InteractionEventArgs(object result) {
      _result = result;
    }

    /// <summary>
    /// Access the interaction result
    /// </summary>
    public object InteractionResult {
      get { return _result; }
    }
  }

  /// <summary>
  /// Defines the result type of an interactor.
  /// </summary>
  [AttributeUsage(AttributeTargets.Class)]
  public class InteractionResultTypeAttribute : System.Attribute {
    private Type _result_type;

    /// <summary>
    /// Initialize with result type
    /// </summary>
    /// <param name="t"></param>
    public InteractionResultTypeAttribute(Type t) {
      _result_type = t;
    }

    /// <summary>
    /// Get result type
    /// </summary>
    public Type ResultType {
      get { return _result_type; }
    }
  }

  /// <summary>
  /// Defines the possible interaction states
  /// </summary>
  public enum InteractionState {
    Idle,
    Interacting
  }

  /// <summary>
  /// Interface for a 2D interactor
  /// </summary>
  public interface I2DInteractor {
    /// <summary>
    /// Place interactor on target
    /// </summary>
    /// <param name="target"></param>
    void InteractOn(Control target);

    /// <summary>
    /// Release target interaction
    /// </summary>
    void ReleaseInteraction();

    /// <summary>
    /// Access current interaction state
    /// </summary>
    object Current {
      get;
    }

    /// <summary>
    /// Access the current interaction state
    /// </summary>
    InteractionState State {
      get;
    }

    /// <summary>
    /// Draw interaction result to image
    /// </summary>
    /// <param name="o">Interaction result</param>
    /// <param name="img">Image</param>
    void DrawIndicator(object o, Emgu.CV.Image<Emgu.CV.Structure.Bgr, byte> img);

    /// <summary>
    /// Set/Get unscaled size
    /// </summary>
    /// <remarks>This property is useful if the target control displays a possibly scaled version
    /// of an image. By setting this property you can scale the rectangle to unscaled image dimensions.</remarks>
    Size UnscaledSize {
      get;
      set;
    }

    /// <summary>
    /// True if cursor should be clipped to client-region of target control
    /// </summary>
    bool ClipCursorToControl {
      get;
      set;
    }

    /// <summary>
    /// Triggered when an interaction is has completed
    /// </summary>
    event EventHandler<InteractionEventArgs> InteractionCompleted;
  }
}
