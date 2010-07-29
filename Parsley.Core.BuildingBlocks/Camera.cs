/*
 * Parsley http://parsley.googlecode.com
 * Copyright (c) 2010, Christoph Heindl. All rights reserved.
 * Code license:	New BSD License
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

using Emgu.CV;
using Emgu.CV.Structure;
using System.IO;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace Parsley.Core.BuildingBlocks {
  
  /// <summary>
  /// Represents a camera.
  /// </summary>
  [Serializable]
  public class Camera : Core.Resource, ISerializable {

    private int _device_index;
    private Emgu.CV.Capture _device;
    private Emgu.CV.IntrinsicCameraParameters _intrinsics;
    
    /// <summary>
    /// Initialize camera from device index
    /// </summary>
    /// <param name="device_index">Device index starting at zero.</param>
    public Camera(int device_index) {
      _device_index = -1;
      this.DeviceIndex = device_index;
      _intrinsics = null;
    }

    public Camera(SerializationInfo info, StreamingContext context)
    {
      _device_index = -1;
      int dev_id = (int)info.GetValue("device_index", typeof(int));
      _intrinsics = (Emgu.CV.IntrinsicCameraParameters)info.GetValue("intrinsic", typeof(Emgu.CV.IntrinsicCameraParameters));
      this.DeviceIndex = dev_id;
      System.Drawing.Size last_frame_size = (System.Drawing.Size)info.GetValue("last_frame_size", typeof(System.Drawing.Size));
      this.FrameWidth = last_frame_size.Width;
      this.FrameHeight = last_frame_size.Height;
    }

    public void GetObjectData(SerializationInfo info, StreamingContext context) {
      info.AddValue("device_index", _device_index);
      info.AddValue("intrinsic", _intrinsics);
      info.AddValue("last_frame_size", this.FrameSize);
    }


    /// <summary>
    /// Initialize camera with no connection
    /// </summary>
    public Camera() : this(-1) {
    }

    /// <summary>
    /// Access intrinsic camera parameters.
    /// </summary>
    [Editor(typeof(IntrinsicTypeEditor),
        typeof(System.Drawing.Design.UITypeEditor))]
    public Emgu.CV.IntrinsicCameraParameters Intrinsics {
      get { return _intrinsics; }
      set { _intrinsics = value; }
    }

    /// <summary>
    /// Test if camera has a connection
    /// </summary>
    [Browsable(false)]
    public bool IsConnected {
      get { return _device_index > -1;}
    }

    /// <summary>
    /// True if camera has intrinsic parameters assigned
    /// </summary>
    [Browsable(false)]
    public bool HasIntrinsics {
      get { return _intrinsics != null; }
    }

    /// <summary>
    /// Connect to camera at given device index
    /// </summary>
    [Description("Specifies the camera device index to use. A device index less than zero indicates no connection. Default is zero.")]
    [RefreshProperties(RefreshProperties.All)]
    public int DeviceIndex {
      get { lock (this) { return _device_index; } }
      set {
        lock(this) {
          if (IsConnected) {
            _device.Dispose();
            _device = null;
            _intrinsics = null;
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
          }
        }
      }
    }

    /// <summary>
    /// Frame width of device
    /// </summary>
    [Description("The width of the camera frame in pixels.")]
    public int FrameWidth {
      get { return (int)GetPropertyOrDefault(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_FRAME_WIDTH, 0);}
      set { SetProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_FRAME_WIDTH, value); }
    }

    /// <summary>
    /// Frame height of device
    /// </summary>
    [Description("The height of the camera frame in pixels.")]
    public int FrameHeight {
      get { return (int)GetPropertyOrDefault(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_FRAME_HEIGHT, 0); }
      set { SetProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_FRAME_HEIGHT, value); }
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
    [Browsable(false)]
    public System.Drawing.Size FrameSize {
      get { return new System.Drawing.Size(this.FrameWidth, this.FrameHeight); }
    }

    /// <summary>
    /// Retrieve the current frame.
    /// </summary>
    /// <returns></returns>
    public Image<Bgr, Byte> Frame() {
      lock (this) {
        if (this.IsConnected && _device != null)
          return _device.QueryFrame();
        else
          return null;
      }
    }

    /// <summary>
    /// Access device property or use default value
    /// </summary>
    /// <param name="prop"> property name</param>
    /// <param name="def">default to use if not connected</param>
    /// <returns>value or default</returns>
    double GetPropertyOrDefault(Emgu.CV.CvEnum.CAP_PROP prop, double def) {
      double value = def;
      lock (this) {
        if (IsConnected) {
          value = _device.GetCaptureProperty(prop);
        } 
      }
      return value;
    }

    private void SetProperty(Emgu.CV.CvEnum.CAP_PROP prop, int v)
    {
      double value = v;
      lock (this)
      {
        if (IsConnected)
        {
          _device.SetCaptureProperty(prop, value);
        }
      }
    }

    protected override void DisposeManaged() {
      if (_device != null) {
        _device.Dispose();
      }
    }
  }
}
