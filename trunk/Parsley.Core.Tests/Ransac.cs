using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using NUnit.Framework;
using MathNet.Numerics.LinearAlgebra;
using Parsley.Core.LaserPlane;

namespace Parsley.Core.Tests {
  [TestFixture]
  class RansacTest {
    
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
      r.Run(100, 0.01, 80);

      Assert.Greater(r.Hypotheses.Count, 20);
      int count_xy = 0;

      foreach (Ransac<PlaneModel>.Hypothesis hyp in r.Hypotheses) {
        Assert.GreaterOrEqual(hyp.ConsensusIds.Count, 80);
        Plane p = hyp.Model.Plane;
        double d_xy = p.Normal.ScalarMultiply(MakeVector(0, 0, 1));
        if ((Math.Abs(d_xy) - 1) < 1e-5) {
          count_xy++;
        } else {
          Assert.Fail("Wrong plane found by Ransac");
        }
        foreach (Vector v in hyp.ConsensusSet) {
          Assert.AreEqual(0, p.DistanceTo(v), 0.001);
        }
      }

      Assert.GreaterOrEqual(count_xy, 10);
    }
  }
}
