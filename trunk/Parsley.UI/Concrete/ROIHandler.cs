using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Parsley.UI.Concrete {

  /// <summary>
  ///
  /// </summary>
  public class ROIHandler {
    /// <summary>
    /// Defines the ROI dragging state
    /// </summary>
    enum State {
      Idle = 0,
      Dragging
    }

    private State _state;
    private bool _clip_cursor;
    private Control _c;
    private Rectangle _roi, _last;
    private SizeF _scaling;

    public ROIHandler(Control c) {
      c.MouseDown += new System.Windows.Forms.MouseEventHandler(c_MouseDown);
      c.MouseMove += new System.Windows.Forms.MouseEventHandler(c_MouseMove);
      c.MouseUp += new System.Windows.Forms.MouseEventHandler(c_MouseUp);
      _c = c;
      _state = new State();
      _clip_cursor = true;
      _roi = _last = Rectangle.Empty;
      _scaling = new SizeF(1, 1);
    }

    /// <summary>
    /// Delegate for hooking up new selected region of interests
    /// </summary>
    public delegate void OnROIHandler(Rectangle r);
    /// <summary>
    /// An event that clients can use to be notified whenever a new
    /// ROI is selected.
    /// </summary>
    public event OnROIHandler OnROI;

    /// <summary>
    /// True if cursor should be restricted to control region while
    /// dragging ROI, false otherwise
    /// </summary>
    public bool ClipCursor {
      get { return _clip_cursor; }
      set { _clip_cursor = value; }
    }

    /// <summary>
    /// Apply scaling to rectangle.
    /// </summary>
    /// <remarks>Since the client area of the given control might contain a
    /// scaled version of the image, this property is used in the OnROI event
    /// to re-scale the rectangle.</remarks>
    public SizeF Scale {
      get { return _scaling; }
      set { _scaling = value; }
    }

    /// <summary>
    /// Get the current rectangle without scale applied.
    /// </summary>
    public Rectangle Current {
      get { return _roi; }
    }

    /// <summary>
    /// Access the last rectangle with scaling applied.
    /// </summary>
    public Rectangle Last {
      get { return _last; }
    }

    /// <summary>
    /// True if an roi request is currently handled.
    /// </summary>
    public bool IsDragging {
      get { return _state == State.Dragging; }
    }

    void c_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
      if (e.Button == MouseButtons.Left && _state == State.Idle) {
        if (this.ClipCursor) {
          Cursor.Clip = _c.RectangleToScreen(_c.ClientRectangle);
        }
        _roi = new Rectangle(e.Location, new Size(0, 0));
        _state = State.Dragging;
      }
    }

    void c_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e) {
      if (e.Button == MouseButtons.Left && _state == State.Dragging) {
        if (this.ClipCursor) {
          Cursor.Clip = new Rectangle(); // Reset cursor clip
        }
        _state = State.Idle;
        // Correct rectangle to have a positive width and height
        Rectangle final = new Rectangle();
        final.X = _roi.Width > 0 ? _roi.X : _roi.X + _roi.Width;
        final.Y = _roi.Height > 0 ? _roi.Y : _roi.Y + _roi.Height;
        final.Width = Math.Abs(_roi.Width);
        final.Height = Math.Abs(_roi.Height);
        // Apply reverse-scaling
        final.X = (int)Math.Floor(final.X / _scaling.Width);
        final.Y = (int)Math.Floor(final.Y / _scaling.Height);
        final.Width = (int)Math.Floor(final.Width / _scaling.Width);
        final.Height = (int)Math.Floor(final.Height / _scaling.Height);

        _roi = Rectangle.Empty;
        _last = final;

        OnROIHandler copy = OnROI;
        if (copy != null) {
          copy(final);
        }
      }
    }

    void c_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e) {
      if (_state == State.Dragging) {
        _roi.Width = e.X - _roi.X;
        _roi.Height = e.Y - _roi.Y;
      }
    }

  }
}
