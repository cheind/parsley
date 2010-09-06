using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathNet.Numerics.LinearAlgebra;
using System.ComponentModel;
using System.Runtime.Serialization;
using log4net;
using Parsley.Core.Extensions;

namespace Parsley.Core.CalibrationPatterns
{
  /// <summary>
  /// Provides a CalibrationPattern, consisting of two predefined different patterns (e.g. circle, marker, chessboard).
  /// PatternA is defined as main-pattern. Additionally to both patterns, the coordinate transformation matrix
  /// describing the relationship of the coordinate systems of patternA and patternB is predefined (pattern editor).
  /// The function member FindPattern tries to find one of these patterns: if only patternB is found,
  /// the object points of patternA are transformed (using the defined transformation) to the coordinate system of patternB.
  /// Finally the image points of the visible patternA are calculated, using the intrinsic calibration matrix with respect to
  /// patternB, by projecting the transformed object points of patternA into the cameras image coordinate system.
  /// 
  /// Please find more explanations, regarding this procedure, in the source code comments below.
  /// </summary>
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
    private readonly ILog _logger;

    /// <summary>
    /// Default Constructor.
    /// </summary>
    public CompositePattern()
    {
      _patternA = null;
      _patternB = null;
      _rotation_BRelativeA = 180.0;
      _translationX = 0;
      _translationY = 0;
      _translationZ = 0;
      _transformationBToA = Matrix.Identity(4, 4);
      _logger = LogManager.GetLogger(typeof(CompositePattern));
    }

    /// <summary>
    /// Deserialization constructor.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    public CompositePattern(SerializationInfo info, StreamingContext context)
    {
      _patternA = (CalibrationPattern)info.GetValue("patternA", typeof(CalibrationPattern));
      _patternB = (CalibrationPattern)info.GetValue("patternB", typeof(CalibrationPattern));
      _rotation_BRelativeA = (double)info.GetValue("rotationBA", typeof(double));
      _translationX = (double)info.GetValue("translationX", typeof(double));
      _translationY = (double)info.GetValue("translationY", typeof(double));
      _translationZ = (double)info.GetValue("translationZ", typeof(double));

      SetTransformationMatrixBA();
      _logger = LogManager.GetLogger(typeof(CompositePattern));
    }

    /// <summary>
    /// Sets the serialization information, to determine which data should be saved.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    public void GetObjectData(SerializationInfo info, StreamingContext context) {
      info.AddValue("patternA", _patternA);
      info.AddValue("patternB", _patternB);
      info.AddValue("rotationBA", _rotation_BRelativeA);
      info.AddValue("translationX", _translationX);
      info.AddValue("translationY", _translationY);
      info.AddValue("translationZ", _translationZ);
    }

    /// <summary>
    /// Pattern type editor used to set the main pattern A.
    /// </summary>
    [Editor(typeof(Parsley.Core.CalibrationPatterns.PatternTypeEditor),
            typeof(System.Drawing.Design.UITypeEditor))]
    public CalibrationPattern PatternA
    {
      get { return _patternA; }
      set 
      { 
        //object points of the composite pattern are always set to main pattern A
        _patternA = value;
        this.ObjectPoints = _patternA.ObjectPoints;
      }
    }

    /// <summary>
    /// Pattern type editor used to set the main pattern B.
    /// </summary>
    [Editor(typeof(Parsley.Core.CalibrationPatterns.PatternTypeEditor),
            typeof(System.Drawing.Design.UITypeEditor))]
    public CalibrationPattern PatternB
    {
      get { return _patternB; }
      set { _patternB = value; }
    }

    /// <summary>
    /// Property: transformation matrix from coordinate system B to coordinate system A
    /// </summary>
    [Browsable(false)]
    public Matrix TransformationMatrixBA
    {
      get { return _transformationBToA; }
    }


    /// <summary>
    /// Sets the angular displacement between coordinate system B to coordinate system A.
    /// Note that the absolute value is limited to 180 degrees.
    /// After setting the displacement => update the transformation matrix.
    /// </summary>
    public double RotationBRelativeA
    {
      get { return _rotation_BRelativeA; }

      set
      {
        if (Math.Abs(value) <= 180)
          _rotation_BRelativeA = value;
        else
          _rotation_BRelativeA = 0;

        SetTransformationMatrixBA();
      }
    }

    /// <summary>
    /// Sets the X-Component of the translation vector.
    /// The transformation matrix is being updated.
    /// </summary>
    public double TranslationX
    {
      get { return _translationX; }

      set 
      { 
        _translationX = value;
        SetTransformationMatrixBA();
      }
    }

    /// <summary>
    /// Sets the Y-Component of the translation vector.
    /// The transformation matrix is being updated.
    /// </summary>
    public double TranslationY
    {
      get { return _translationY; }

      set 
      { 
        _translationY = value;
        SetTransformationMatrixBA();
      }
    }

    /// <summary>
    /// Sets the Z-Component of the translation vector.
    /// The transformation matrix is being updated.
    /// </summary>
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
      if (this.IntrinsicParameters != null && _patternA != null && _patternB != null)
      {
        bool foundA = false;
        System.Drawing.PointF[] currentImagePointsA;
        System.Drawing.PointF[] currentImagePointsB;

        this.ObjectPoints = _patternA.ObjectPoints;

        foundA = _patternA.FindPattern(img, out currentImagePointsA);

        if (foundA)
        {
          image_points = currentImagePointsA;
          //_logger.Info("Pattern found.");
          return true;
        }
        else
          if (_patternB.FindPattern(img, out currentImagePointsB))
          {
            ExtrinsicCalibration ec_B = null;
            Emgu.CV.ExtrinsicCameraParameters ecp_B = null;
            Matrix extrinsic_matrix = Matrix.Identity(4, 4);
            Matrix temp_matrix = null;
            Emgu.CV.Structure.MCvPoint3D32f[] transformedCornerPoints = new Emgu.CV.Structure.MCvPoint3D32f[_patternA.ObjectPoints.Length];

            try
            {
              ec_B = new ExtrinsicCalibration(_patternB.ObjectPoints, this.IntrinsicParameters);
              ecp_B = ec_B.Calibrate(currentImagePointsB);

              if (ecp_B != null)
              {
                temp_matrix = Parsley.Core.Extensions.ConvertToParsley.ToParsley(ecp_B.ExtrinsicMatrix);
                extrinsic_matrix.SetMatrix(0, temp_matrix.RowCount - 1, 0, temp_matrix.ColumnCount - 1, temp_matrix);

                //transform object points of A into B
                transformedCornerPoints = MatrixTransformation.TransformVectorToEmgu(_transformationBToA.Inverse(), 1.0, _patternA.ObjectPoints).ToArray<Emgu.CV.Structure.MCvPoint3D32f>();

                //project the points to 2D-Points (image points) - transformation B-C and projection
                image_points = Emgu.CV.CameraCalibration.ProjectPoints(transformedCornerPoints, ecp_B, this.IntrinsicParameters);
                //_logger.Info("Pattern found.");
                return true;
              }
              else
              {
                _logger.Warn("Error calculating extrinsic parameters.");
                image_points = null;
                return false;
              }
            }
            catch (Exception e)
            {
              _logger.Warn("Caught Exception: {0}.", e);
              image_points = null;
              return false;
            }

          }
          else
          {
            //_logger.Warn("Error: Pattern not found.");
            image_points = null;
            return false;
          }

      }
      else
      {
        _logger.Warn("Error: Intrinsics are needed to find a Composite Pattern but not available.");
        image_points = null;
        return false;
      }
    }


    /// <summary>
    /// Sets up the current transformation matrix, describing the relation
    /// between patternB coordinate system and patternA coordinate system.
    /// </summary>
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
