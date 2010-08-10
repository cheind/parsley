using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MathNet.Numerics.LinearAlgebra;
using System.ComponentModel;
using Parsley.Core.Extensions;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Windows.Forms;
using Emgu.CV.Structure;

namespace Parsley.Core.BuildingBlocks {

  [Serializable]
  [Parsley.Core.Addins.Addin]
  public class RotaryPositioner : ISerializable, IPositioner {
    Emgu.CV.ExtrinsicCameraParameters _ecp;
    private Matrix _final;
    private double _angle_degrees;

    public RotaryPositioner() {
      _final = Matrix.Identity(4, 4);
      _ecp = null;
    }

    public RotaryPositioner(SerializationInfo info, StreamingContext context)
    {
      _ecp = (Emgu.CV.ExtrinsicCameraParameters)info.GetValue("ecp",  typeof(Emgu.CV.ExtrinsicCameraParameters));
      _final = Matrix.Create((double[][])info.GetValue("final", typeof(double[][])));
      _angle_degrees = (double)info.GetValue("angle", typeof(double));
    }

    public void GetObjectData(SerializationInfo info, StreamingContext context) {
      info.AddValue("ecp", _ecp);
      info.AddValue("final", _final.GetArray());
      info.AddValue("angle", _angle_degrees);
    }


    /// <summary>
    /// Set/Get the angular position of the positioner in degrees unit.
    /// </summary>
    public double Angle {
      get { return _angle_degrees; }
      set {
        _angle_degrees = value;
      }
    }


    /// <summary>
    /// Calculates the transformation matrix.
    /// Firstly, the transformation matrix for a common rotation around the z-axis is created, 
    /// using the desired angular position, stored in _angle_degrees.
    /// Secondly, the extrinsic matrix (of the intial positioner pose [extrinsic calibration) is
    /// extracted into a 4x4-matrix.
    /// Finally, the final transformation matrix is calculated:
    /// * transform the points of the camera coordinate system, into the positioner system.
    /// * perform the rotation.
    /// * transform back to the camera coordinate system.
    /// </summary>
    /// <param name="the_cam"></param>
    public void UpdateTransformation(Camera the_cam)
    {
      double angle_rad = _angle_degrees / 180.0 * Math.PI;
      Matrix rz = Matrix.Identity(4, 4);
      rz[0, 0] = Math.Cos(angle_rad);
      rz[0, 1] = Math.Sin(angle_rad);
      rz[1, 0] = -Math.Sin(angle_rad);
      rz[1, 1] = Math.Cos(angle_rad);

      Matrix e;
      if (_ecp == null) {
        e = Matrix.Identity(4, 4);
      } else {
        e = Matrix.Identity(4, 4);
        for (int i = 0; i < 3; ++i) {
          for (int j = 0; j < 4; ++j) {
            e[i, j] = _ecp.ExtrinsicMatrix[i, j];
          }
        }
      }

      _final = e * rz * e.Inverse();
    }

    [Editor(typeof(ExtrinsicTypeEditor),
            typeof(System.Drawing.Design.UITypeEditor))]
    public Emgu.CV.ExtrinsicCameraParameters PositionerPose {
      get {
        return _ecp;
      }
      set {
        _ecp = value;
      }
    }

    /// <summary>
    /// Transforms 3D-Points from the Camera Coordinate System into the Marker Coordinate System
    /// The transformation matrix "_final" is calculated in "UpdateTransformation"
    /// </summary>
    /// <param name="points"> List of Vectors, containing the 3d points which should be transformed</param>
    public void TransformPoints(List<Vector> points) {
      for (int i = 0; i < points.Count; ++i) {
        points[i] = _final.Multiply(points[i].ToHomogeneous(1).ToColumnMatrix()).GetColumnVector(0).ToNonHomogeneous();
      }
    }
  }
}
