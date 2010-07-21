/*
 * Parsley http://parsley.googlecode.com
 * Copyright (c) 2010, Christoph Heindl. All rights reserved.
 * Code license:	New BSD License
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Emgu.CV.Structure;

namespace Parsley {
  public partial class PropertyPane : UserControl {
    private Context _context;
    private IEnumerable<Core.Addins.AddinInfo> _interactors;
    private UI.I2DInteractor _current;
    
    public PropertyPane() {
      InitializeComponent();
      _interactors = Core.Addins.AddinStore.FindAddins(
        typeof(UI.I2DInteractor),
        ai => ai.DefaultConstructible && Attribute.IsDefined(ai.Type, typeof(UI.InteractionResultTypeAttribute))
      );
      int count = _interactors.Count();
      _pg_config.SelectedGridItemChanged += new SelectedGridItemChangedEventHandler(_pg_config_SelectedGridItemChanged);
    }

    void _pg_config_SelectedGridItemChanged(object sender, SelectedGridItemChangedEventArgs e) {
      if (_current != null) {
        _current.ReleaseInteraction();
        _current = null;
      }

      Type r = e.NewSelection.PropertyDescriptor.PropertyType;
      Core.Addins.AddinInfo suitable = _interactors.FirstOrDefault(
        ai => ai.Attribute<UI.InteractionResultTypeAttribute>().ResultType == r);
      if (suitable != null) {
        _current = Core.Addins.AddinStore.CreateInstance(suitable) as UI.I2DInteractor;
        _current.InteractionCompleted += new EventHandler<Parsley.UI.InteractionEventArgs>(_current_InteractionCompleted);
        _current.UnscaledSize = _context.Setup.Camera.FrameSize;
        _current.InteractOn(_context.EmbeddableStream.PictureBox);
      }
    }

    void _current_InteractionCompleted(object sender, Parsley.UI.InteractionEventArgs e) {
      GridItem i = _pg_config.SelectedGridItem;
      i.PropertyDescriptor.SetValue(i.Parent.Value, e.InteractionResult);
      _pg_config.Invalidate();
      _pg_config.Refresh();
    }
    
    
    private void PropertyPane_VisibleChanged(object sender, EventArgs e) {
      if (this.Visible) {
        if (_context != null) {
          _context.FrameGrabber.OnFramePrepend += new Parsley.Core.BuildingBlocks.FrameGrabber.OnFrameHandler(FrameGrabber_OnFramePrepend);
        }
      } else {
        if (_context != null) {
          _context.FrameGrabber.OnFramePrepend -= new Parsley.Core.BuildingBlocks.FrameGrabber.OnFrameHandler(FrameGrabber_OnFramePrepend);
        }
      }
    }

    void FrameGrabber_OnFramePrepend(Parsley.Core.BuildingBlocks.FrameGrabber fp, Emgu.CV.Image<Emgu.CV.Structure.Bgr, byte> img) {
      UI.I2DInteractor i = _current;
      if (i != null) {
        if (i.State == Parsley.UI.InteractionState.Interacting) {
          _current.DrawIndicator(i.Current, img);
        } else {
          GridItem e = _pg_config.SelectedGridItem;
          GridItem p = e.Parent;
          if (p != null) {
            _current.DrawIndicator(e.PropertyDescriptor.GetValue(p.Value), img);
          }
        }
      }
    }

    public Context Context {
      set { 
        _context = value;
        _pg_config.SelectedObject = _context;
      }
    }

    public PropertyGrid PropertyGrid {
      get { return _pg_config; }
    }
  }
}
