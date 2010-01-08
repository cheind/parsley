using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Parsley.Core {
  /// <summary>
  /// Camera properties
  /// </summary>
  public class CameraProperties {
    private Emgu.CV.Capture _camera;

    /// <summary>
    /// Construct from camera
    /// </summary>
    public CameraProperties(Emgu.CV.Capture camera) {
      _camera = camera;
    }

    [Category("Image Properties"), Description("Image brightness")]
    public double Brightness {
      get { return _camera.GetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_BRIGHTNESS); }
      set { _camera.SetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_BRIGHTNESS, value); }
    }

    [Category("Image Properties"), Description("Image contrast")]
    public double Contrast {
      get { return _camera.GetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_CONTRAST); }
      set { _camera.SetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_CONTRAST, value); }
    }

    [Category("Image Properties"), Description("FPS")]
    public double FPS {
      get { return _camera.GetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_FPS); }
      set { _camera.SetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_FPS, value); }
    }

    [Category("Image Properties"), Description("Frame Width")]
    public double FrameWidth {
      get { return _camera.GetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_FRAME_WIDTH); }
      set { _camera.SetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_FRAME_WIDTH, value); }
    }

    [Category("Image Properties"), Description("Frame Height")]
    public double FrameHeight {
      get { return _camera.GetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_FRAME_HEIGHT); }
      set { _camera.SetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_FRAME_HEIGHT, value); }
    }

    [Category("Image Properties"), Description("Hue")]
    public double Hue {
      get { return _camera.GetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_HUE); }
      set { _camera.SetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_HUE, value); }
    }

    [Category("Image Properties"), Description("Saturation")]
    public double Saturation {
      get { return _camera.GetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_SATURATION); }
      set { _camera.SetCaptureProperty(Emgu.CV.CvEnum.CAP_PROP.CV_CAP_PROP_SATURATION, value); }
    }
  }
}
