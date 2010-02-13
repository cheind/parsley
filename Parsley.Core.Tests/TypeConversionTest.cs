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
using Parsley.Core.Extensions;


namespace Parsley.Core.Tests {

  [TestFixture]
  class TypeConversionTest {

    [Test]
    public void TestConvertEmguToParsley() {
      Emgu.CV.Structure.MCvPoint3D32f p = new Emgu.CV.Structure.MCvPoint3D32f(1.0f, 2.0f, 3.0f);
      MathNet.Numerics.LinearAlgebra.Vector v = p.ToParsley();

      Assert.AreEqual(1.0, v[0]);
      Assert.AreEqual(2.0, v[1]);
      Assert.AreEqual(3.0, v[2]);

      double[,] data = new double[2, 3] { { 1.0, 2.0, 3.0 }, { 4.0, 5.0, 6.0 } };

      Emgu.CV.Matrix<double> m = new Emgu.CV.Matrix<double>(data);
      MathNet.Numerics.LinearAlgebra.Matrix m2 = m.ToParsley();

      Assert.AreEqual(data[0, 0], m2[0, 0]);
      Assert.AreEqual(data[0, 1], m2[0, 1]);
      Assert.AreEqual(data[0, 2], m2[0, 2]);
      Assert.AreEqual(data[1, 0], m2[1, 0]);
      Assert.AreEqual(data[1, 1], m2[1, 1]);
      Assert.AreEqual(data[1, 2], m2[1, 2]);
    }

    [Test]
    public void TestConvertSystemToParsley() {
      System.Drawing.PointF p = new System.Drawing.PointF(1.0f, 2.0f);
      MathNet.Numerics.LinearAlgebra.Vector v = p.ToParsley();

      Assert.AreEqual(1.0, v[0]);
      Assert.AreEqual(2.0, v[1]);
    }

    [Test]
    public void TestConvertParsleyToEmgu() {
      MathNet.Numerics.LinearAlgebra.Vector v = new MathNet.Numerics.LinearAlgebra.Vector(new double[] { 1.0f, 2.0f, 3.0f });
      Emgu.CV.Structure.MCvPoint3D32f f = v.ToEmguF();
      Assert.AreEqual(1.0, f.x);
      Assert.AreEqual(2.0, f.y);
      Assert.AreEqual(3.0, f.z);

      Emgu.CV.Structure.MCvPoint3D64f d = v.ToEmgu();
      Assert.AreEqual(1.0, d.x);
      Assert.AreEqual(2.0, d.y);
      Assert.AreEqual(3.0, d.z);

      double[,] data = new double[2, 3] { { 1.0, 2.0, 3.0 }, { 4.0, 5.0, 6.0 } };

      MathNet.Numerics.LinearAlgebra.Matrix m = MathNet.Numerics.LinearAlgebra.Matrix.Create(data);
      Emgu.CV.Matrix<double> m2 = m.ToEmgu();

      Assert.AreEqual(data[0, 0], m2[0, 0]);
      Assert.AreEqual(data[0, 1], m2[0, 1]);
      Assert.AreEqual(data[0, 2], m2[0, 2]);
      Assert.AreEqual(data[1, 0], m2[1, 0]);
      Assert.AreEqual(data[1, 1], m2[1, 1]);
      Assert.AreEqual(data[1, 2], m2[1, 2]);
    }

    [Test]
    public void TestConvertParsleyToInterop() {
      MathNet.Numerics.LinearAlgebra.Vector v = new MathNet.Numerics.LinearAlgebra.Vector(new double[] { 1.0, 2.0, 3.0 });
      double[] i = v.ToInterop();

      Assert.AreEqual(1.0, i[0]);
      Assert.AreEqual(2.0, i[1]);
      Assert.AreEqual(3.0, i[2]);

      double[,] data = new double[2, 3] { { 1.0, 2.0, 3.0 }, { 4.0, 5.0, 6.0 } };

      MathNet.Numerics.LinearAlgebra.Matrix m = MathNet.Numerics.LinearAlgebra.Matrix.Create(data);
      double[,] m2 = m.ToInterop();


      Assert.AreEqual(data[0, 0], m2[0, 0]);
      Assert.AreEqual(data[0, 1], m2[0, 1]);
      Assert.AreEqual(data[0, 2], m2[0, 2]);
      Assert.AreEqual(data[1, 0], m2[1, 0]);
      Assert.AreEqual(data[1, 1], m2[1, 1]);
      Assert.AreEqual(data[1, 2], m2[1, 2]);
    }


  }
}
