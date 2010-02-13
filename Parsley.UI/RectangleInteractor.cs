/*
 * Parsley http://parsley.googlecode.com
 * Copyright (c) 2010, Christoph Heindl. All rights reserved.
 * Code license:	New BSD License
 */

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
  [Core.Addins.Addin]
  [InteractionResultType(typeof(Rectangle))]
  public class RectangleInteractor : I2DInteractor {
    private Control _target;
    private InteractionState _state;
    private bool _clip_cursor;
    private Rectangle _current;
    private Size _unscaled_size;

    /// <summary>
    /// Initialize interactor
    /// </summary>
    public RectangleInteractor() {
      _target = null;
      _state = InteractionState.Idle;
      _current = Rectangle.Empty;
      _unscaled_size = Size.Empty;
      _clip_cursor = false;
    }

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
    /// Get/set unscaled image size
    /// </summary>
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
    /// Get current (possibly uncompleted) rectangle
    /// </summary>
    public object Current {
      get { return this.RectifyAndScaleRectangle(_current); }
    }

    /// <summary>
    /// Triggered when rectangle is completed
    /// </summary>
    public event EventHandler<InteractionEventArgs> InteractionCompleted;

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
      if (e.Button == MouseButtons.Left && _state == InteractionState.Interacting) {
        if (this.ClipCursorToControl) {
          Cursor.Clip = Rectangle.Empty;
        }
        _state = InteractionState.Idle;
        Rectangle final = this.RectifyAndScaleRectangle(_current);
        _current = Rectangle.Empty;

        if (InteractionCompleted != null) { 
          InteractionCompleted(this, new InteractionEventArgs(final)); 
        }
      }
    }

    /// <summary>
    /// Drag rectangle
    /// </summary>
    /// <param name="sender">Sender</param>
    /// <param name="e">Events</param>
    void _target_MouseMove(object sender, MouseEventArgs e) {
      if (_state == InteractionState.Interacting) {
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
      if (e.Button == MouseButtons.Left && _state == InteractionState.Idle) {
        if (this.ClipCursorToControl) {
          Cursor.Clip = _target.RectangleToScreen(_target.ClientRectangle);
        }
        _current = new Rectangle(e.Location, new Size(0, 0));
        _state = InteractionState.Interacting;
      }
    }

    /// <summary>
    /// Get interactor state
    /// </summary>
    public InteractionState State {
      get { return _state; }
    }

    /// <summary>
    /// Draw rectangle to image
    /// </summary>
    /// <param name="o">Rectangle</param>
    /// <param name="img">Image</param>
    public void DrawIndicator(object o, Emgu.CV.Image<Emgu.CV.Structure.Bgr, byte> img) {
      img.Draw((Rectangle)o, new Emgu.CV.Structure.Bgr(Color.Green), 1);
    }
  }
}
