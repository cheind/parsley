/*
 * Parsley http://parsley.googlecode.com
 * Copyright (c) 2010, Christoph Heindl. All rights reserved.
 * Code license:	New BSD License
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Parsley.UI.Concrete {
  public partial class StreamViewer : UI.AspectRatioForm {

    public StreamViewer() {
      InitializeComponent();
      this.IsMaintainingAspectRatio = false;
    }

    public Parsley.Core.BuildingBlocks.FrameGrabber FrameGrabber {
      get { return _display.FrameGrabber; }
      set { 
        _display.FrameGrabber = value;
        if (_display.FrameGrabber != null &&
            _display.FrameGrabber.Camera != null &&
            _display.FrameGrabber.Camera.IsConnected) 
        {
          this.AspectRatio = _display.FrameGrabber.Camera.FrameAspectRatio;
          this.IsMaintainingAspectRatio = true;
        } else {
          this.IsMaintainingAspectRatio = false;
        }
      }
    }

    public EmbeddableStream EmbeddableStream {
      get { return _display; }
    }

    public Emgu.CV.CvEnum.INTER Interpolation {
      get { return _display.Interpolation; }
      set { _display.Interpolation = value; }
    }

    public Emgu.CV.UI.ImageBox.FunctionalModeOption FunctionalMode {
      get { return _display.FunctionalMode; }
      set { _display.FunctionalMode = value; }
    }
  }
}
