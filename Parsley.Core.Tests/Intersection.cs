using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;
using MathNet.Numerics.LinearAlgebra;
using Parsley.Core;

namespace Parsley.Core.Tests {

  [TestFixture]
  public class IntersectionTest {
    
    Vector MakeVector(double x, double y, double z) {
      return new Vector(new double[] { x, y, z });
    }

    [Test]
    public void RayPlane() {
      Plane p = new Plane(MakeVector(1, 1, 1), MakeVector(0, 0, 1));
      double t;
      Assert.True(Intersection.RayPlane(new Ray(MakeVector(0, 0, 1)), p, out t));
      Assert.AreEqual(1.0, t, 0.00001);
      Assert.False(Intersection.RayPlane(new Ray(MakeVector(0, 1, 0)), p, out t));
    }
  }
}
