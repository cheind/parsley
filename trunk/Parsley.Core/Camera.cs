using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

using Emgu.CV;
using Emgu.CV.Structure;

namespace Parsley.Core {
  
  /// <summary>
  /// Represents a camera.
  /// </summary>
  public class Camera : SharedResource {
    private Emgu.CV.Capture _device;
    private Emgu.CV.IntrinsicCameraParameters _intrinsics;
    private List<Emgu.CV.ExtrinsicCameraParameters> _extrinsics;

    /// <summary>
    /// Initialize camera from device index
    /// </summary>
    /// <param name="device_index">Device index starting at zero.</param>
    public Camera(int device_index) {
      try {
        _device = new Emgu.CV.Capture(device_index);
      } catch (NullReferenceException) {
        throw new ArgumentException(String.Format("No camera device found at slot {0}.", device_index));
      }
      _intrinsics = null;
      _extrinsics = new List<ExtrinsicCameraParameters>();
    }

    /// <summary>
    /// Initialize with pre-existing device
    /// </summary>
    /// <remarks>Device is disposed when camera is disposed.</remarks>
    /// <param name="device">Device to initialize from</param>
    public Camera(Emgu.CV.Capture device) {
      _device = device;
    }

    /// <summary>
    /// True if camera has an intrinsic calibration associated.
    /// </summary>
    [Description("Determines if camera has an instrinsic calibration")]
    public bool HasIntrinsics {
      get { return _intrinsics != null; }
    }

    /// <summary>
    /// True if camera has at least one extrinsic calibration associated.
    /// </summary>
    [Description("Determines if camera has an extrinsic calibration")]
    public bool HasExtrinsics {
      get { return _extrinsics.Count > 0; }
    }

    /// <summary>
    /// Access intrinsic calibration
    /// </summary>
    [Browsable(false)]
    public Emgu.CV.IntrinsicCameraParameters Intrinsics {
      get { return _intrinsics; }
      set { _intrinsics = value; }
    }

    /// <summary>
    /// Access associated extrinsic calibrations
    /// </summary>
    [Browsable(false)]
    public List<Emgu.CV.ExtrinsicCameraParameters> Extrinsics {
      get { return _extrinsics; }
    }

    /// <summary>
    /// Frame width of device
    /// </summary>
    [Description("Frame Width")]
    public int FrameWidth {
      get { return (int)_device.GetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_FRAME_WIDTH); }
    }

    /// <summary>
    /// Frame height of device
    /// </summary>
    [Description("Frame Height")]
    public int FrameHeight {
      get { return (int)_device.GetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_FRAME_HEIGHT); }
    }

    /// <summary>
    /// Aspect ratio of FrameWidth / FrameHeight
    /// </summary>
    [Description("Frame Aspect Ratio of Width/Height")]
    public double FrameAspectRatio {
      get { return ((double)FrameWidth) / FrameHeight; }
    }

    /// <summary>
    /// Frame size of device
    /// </summary>
    public System.Drawing.Size FrameSize {
      get { return new System.Drawing.Size(this.FrameWidth, this.FrameHeight); }
    }

    /// <summary>
    /// Retrieve the current frame.
    /// </summary>
    /// <returns></returns>
    public Image<Bgr, Byte> Frame() {
      return _device.QueryFrame();
    }

    protected override void DisposeManaged() {
      _device.Dispose();
    }
  }
}
