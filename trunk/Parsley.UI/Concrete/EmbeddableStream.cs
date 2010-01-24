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
    private Emgu.CV.CvEnum.INTER _interpolation;
    private bool _can_invoke;
    private ROIHandler _roi_handler;

    public EmbeddableStream() {
      InitializeComponent();
      _grabber = null;
      _interpolation = Emgu.CV.CvEnum.INTER.CV_INTER_NN;
      _can_invoke = false;
      _roi_handler = new ROIHandler(_picture_box);
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

    public Core.FrameGrabber FrameGrabber {
      set {
        this.ReleaseGrab(_grabber);
        _grabber = value;
        this.GrabFrom(_grabber);
      }
      get { return _grabber; }
    }

    public ROIHandler ROIHandler {
      get { return _roi_handler; }
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

    void _grabber_OnFrame(Parsley.Core.FrameGrabber fg, Emgu.CV.Image<Emgu.CV.Structure.Bgr, byte> img) {
      // Note: This method is called from the frame-grabber's thread loop. 
      // The framegrabber holds a Breath on the camera to ensure that the camera object remains
      // alive during this callback. The camera object is a SharedResource, meaning that any
      // invocation to Dispose will block until all Breaths on the camera are released. Suppose
      // The thread that owns the picture box has called dispose and blocks. The callback here is called
      // and invoke is used. Invoke executes the delegate on thread that owns this control, which is the one
      // that is already blocked. This leads to a deadlock. That is why we use BeginInvoke, which executes
      // the delegate on the GUI thread associated with this control.
      if (_can_invoke) {
        Rectangle c = _picture_box.ClientRectangle;
        Emgu.CV.Image<Emgu.CV.Structure.Bgr, byte> img_copy = img.Resize(c.Width, c.Height, _interpolation);
        int w = img.Width;
        int h = img.Height;
        this.BeginInvoke(new MethodInvoker(delegate {
          // Update scaling info for back-converting ROI to original image dimensions
          _roi_handler.Scale = new SizeF((float)c.Width / w, (float)c.Height / h);

          if (_roi_handler.IsDragging) {
            img_copy.Draw(_roi_handler.Current, new Bgr(Color.Red), 1);
          }
          
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
