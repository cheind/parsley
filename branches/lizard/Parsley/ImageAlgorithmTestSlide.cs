using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Parsley {
  public partial class ImageAlgorithmTestSlide : FrameGrabberSlide {
    private Core.IImageAlgorithm _algorithm = null;


    public ImageAlgorithmTestSlide(Context c)
      : base(c) 
    {
      InitializeComponent();

      // Fill combobox with available algorithms
      foreach (Core.Addins.AddinInfo info in Core.Addins.AddinStore.FindAddins(typeof(Core.IImageAlgorithm))) {
        _cmb_algorithm.Items.Add(info);
      }

    }

    private ImageAlgorithmTestSlide() 
      : base(null) 
    {
      InitializeComponent();
    }

    protected override void OnFrame(Parsley.Core.BuildingBlocks.FrameGrabber fp, Emgu.CV.Image<Emgu.CV.Structure.Bgr, byte> img) {
      if (_algorithm != null) {
        _algorithm.ProcessImage(img);
      }
      base.OnFrame(fp, img);
    }

    private void ImageAlgorithmTestSlide_Load(object sender, EventArgs e) {

    }

    private void _cmb_algorithm_SelectedIndexChanged(object sender, EventArgs e) {
      if (_cmb_algorithm.SelectedIndex >= 0) {
        // Instance a new object from selected type
        Core.Addins.AddinInfo info = _cmb_algorithm.SelectedItem as Core.Addins.AddinInfo;
        if (info != null) {
          _algorithm = Core.Addins.AddinStore.CreateInstance(info) as Core.IImageAlgorithm;
          _property_grid.SelectedObject = _algorithm;
        }
      }
    }
  }
}
