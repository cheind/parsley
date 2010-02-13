/*
 * Parsley http://parsley.googlecode.com
 * Copyright (c) 2010, Christoph Heindl. All rights reserved.
 * Code license:	New BSD License
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;
using MathNet.Numerics.LinearAlgebra;
using Parsley.Core;

namespace Parsley.Core.Tests {

  [TestFixture]
  public class RayTest {

    Vector MakeVector(double x, double y, double z) {
      return new Vector(new double[] { x, y, z });
    }

    [Test]
    public void Construction() {
      Ray r = new Ray(MakeVector(1, 0, 0));
      Assert.AreEqual(0, (MakeVector(1, 0, 0) - r.Direction).Norm(), 0.0001);
    }

    [Test]
    public void At() {
      Ray r = new Ray(MakeVector(1, 0, 0));
      Assert.AreEqual(0, (MakeVector(1, 0, 0) - r.At(1)).Norm(), 0.00001);
      Assert.AreEqual(0, (MakeVector(5, 0, 0) - r.At(5)).Norm(), 0.00001);
    }
  }
}
