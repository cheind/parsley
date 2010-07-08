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

// Shortcuts for this file
using SingleChannelImage = Emgu.CV.Image<Emgu.CV.Structure.Gray, byte>;
using RGBImage = Emgu.CV.Image<Emgu.CV.Structure.Bgr, byte>;

namespace Parsley.UI.Concrete {

  public partial class EmbeddableStream : UserControl {
    private Core.BuildingBlocks.FrameGrabber _grabber;
    private Emgu.CV.CvEnum.INTER _interpolation;
    private bool _can_invoke;

    public EmbeddableStream() {
      InitializeComponent();
      _grabber = null;
      _interpolation = Emgu.CV.CvEnum.INTER.CV_INTER_NN;
      _can_invoke = false;
      this.HandleCreated += new EventHandler(EmbeddableStream_HandleCreated);
      this.HandleDestroyed += new EventHandler(EmbeddableStream_HandleDestroyed);
    }

    void EmbeddableStream_HandleDestroyed(object sender, EventArgs e) {
      _can_invoke = false;
    }

    void EmbeddableStream_HandleCreated(object sender, EventArgs e) {
      _can_invoke = true;
    }

    public Emgu.CV.CvEnum.INTER Interpolation {
      get { return _interpolation; }
      set { _interpolation = value; }
    }

    public Emgu.CV.UI.ImageBox.FunctionalModeOption FunctionalMode {
      get { return _picture_box.FunctionalMode; }
      set { _picture_box.FunctionalMode = value; }
    }

    public Core.BuildingBlocks.FrameGrabber FrameGrabber {
      set {
        this.ReleaseGrab(_grabber);
        _grabber = value;
        this.GrabFrom(_grabber);
      }
      get { return _grabber; }
    }

    public PictureBox PictureBox {
      get { return _picture_box; }
    }

    void GrabFrom(Core.BuildingBlocks.FrameGrabber fg) {
      if (fg != null) {
        fg.OnFrame += new Parsley.Core.BuildingBlocks.FrameGrabber.OnFrameHandler(_grabber_OnFrame);
      }
    }

    /// <summary>
    /// Release the frame grabber callback.
    /// </summary>
    /// <remarks>Automatically called from Dispose.</remarks>
    /// <param name="fg"></param>
    void ReleaseGrab(Core.BuildingBlocks.FrameGrabber fg) {
      if (fg != null) {
        fg.OnFrame -= new Parsley.Core.BuildingBlocks.FrameGrabber.OnFrameHandler(_grabber_OnFrame);
      }
    }

    void _grabber_OnFrame(Parsley.Core.BuildingBlocks.FrameGrabber fg, Emgu.CV.Image<Emgu.CV.Structure.Bgr, byte> img) {
      // Note: This method is called from the frame-grabber's thread loop. 
      // The framegrabber holds a Breath on the camera to ensure that the camera object remains
      // alive during this callback. The camera object is a SharedResource, meaning that any
      // invocation to Dispose will block until all Breaths on the camera are released. Suppose
      // The thread that owns the picture box has called dispose and blocks. The callback here is called
      // and invoke is used. Invoke executes the delegate on thread that owns this control, which is the one
      // that is already blocked. This leads to a deadlock. That is why we use BeginInvoke, which executes
      // the delegate on the GUI thread associated with this control.
      if (_can_invoke) {
        Rectangle client = this.ClientRectangle;
        Emgu.CV.Image<Emgu.CV.Structure.Bgr, byte> img_copy = img.Resize(client.Width, client.Height, _interpolation);
        this.BeginInvoke(new MethodInvoker(delegate {
          // Update image
          Emgu.CV.IImage prev = _picture_box.Image;
          _picture_box.Image = img_copy;
          if (prev != null) {
            prev.Dispose();
          }
        }));
      }
    }
  }
}
