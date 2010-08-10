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
    /// <summary>
    /// Used to tranform the given points.
    /// </summary>
    /// <param name="points"></param>
    void TransformPoints(List<Vector> points);

    Emgu.CV.ExtrinsicCameraParameters PositionerPose
    {
      get;
      set;
    }

    /// <summary>
    /// Updates the Point transformation matrix.
    /// </summary>
    /// <param name="the_cam"></param>
    void UpdateTransformation(Camera the_cam);
  }
}
