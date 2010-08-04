using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathNet.Numerics.LinearAlgebra;

namespace Parsley.Core
{
  public interface IPositioner
  {

    void TransformPoints(List<Vector> points);

    Emgu.CV.ExtrinsicCameraParameters PositionerPose
    {
      get;
      set;
    }

    double Angle
    {
      get;
      set;
    }
  }
}
