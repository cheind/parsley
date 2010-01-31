using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;
using MathNet.Numerics.LinearAlgebra;
using Parsley.Core;

namespace Parsley.Core.Tests {

  [TestFixture]
  public class PlaneTest {

    public static Vector[] RandomPointsOnPlane(Plane p, int count) {
      // 2 free variables, last one is calculated
      Vector[] vecs = new Vector[count];

      Vector n = p.Normal;
      int i = n[0] > 0 ? 0 : (n[1] > 0 ? 1 : 2);
      int j = (i + 1) % 3;
      int k = (i + 2) % 3;

      Random _r = new Random();
      for (int x = 0; x < count; ++x) {
        Vector v = new Vector(3);
        v[j] = -100.0 + _r.NextDouble() * 200;
        v[k] = -100.0 + _r.NextDouble() * 200;
        v[i] = (- p.D - p.Normal[j] * v[j] - p.Normal[k] * v[k]) / p.Normal[i];
        vecs[x] = v;
      }
      return vecs;
    }

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
      Plane p;
      Assert.True(Plane.FromPoints(MakeVector(0, 0, 0), MakeVector(1, 0, 0), MakeVector(0, 1, 0), out p));
      Assert.AreEqual(0, p.D);
      Assert.AreEqual(0, (MakeVector(0, 0, 1) - p.Normal).Norm(), 0.00001);

      Assert.False(Plane.FromPoints(MakeVector(1, 1, 1), MakeVector(2, 2, 2), MakeVector(3, 3, 3), out p));
    }

    [Test]
    public void DistanceTo() {
      Plane p = new Plane(MakeVector(2, 2, 2), MakeVector(0, 0, 1));
      Assert.AreEqual(-2, p.DistanceTo(MakeVector(0, 0, 0)), 0.00001);
      Assert.AreEqual(0, p.DistanceTo(MakeVector(0, 0, 2)), 0.00001);
      Assert.AreEqual(1, p.DistanceTo(MakeVector(3, 2, 3)), 0.00001);
    }

    [Test]
    public void FitByAveraging() {
      Plane p = new Plane(MakeVector(1, 1, 1), MakeVector(1, 1, 1).Normalize());

      Vector[] v = PlaneTest.RandomPointsOnPlane(p, 1000);
      Profile prof = new Profile("average-fit");
      
      Plane r = Plane.FitByAveraging(v);
      prof.Dispose();

      Assert.AreEqual(1.0, Math.Abs(Vector.ScalarProduct(r.Normal, p.Normal)), 0.00001);
      Assert.AreEqual(Math.Abs(r.D) - Math.Abs(p.D), 0, 0.00001);

      Assert.Throws<ArgumentException>(delegate {
        Plane.FitByPCA(new Vector[] { MakeVector(0, 0, 0) });
      });
    }

    [Test]
    public void FitByPCA() {
      Plane p = new Plane(MakeVector(1, 1, 1), MakeVector(1, 1, 1).Normalize());

      Vector[] v = PlaneTest.RandomPointsOnPlane(p, 1000);
      Profile prof = new Profile("pca-fit");

      Plane r = Plane.FitByPCA(v);
      prof.Dispose();

      Assert.AreEqual(1.0, Math.Abs(Vector.ScalarProduct(r.Normal, p.Normal)), 0.00001);
      Assert.AreEqual(Math.Abs(r.D) - Math.Abs(p.D), 0, 0.00001);

      Assert.Throws<ArgumentException>(delegate {
        Plane.FitByPCA(new Vector[] { MakeVector(0, 0, 0) });
      });
    }


  }
}
