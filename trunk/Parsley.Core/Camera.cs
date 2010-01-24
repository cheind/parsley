using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

using Emgu.CV;
using Emgu.CV.Structure;
using System.IO;
using System.Xml.Serialization;

namespace Parsley.Core {
  
  /// <summary>
  /// Represents a camera.
  /// </summary>
  public class Camera : Resource.SharedResource {
    [XmlRoot("parsley_calibration")]
    public class Calibration {
      private Emgu.CV.IntrinsicCameraParameters _intrinsics;
      private List<Emgu.CV.ExtrinsicCameraParameters> _extrinsics;

      public Calibration() {
        _intrinsics = null;
        _extrinsics = new List<ExtrinsicCameraParameters>();
      }

      /// <summary>
      /// Access intrinsic calibration
      /// </summary>
      [XmlElement("intrinsics")]
      public Emgu.CV.IntrinsicCameraParameters Intrinsics {
        get { return _intrinsics; }
        set { _intrinsics = value; }
      }

      /// <summary>
      /// Access associated extrinsic calibrations
      /// </summary>
      [XmlElement("extrinsics")]
      public List<Emgu.CV.ExtrinsicCameraParameters> Extrinsics {
        get { return _extrinsics; }
      }

      public void Reset() {
        _intrinsics = null;
        _extrinsics.Clear();
      }
    }

    private int _device_index;
    private Emgu.CV.Capture _device;
    private Calibration _calibration;
    
    /// <summary>
    /// Initialize camera from device index
    /// </summary>
    /// <param name="device_index">Device index starting at zero.</param>
    public Camera(int device_index) {
      _calibration = new Calibration();
      this.DeviceIndex = device_index;
    }

    /// <summary>
    /// Initialize camera with no connection
    /// </summary>
    public Camera() : this(-1) {
    }

    /// <summary>
    /// True if camera has an intrinsic calibration associated.
    /// </summary>
    [Description("Determines if camera has an instrinsic calibration")]
    public bool HasIntrinsics {
      get { return _calibration.Intrinsics != null; }
    }

    /// <summary>
    /// True if camera has at least one extrinsic calibration associated.
    /// </summary>
    [Description("Determines if camera has an extrinsic calibration")]
    public bool HasExtrinsics {
      get { return _calibration.Extrinsics.Count > 0; }
    }

    /// <summary>
    /// Test if camera has a connection
    /// </summary>
    [Browsable(false)]
    public bool IsConnected {
      get { return _device != null; }
    }

    /// <summary>
    /// Connect to camera at given device index
    /// </summary>
    public int DeviceIndex {
      get { lock (this) { return _device_index; } }
      set {
        lock(this) {
          if (IsConnected) {
            _device.Dispose();
            _device = null;
            _calibration.Reset();
          }
          try {
            if (value >= 0) {
              _device = new Emgu.CV.Capture(value);
              _device_index = value;
            } else {
              _device_index = -1;
              _device = null;
            }
          } catch (NullReferenceException) {
            _device_index = -1;
            _device = null;
            //throw new ArgumentException(String.Format("No camera device found at slot {0}.", value));
          }
        }
      }
    }

    /// <summary>
    /// Access intrinsic calibration
    /// </summary>
    [Browsable(false)]
    public Emgu.CV.IntrinsicCameraParameters Intrinsics {
      get { return _calibration.Intrinsics; }
      set { _calibration.Intrinsics = value; }
    }

    /// <summary>
    /// Access associated extrinsic calibrations
    /// </summary>
    [Browsable(false)]
    public List<Emgu.CV.ExtrinsicCameraParameters> Extrinsics {
      get { return _calibration.Extrinsics; }
    }

    /// <summary>
    /// Frame width of device
    /// </summary>
    [Description("Frame Width")]
    public int FrameWidth {
      get { return (int)PropertyOrDefault(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_FRAME_WIDTH, 0);}
    }

    /// <summary>
    /// Frame height of device
    /// </summary>
    [Description("Frame Height")]
    public int FrameHeight {
      get { return (int)PropertyOrDefault(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_FRAME_HEIGHT, 0); }
    }

    /// <summary>
    /// Aspect ratio of FrameWidth / FrameHeight
    /// </summary>
    [Browsable(false)]
    public double FrameAspectRatio {
      get { return ((double)FrameWidth) / FrameHeight; }
    }

    /// <summary>
    /// Frame size of device
    /// </summary>
    [Description("Frame Size")]
    public System.Drawing.Size FrameSize {
      get { return new System.Drawing.Size(this.FrameWidth, this.FrameHeight); }
    }

    /// <summary>
    /// Retrieve the current frame.
    /// </summary>
    /// <returns></returns>
    public Image<Bgr, Byte> Frame() {
      lock (this) {
        if (this.IsConnected)
          return _device.QueryFrame();
        else
          return null;
      }
    }

    public void SaveCalibration(string path) {
      XmlSerializer s = new XmlSerializer(typeof(Calibration));
      TextWriter w = new StreamWriter(path);
      s.Serialize(w, _calibration);
      w.Close();
    }

    public void LoadCalibration(string path) {
      TextReader r = new StreamReader(path);
      XmlSerializer s = new XmlSerializer(typeof(Calibration));
      _calibration = s.Deserialize(r) as Calibration;
    }

    /// <summary>
    /// Access device property or use default value
    /// </summary>
    /// <param name="prop"> property name</param>
    /// <param name="def">default to use if not connected</param>
    /// <returns>value or default</returns>
    double PropertyOrDefault(Emgu.CV.CvEnum.CAP_PROP prop, double def) {
      double value = def;
      lock (this) {
        if (IsConnected) {
          value = _device.GetCaptureProperty(prop);
        } 
      }
      return value;
    }

    protected override void DisposeManaged() {
      if (_device != null) {
        _device.Dispose();
      }
    }
  }
}
