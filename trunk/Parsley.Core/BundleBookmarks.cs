using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Parsley.Core {

  /// <summary>
  /// Fast access to frequently required bundle items.
  /// </summary>
  public class BundleBookmarks {
    private Bundle _b;

    public BundleBookmarks(Bundle b) {
      _b = b;
    }

    public Bundle Bundle {
      get { return _b; }
    }

    public Emgu.CV.Image<Emgu.CV.Structure.Bgr, byte> Image {
      get { return _b.Fetch<Emgu.CV.Image<Emgu.CV.Structure.Bgr, byte>>("image"); }
      set { _b.Store("image", value); }
    }

    public List<Plane> ReferencePlanes {
      get { return _b.Fetch<List<Plane>>("reference_planes"); }
      set { _b.Store("reference_planes", value); }
    }

    public System.Drawing.Rectangle ROI {
      get { return _b.Fetch<System.Drawing.Rectangle>("roi"); }
      set { _b.Store("roi", value); }
    }

    public List<Ray> EyeRays {
      get { return _b.Fetch<List<Ray>>("eye_rays"); }
      set { _b.Store("eye_rays", value); }
    }

    public ColorChannel LaserColor {
      get { return _b.Fetch<ColorChannel>("laser_color"); }
      set { _b.Store("laser_color", value); }
    }

    public List<System.Drawing.PointF> LaserPixel {
      get { return _b.Fetch<List<System.Drawing.PointF>>("laser_pixel"); }
      set { _b.Store("laser_pixel", value); }
    }

    public Plane LaserPlane {
      get { return _b.Fetch<Plane>("laser_plane"); }
      set { _b.Store("laser_plane", value); } 
    }
  }
}
