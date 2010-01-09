using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Emgu.CV.Structure;

namespace Parsley.Core {

  /// <summary>
  /// Base class for intrinsic/extrinsic calibration
  /// </summary>
  public class CalibrationBase {
    private MCvPoint3D32f[] _object_points;

    /// <summary>
    /// Initialize with reference points on object
    /// </summary>
    /// <param name="object_points"></param>
    public CalibrationBase(MCvPoint3D32f[] object_points) {
      _object_points = object_points;
    }

    /// <summary>
    /// Access the object points
    /// </summary>
    public MCvPoint3D32f[] ObjectPoints {
      get { return _object_points; }
    }
  }
}
