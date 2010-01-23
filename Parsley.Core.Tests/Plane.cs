using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;
using MathNet.Numerics.LinearAlgebra;
using Parsley.Core.LaserPlane;

namespace Parsley.Core.Tests {

  [TestFixture]
  class PlaneTest {

    Vector MakeVector(double x, double y, double z) {
      return new Vector(new double[] { x, y, z });
    }

    [Test]
    public void ConstructorPointNormal() {
      Plane p = new Plane(MakeVector(0, 0, 0), MakeVector(0, 0, 10));
      Assert.AreEqual(0, p.D);
      Assert.AreEqual(1, p.Normal.Norm(), 0.00001);
      Assert.AreEqual(0, (MakeVector(0, 0, 1) - p.Normal).Norm(), 0.00001);

      p = new Plane(MakeVector(1, 1, 1), MakeVector(0, 0, 1));
      Assert.AreEqual(p.D, -1, 0.00001);
    }

    [Test]
    public void ConstructorPoints() {
      Plane p = new Plane(MakeVector(0, 0, 0), MakeVector(1, 0, 0), MakeVector(0, 1, 0));
      Assert.AreEqual(0, p.D);
      Assert.AreEqual(0, (MakeVector(0, 0, 1) - p.Normal).Norm(), 0.00001);

      Assert.Throws<ArgumentException>(delegate {
        new Plane(MakeVector(1, 1, 1), MakeVector(2, 2, 2), MakeVector(3, 3, 3));
      });
    }

    [Test]
    public void DistanceTo() {
      Plane p = new Plane(MakeVector(2, 2, 2), MakeVector(0, 0, 1));
      Assert.AreEqual(-2, p.DistanceTo(MakeVector(0, 0, 0)), 0.00001);
      Assert.AreEqual(0, p.DistanceTo(MakeVector(0, 0, 2)), 0.00001);
      Assert.AreEqual(1, p.DistanceTo(MakeVector(3, 2, 3)), 0.00001);
    }

    [Test]
    public void FitThrough() {
      Assert.Throws<ArgumentException>(delegate {
        Plane.FitThrough(new Vector[] { MakeVector(0, 0, 0) });
      });
    }


  }
}
