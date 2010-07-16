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
  class RansacTest {
    
    /*
    Vector MakeVector(double x, double y, double z) {
      return new Vector(new double[] { x, y, z });
    }

    [Test]
    public void FindPlane() {
      Plane xy = new Plane(MakeVector(0, 0, 0), MakeVector(0, 0, 1));
      Plane noise = new Plane(MakeVector(0, 0, 0), MakeVector(1, 0, 0));

      Vector[] xy_points = PlaneTest.RandomPointsOnPlane(xy, 100);
      Vector[] noise_points = PlaneTest.RandomPointsOnPlane(noise, 10);
      Vector[] points = new Vector[110];

      xy_points.CopyTo(points, 0);
      noise_points.CopyTo(points, 100);

      Ransac<PlaneModel> r = new Ransac<PlaneModel>(points);
      int count_xy = 0;
      for (int i = 0; i < 100; ++i) {
        Ransac<PlaneModel>.Hypothesis h = r.Run(100, 0.01, 80, null);
        if (h != null) {
          Assert.GreaterOrEqual(h.ConsensusIds.Count, 80);
          Plane p = h.Model.Plane;
          double d_xy = p.Normal.ScalarMultiply(MakeVector(0, 0, 1));
          if ((Math.Abs(d_xy) - 1) < 1e-5) {
            count_xy++;
          } else {
            Assert.Fail("Wrong plane found by Ransac");
          }
          foreach (Vector v in h.ConsensusSet) {
            Assert.AreEqual(0, p.SignedDistanceTo(v), 0.001);
          }
        }
      }
      Assert.GreaterOrEqual(count_xy, 10);
    }
     */
  }
}
