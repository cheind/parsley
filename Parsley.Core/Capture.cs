using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Emgu.CV;
using Emgu.CV.Structure;

namespace Parsley.Core {

  /// <summary>
  /// Capture from camera
  /// </summary>
  public class Capture : Resource {

    private Emgu.CV.Capture _capture;

    /// <summary>
    /// Capture from camera with specific index starting at 0.
    /// </summary>
    public static Capture FromCamera(int index) {
      return new Capture(new Emgu.CV.Capture(index));
    }

    /// <summary>
    /// Capture from movie saved on disk
    /// </summary>
    public static Capture FromMovie(string filename) {
      return new Capture(new Emgu.CV.Capture(filename));
    }

    /// <summary>
    /// Capture from resource
    /// </summary>
    Capture(Emgu.CV.Capture detail) {
      _capture = detail;
    }

    /// <summary>
    /// Return the next colored frame.
    /// </summary>
    /// <returns></returns>
    public Image<Bgr, Byte> Frame() {
      return _capture.QueryFrame();
    }

    /// <summary>
    /// Access the camera properties
    /// </summary>
    public CameraProperties Properties {
      get { return new CameraProperties(_capture); }
    }

    protected override void DisposeManaged() {
      _capture.Dispose();
    }
  }
}
