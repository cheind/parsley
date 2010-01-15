using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Parsley.UI {

  public struct AnimationProperties {
    public enum Direction {
      Left = 0x00000002,
      Right = 0x00000001
    };
    public enum Action {
      Activate = 0x20000,
      Hide = 0x10000
    };

    public AnimationProperties(Control c, Direction d, Action a, int msecs) {
      control = c;
      direction = d;
      action = a;
      duration = msecs;
    }

    public int duration;
    public Control control;
    public Direction direction;
    public Action action;
  };

  public partial class SlideControl : Panel {
    private Stack<Slide> _undo;
    private Slide _selected;

    public SlideControl() {
      InitializeComponent();
      _undo = new Stack<Slide>();
      _selected = null;
    }

    public void AddSlide(Slide sc) {
      this.SuspendLayout();
      sc.Dock = DockStyle.Fill;
      sc.Visible = false;
      this.Controls.Add(sc);
      sc.SlideControl = this;
      this.ResumeLayout();
    }

    public Slide Selected {
      get { return _selected; }
      set {
        CancelEventArgs e = new CancelEventArgs();
        if (_selected != null) {
          _selected.OnSlideHiding(e);
        }
        if (!e.Cancel) {
          _selected = value;
          if (_selected != null) {
            _selected.BringToFront();
            _selected.OnSlideShowing();
            _selected.Visible = true;
          }
        }
      }
    }

    public void ForwardTo(string name) {
      this.ForwardTo(this.FindByName(name));
    }

    public void ForwardTo<T>() where T : Slide {
      this.ForwardTo(this.FindByType<T>());
    }

    public void ForwardTo(Slide s) {
      CancelEventArgs e = new CancelEventArgs();
      if (_selected != null) {
        _selected.OnSlideHiding(e);
      }
      if (!e.Cancel) {
        AnimateControl(new AnimationProperties(
          _selected,
          AnimationProperties.Direction.Left,
          AnimationProperties.Action.Hide,
          250
        ));
        _undo.Push(_selected);
        _selected.Visible = false;
        _selected = s;
        _selected.BringToFront();
        _selected.OnSlideShowing();
        _selected.Visible = true;
      }
    }

    public Slide Previous {
      get { return _undo.Peek(); }
    }

    public bool HasPrevious {
      get { return _undo.Count > 0; }
    }

    public void Backward() {
      if (_undo.Count > 0) {
        Slide dest = _undo.Pop();
        this.BackwardTo(dest);
      }
    }

    public void BackwardTo(string name) {
      this.BackwardTo(this.FindByName(name));
    }

    public void BackwardTo<T>() where T : Slide {
      this.BackwardTo(this.FindByType<T>());
    }

    public void BackwardTo(Slide s) {
      CancelEventArgs e = new CancelEventArgs();
      if (_selected != null) {
        _selected.OnSlideHiding(e);
      }
      AnimateControl(new AnimationProperties(
        _selected,
        AnimationProperties.Direction.Left,
        AnimationProperties.Action.Hide,
        250
      ));
      _selected.Visible = false;
      _selected = s;
      _selected.BringToFront();
      _selected.OnSlideShowing();
      _selected.Visible = true;
    }

    public Slide FindByName(string name) {
      Control[] dest = this.Controls.Find(name, false);
      if (dest.Length == 0) {
        throw new ArgumentException(String.Format("No slide with name '{0}' found."), name);
      }
      return dest[0] as Slide;
    }

    public Slide FindByType<T>() where T : Slide {
      IEnumerable<T> dest = this.Controls.OfType<T>();
      return dest.ElementAt(0);
    }

    private static void AnimateControl(AnimationProperties props) {
      const int slide = 0x00040000;
      int ret = AnimateWindow(
        props.control.Handle,
        props.duration,
        slide | (int)props.direction | (int)props.action
      );
    }

    [DllImport("User32.dll")]
    private static extern int AnimateWindow(IntPtr hwnd, int dwTime, int dwFlags);
  }
}
