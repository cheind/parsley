using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathNet.Numerics.LinearAlgebra;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Parsley.Core.BuildingBlocks
{
  [Serializable]
  [Parsley.Core.Addins.Addin]
  public class CompositePattern : CalibrationPattern, ISerializable
  {
    CalibrationPattern _patternA;
    CalibrationPattern _patternB;
    double _rotation_BRelativeA;
    double _translationX;
    double _translationY;
    double _translationZ;
    Matrix _transformationBToA;


    public CompositePattern()
    {
      _patternA = null;
      _patternB = null;
      _rotation_BRelativeA = 180.0;
      _translationX = 0;
      _translationY = 0;
      _translationZ = 0;
      _transformationBToA = Matrix.Identity(4, 4);
    }

    public CompositePattern(SerializationInfo info, StreamingContext context)
    {
      _patternA = (CalibrationPattern)info.GetValue("patternA", typeof(CalibrationPattern));
      _patternB = (CalibrationPattern)info.GetValue("patternB", typeof(CalibrationPattern));
      _rotation_BRelativeA = (double)info.GetValue("rotationBA", typeof(double));
      _translationX = (double)info.GetValue("translationX", typeof(double));
      _translationY = (double)info.GetValue("translationY", typeof(double));
      _translationZ = (double)info.GetValue("translationZ", typeof(double));

      SetTransformationMatrixBA();
    }

    public void GetObjectData(SerializationInfo info, StreamingContext context) {
      info.AddValue("patternA", _patternA);
      info.AddValue("patternB", _patternB);
      info.AddValue("rotationBA", _rotation_BRelativeA);
      info.AddValue("translationX", _translationX);
      info.AddValue("translationY", _translationY);
      info.AddValue("translationZ", _translationZ);
    }


    [Editor(typeof(Parsley.Core.BuildingBlocks.PatternTypeEditor),
            typeof(System.Drawing.Design.UITypeEditor))]
    public CalibrationPattern PatternA
    {
      get { return _patternA; }
      set { _patternA = value; }
    }

    [Editor(typeof(Parsley.Core.BuildingBlocks.PatternTypeEditor),
            typeof(System.Drawing.Design.UITypeEditor))]
    public CalibrationPattern PatternB
    {
      get { return _patternB; }
      set { _patternB = value; }
    }

    [Browsable(false)]
    public Matrix TransformationMatrixBA
    {
      get { return _transformationBToA; }
    }


    public double RotationBRelativeA
    {
      get { return _rotation_BRelativeA; }

      set
      {
        if (Math.Abs(value) <= 360)
          _rotation_BRelativeA = value;
        else
          _rotation_BRelativeA = 0;

        SetTransformationMatrixBA();
      }
    }

    public double TranslationX
    {
      get { return _translationX; }

      set 
      { 
        _translationX = value;
        SetTransformationMatrixBA();
      }
    }

    public double TranslationY
    {
      get { return _translationY; }

      set 
      { 
        _translationY = value;
        SetTransformationMatrixBA();
      }
    }

    public double TranslationZ
    {
      get { return _translationZ; }

      set 
      { 
        _translationZ = value;
        SetTransformationMatrixBA();
      }
    }

    public override bool FindPattern(Emgu.CV.Image<Emgu.CV.Structure.Gray, byte> img, out System.Drawing.PointF[] image_points)
    {
      if (_patternA != null && _patternB != null)
      {
        bool foundA, foundB;

        foundB = _patternB.FindPattern(img, out image_points);
        foundA = _patternA.FindPattern(img, out image_points);

        this.ObjectPoints = _patternA.ObjectPoints;

        return (foundA && foundB);
      }
      else
      {
        image_points = null;
        return false;
      }
    }


    private void SetTransformationMatrixBA()
    {
      double angle_rad = _rotation_BRelativeA / 180.0 * Math.PI;

      _transformationBToA = Matrix.Identity(4, 4);
      //set up rotation and translation matrix (rotation around z-axis)
      _transformationBToA[0, 0] = Math.Cos(angle_rad);
      _transformationBToA[0, 1] = -Math.Sin(angle_rad);
      _transformationBToA[1, 0] = Math.Sin(angle_rad);
      _transformationBToA[1, 1] = Math.Cos(angle_rad);

      //translation
      _transformationBToA[0, 3] = _translationX;
      _transformationBToA[1, 3] = _translationY;
      _transformationBToA[2, 3] = _translationZ;
    }
  }
}
