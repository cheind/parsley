using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathNet.Numerics.LinearAlgebra;
using Emgu.CV.Structure;

namespace Parsley.Core.BuildingBlocks
{
  public interface IPositioner
  {

    void TransformPoints(List<Vector> points);

    Emgu.CV.ExtrinsicCameraParameters PositionerPose
    {
      get;
      set;
    }

    void UpdateTransformation(Camera the_cam);
  }
}
