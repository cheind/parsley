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
    private Core.FrameGrabber _grabber;
    private bool _is_paused;
    private bool _is_disposing;
    private Emgu.CV.CvEnum.INTER _interpolation;

    public EmbeddableStream() {
      InitializeComponent();
      _is_disposing = false;
      _is_paused = false;
      _grabber = null;
      _interpolation = Emgu.CV.CvEnum.INTER.CV_INTER_NN;
    }

    public Emgu.CV.CvEnum.INTER Interpolation {
      get { return _interpolation; }
      set { _interpolation = value; }
    }

    public Core.FrameGrabber FrameGrabber {
      set {
        this.ReleaseGrab(_grabber);
        _grabber = value;
        this.GrabFrom(_grabber);
      }
      get { return _grabber; }
    }

    public void Pause() {
      _is_paused = true;
    }

    public void Resume() {
      _is_paused = false;
    }

    void GrabFrom(Core.FrameGrabber fg) {
      if (fg != null) {
        fg.OnFrame += new Parsley.Core.FrameGrabber.OnFrameHandler(_grabber_OnFrame);
      }
    }

    /// <summary>
    /// Release the frame grabber callback.
    /// </summary>
    /// <remarks>Automatically called from Dispose.</remarks>
    /// <param name="fg"></param>
    void ReleaseGrab(Core.FrameGrabber fg) {
      if (fg != null) {
        fg.OnFrame -= new Parsley.Core.FrameGrabber.OnFrameHandler(_grabber_OnFrame);
      }
    }

    Emgu.CV.IImage _grabber_OnFrame(Parsley.Core.FrameGrabber fg, Emgu.CV.IImage img) {
      // Note: This method is called from the frame-grabber's thread loop. 
      // The framegrabber holds a Breath on the camera to ensure that the camera object remains
      // alive during this callback. The camera object is a SharedResource, meaning that any
      // invocation to Dispose will block until all Breaths on the camera are released. Suppose
      // The thread that owns the picture box has called dispose and blocks. The callback here is called
      // and invoke is used. Invoke executes the delegate on thread that owns this control, which is the one
      // that is already blocked. This leads to a deadlock. That is why we use BeginInvoke, which executes
      // the delegate on the GUI thread associated with this control.
      Emgu.CV.IImage img_copy = this.ResizeImage(img, _picture_box.ClientRectangle.Width, _picture_box.ClientRectangle.Height);
      if (this.InvokeRequired) {
        this.BeginInvoke(new MethodInvoker(delegate {
          Emgu.CV.IImage prev = _picture_box.Image;
          _picture_box.Image = img_copy;
          if (prev != null) {
            prev.Dispose();
          }
        }));
      }
      return img;
    }

    Emgu.CV.IImage ResizeImage(Emgu.CV.IImage img, int w, int h) {
      // Make a guess based on the number of channels
      Emgu.CV.IImage ret;
      
      switch (img.NumberOfChannels) {
        case 1:
          SingleChannelImage casted = img as SingleChannelImage;
          ret = casted.Resize(w, h, _interpolation);          
          break;
        case 3:
          RGBImage rgb = img as RGBImage;
          ret = rgb.Resize(w, h, _interpolation);
          break;
        default:
          throw new ArgumentException("Image type not supported");
      }
      return ret;
    }
  }
}
