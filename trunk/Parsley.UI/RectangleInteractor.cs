using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Parsley.UI {

  /// <summary>
  /// Interactor to select rectangles from control
  /// </summary>
  public class RectangleInteractor : I2DInteractor {
    private Control _target;
    private State _state;
    private bool _clip_cursor;
    private Rectangle _current, _last_complete;
    private Size _unscaled_size;

    /// <summary>
    /// Initialize interactor
    /// </summary>
    public RectangleInteractor() {
      _target = null;
      _state = State.Idle;
      _last_complete = _current = Rectangle.Empty;
      _unscaled_size = Size.Empty;
      _clip_cursor = false;
    }

    /// <summary>
    /// Callback handler for rectangles selected by user
    /// </summary>
    public delegate void OnRectangleHandler(Rectangle r);
    /// <summary>
    /// Raised when user has selected a rectangle
    /// </summary>
    public event OnRectangleHandler OnRectangle;

    /// <summary>
    /// Interact on target
    /// </summary>
    /// <param name="target">Target</param>
    public void InteractOn(Control target) {
      _target = target;
      _target.MouseDown += new MouseEventHandler(_target_MouseDown);
      _target.MouseMove += new MouseEventHandler(_target_MouseMove);
      _target.MouseUp += new MouseEventHandler(_target_MouseUp);
    }

    /// <summary>
    /// Release target interaction
    /// </summary>
    public void ReleaseInteraction() {
      _target.MouseDown -= new MouseEventHandler(_target_MouseDown);
      _target.MouseMove -= new MouseEventHandler(_target_MouseMove);
      _target.MouseUp -= new MouseEventHandler(_target_MouseUp);
    }

    /// <summary>
    /// Get current dragging state
    /// </summary>
    public State DraggingState {
      get { return _state; }
    }

    /// <summary>
    /// Set/Get unscaled size
    /// </summary>
    /// <remarks>This property is useful if the target control displays a possibly scaled version
    /// of an image. By setting this property you can scale the rectangle to unscaled image dimensions.</remarks>
    public Size UnscaledSize {
      get { return _unscaled_size; }
      set { _unscaled_size = value; }
    }   

    /// <summary>
    /// True if cursor should be clipped to client-region of target control
    /// </summary>
    public bool ClipCursorToControl {
      get { return _clip_cursor; }
      set { _clip_cursor = value; }
    }

    /// <summary>
    /// Get last completed rectangle
    /// </summary>
    public Rectangle LastCompleteRectangle {
      get { return _last_complete; }
    }

    /// <summary>
    /// Get current (possibly uncompleted) rectangle
    /// </summary>
    public Rectangle CurrentRectangle {
      get { return this.RectifyAndScaleRectangle(_current); }
    }

    /// <summary>
    /// Modifies rectangle to eliminate negative width/height values and additionally
    /// scales it appropriately.
    /// </summary>
    /// <param name="r">Rectangle</param>
    Rectangle RectifyAndScaleRectangle(Rectangle r) {
      Rectangle final = new Rectangle();
      final.X = r.Width > 0 ? r.X : r.X + r.Width;
      final.Y = r.Height > 0 ? r.Y : r.Y + r.Height;
      final.Width = Math.Abs(r.Width);
      final.Height = Math.Abs(r.Height);
      // Apply reverse-scaling
      if (_unscaled_size != Size.Empty) {
        Rectangle target_rect = _target.ClientRectangle;
        double sx = (double)_unscaled_size.Width / target_rect.Width;
        double sy = (double)_unscaled_size.Height / target_rect.Height;

        final.X = (int)Math.Floor(final.X * sx);
        final.Y = (int)Math.Floor(final.Y * sy);
        final.Width = (int)Math.Floor(final.Width * sx);
        final.Height = (int)Math.Floor(final.Height * sy);
      }
      

      return final;
    }

    /// <summary>
    /// Finish rectangle selection
    /// </summary>
    /// <param name="sender">Sender</param>
    /// <param name="e">Events</param>
    void _target_MouseUp(object sender, MouseEventArgs e) {
      if (e.Button == MouseButtons.Left && _state == State.Dragging) {
        if (this.ClipCursorToControl) {
          Cursor.Clip = Rectangle.Empty;
        }
        _state = State.Idle;
        Rectangle final = this.RectifyAndScaleRectangle(_current);
        _last_complete = final;
        _current = Rectangle.Empty;

        OnRectangleHandler c = OnRectangle; // null while firing event
        if (c != null) { c(final); }
      }
    }

    /// <summary>
    /// Drag rectangle
    /// </summary>
    /// <param name="sender">Sender</param>
    /// <param name="e">Events</param>
    void _target_MouseMove(object sender, MouseEventArgs e) {
      if (_state == State.Dragging) {
        _current.Width = e.X - _current.X;
        _current.Height = e.Y - _current.Y;
      }
    }

    /// <summary>
    /// Start rectangle selection
    /// </summary>
    /// <param name="sender">Sender</param>
    /// <param name="e">Events</param>
    void _target_MouseDown(object sender, MouseEventArgs e) {
      if (e.Button == MouseButtons.Left && _state == State.Idle) {
        if (this.ClipCursorToControl) {
          Cursor.Clip = _target.RectangleToScreen(_target.ClientRectangle);
        }
        _current = new Rectangle(e.Location, new Size(0, 0));
        _state = State.Dragging;
      }
    }

    /// <summary>
    /// Defines the Rectangle dragging state
    /// </summary>
    public enum State {
      Idle = 0,
      Dragging
    }
  }
}
