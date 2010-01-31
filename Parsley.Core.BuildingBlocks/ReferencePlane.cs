using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MathNet.Numerics.LinearAlgebra;
using Parsley.Core.Extensions;

namespace Parsley.Core.BuildingBlocks {
  
  public class ReferencePlane {
    private Plane _plane;
    private System.Drawing.RectangleF _roi;
    private Emgu.CV.ExtrinsicCameraParameters _ecp;

    /// <summary>
    /// Construct reference plane from extrinsic calibration
    /// </summary>
    /// <param name="ecp">Extrinsic calibration</param>
    public ReferencePlane(Emgu.CV.ExtrinsicCameraParameters ecp, System.Drawing.Rectangle roi) {
      Matrix m = ecp.ExtrinsicMatrix.ToParsley();
      _ecp = ecp;
      _plane = new Plane(m.GetColumnVector(3), m.GetColumnVector(2).Normalize());
      _roi = new System.Drawing.RectangleF(roi.Location, roi.Size);
    }

    /// <summary>
    /// Get the plane equation
    /// </summary>
    public Plane Plane {
      get { return _plane; }
    }

    /// <summary>
    /// Access the ROI
    /// </summary>
    public System.Drawing.RectangleF ROI {
      get { return _roi; }
    }

    public Emgu.CV.ExtrinsicCameraParameters Extrinsic {
      get { return _ecp; }
    }
  }
}
