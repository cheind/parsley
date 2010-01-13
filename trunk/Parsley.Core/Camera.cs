using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

using Emgu.CV;
using Emgu.CV.Structure;

namespace Parsley.Core {
  
  /// <summary>
  /// High level access to a camera
  /// </summary>
  public class Camera : SharedResource {
    private Emgu.CV.Capture _device;
    private Emgu.CV.IntrinsicCameraParameters _intrinsics;
    private Emgu.CV.ExtrinsicCameraParameters _extrinsics;

    public Camera(int device_index) {
      _device = new Emgu.CV.Capture(device_index);
      _intrinsics = null;
      _extrinsics = null;
    }

    public Camera(Emgu.CV.Capture device) {
      _device = device;
    }

    [Description("Determines if camera has an instrinsic calibration")]
    public bool HasIntrinsics {
      get { return _intrinsics != null; }
    }

    [Description("Determines if camera has an extrinsic calibration")]
    public bool HasExtrinsics {
      get { return _extrinsics != null; }
    }

    [Browsable(false)]
    public Emgu.CV.IntrinsicCameraParameters Intrinsics {
      get { return _intrinsics; }
      set { _intrinsics = value; }
    }

    [Browsable(false)]
    public Emgu.CV.ExtrinsicCameraParameters Extrinsics {
      get { return _extrinsics; }
      set { _extrinsics = value; }
    }

    [Description("Frame Width")]
    public int FrameWidth {
      get { return (int)_device.GetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_FRAME_WIDTH); }
    }

    [Description("Frame Height")]
    public int FrameHeight {
      get { return (int)_device.GetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_FRAME_HEIGHT); }
    }

    [Description("Frame Aspect Ratio of Width/Height")]
    public double FrameAspectRatio {
      get { return ((double)FrameWidth) / FrameHeight; }
    }

    public Image<Bgr, Byte> Frame() {
      return _device.QueryFrame();
    }

    protected override void DisposeManaged() {
      _device.Dispose();
    }
  }
}
