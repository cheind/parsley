using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Parsley.Core {
  public class Bookmarks {
    private Dictionary<string, object> _v;

    public Bookmarks(Dictionary<string, object> values) {
      _v = values;
    }

    public Dictionary<string, object> Values {
      get { return _v; }
    }

    public Emgu.CV.Image<Emgu.CV.Structure.Bgr, byte> Image {
      get { return (Emgu.CV.Image<Emgu.CV.Structure.Bgr, byte>)_v["image"]; }
      set { _v["image"] = value; }
    }

    public List<Plane> ReferencePlanes {
      get { return (List<Plane>)_v["reference_planes"]; }
      set { _v["reference_planes"] = value; }
    }

    public System.Drawing.Rectangle ROI {
      get { return (System.Drawing.Rectangle)_v["roi"]; }
      set { _v["roi"] = value; }
    }

    public List<Ray> EyeRays {
      get { return (List<Ray>)_v["eye_rays"]; }
      set { _v["eye_rays"] = value; }
    }

    public ColorChannel LaserColor {
      get { return (ColorChannel)_v["laser_color"]; }
      set { _v["laser_color"] = value; }
    }

    public List<System.Drawing.PointF> LaserPixel {
      get { return (List<System.Drawing.PointF>)_v["laser_pixel"]; }
      set { _v["laser_pixel"] = value; }
    }

    public Plane LaserPlane {
      get { return (Plane)_v["laser_plane"]; }
      set { _v["laser_plane"] = value; }
    }
  }
}
