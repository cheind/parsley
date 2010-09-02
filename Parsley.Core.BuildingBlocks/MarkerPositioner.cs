/*
 * Parsley http://parsley.googlecode.com
 * Copyright (c) 2010, Christoph Heindl
 * Copyright (c) 2010, Matthias Plasch
 * All rights reserved.
 * Code license:	New BSD License
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using MathNet.Numerics.LinearAlgebra;
using System.ComponentModel;
using Parsley.Core.Extensions;
using Emgu.CV.Structure;
using Emgu.CV;
using Emgu.CV.CvEnum;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;
using log4net;

namespace Parsley.Core.BuildingBlocks
{
  [Serializable]
  [Parsley.Core.Addins.Addin]
  public class MarkerPositioner : IPositioner, ISerializable{

    private Emgu.CV.ExtrinsicCameraParameters _ecp_A;
    private Parsley.Core.BuildingBlocks.CompositePattern _cpattern;
    private Matrix _extrinsicMatrix_A;
    private Matrix _final;
    private readonly ILog _logger;
    private double _angle_degrees;

    /// <summary>
    /// Default Constructor
    /// </summary>
    /// <param name="theCam"> Camera Object needed in order to obtain the current camera frame.</param>
    public MarkerPositioner() {
      _final = Matrix.Identity(4, 4);
      _ecp_A = null;
      _cpattern = null;
      _extrinsicMatrix_A = Matrix.Identity(4, 4);
      _logger = LogManager.GetLogger(typeof(MarkerPositioner));
      _angle_degrees = 0;
    }

    public MarkerPositioner(SerializationInfo info, StreamingContext context)
    {
      _ecp_A = (Emgu.CV.ExtrinsicCameraParameters)info.GetValue("ecpA",  typeof(Emgu.CV.ExtrinsicCameraParameters));
      _final = Matrix.Create((double[][])info.GetValue("final", typeof(double[][])));
      _cpattern = (Parsley.Core.BuildingBlocks.CompositePattern)info.GetValue("patternC", typeof(Parsley.Core.CalibrationPattern));

      _logger = LogManager.GetLogger(typeof(MarkerPositioner));
      _extrinsicMatrix_A = ExtractExctrinsicMatrix(_ecp_A);
    }

    public void GetObjectData(SerializationInfo info, StreamingContext context) {
      info.AddValue("ecpA", _ecp_A);
      info.AddValue("final", _final.GetArray());
      info.AddValue("patternC", _cpattern);
    }

    /// <summary>
    /// Transforms 3D-Points from the Camera Coordinate System into the Marker Coordinate System
    /// The transformation matrix "_final" is calculated in "UpdateTransformation"
    /// </summary>
    /// <param name="points"> List of Vectors, containing the 3d points which should be transformed</param>
    public void TransformPoints(List<Vector> points)
    {
      for (int i = 0; i < points.Count; ++i) {
        points[i] = _final.Multiply(points[i].ToHomogeneous(1).ToColumnMatrix()).GetColumnVector(0).ToNonHomogeneous();
      }
    }

    /// <summary>
    /// Extracts the extrinsic matrix from the cameras' extrinsic parameters
    /// The values of the extrinsic matrix are extracted into a 4x4 Matrix.
    /// This function is static, since it is called firstly in the deserialization constructor.
    /// </summary>
    private static Matrix ExtractExctrinsicMatrix(ExtrinsicCameraParameters ecp)
    {
      Matrix extrinsicMatrix = Matrix.Identity(4, 4);
      if (ecp != null)
      {
        for (int i = 0; i < 3; i++)
        {
          for (int j = 0; j < 4; j++)
          {
            extrinsicMatrix[i, j] = ecp.ExtrinsicMatrix[i, j];
          }
        }
      }
      return extrinsicMatrix;
    }

    /// <summary>
    /// Calculates the transformation matrix, which is used to transform the 3d-object points, which were scanned with reference
    /// to the moved marker coordinate system, back to the initial marker system and henceforth back to the camera system.
    /// The camera object is needed in order to gain the current camera frame. Furthermore, the cameras' intrinsics are needed
    /// to perform an extrinsic calibration.
    /// 
    /// The transformation matrix is calculated as follows:
    /// * All scanned points are referenced to the camera coordinate system.
    /// * The extrinsic matrices _extrinsicMatrix_A and _extrinsicMatrix_B describe the transformations between the 
    ///   initial detected marker systems (A and B) and camera system.
    /// * Two maker patterns are used in this implementation. The second marker B is only used, if marker A
    ///   is hidden due to the position of the scanning object. Dependent on whether marker A or marker B is hidden,
    ///   the extrinsicMatrix B or A will be used to calculate the transformation matrix, respectively.
    ///   ==> extrinsicM1 holds the current used extrinsicMatrix of the initial marker position.
    /// * extrinsicM2 describes the transformation between moved marker system and camera system.
    /// * Since the position of the scanned object, relative to the marker does not change, all points in the camera system
    ///   can be represented in a common coordinate system by using the inverse transformation "inv(extrinsicM2)".
    /// * Because the points should be represented in the camera system, they need to be tranformed back into the camera system
    ///   using the transformation "extrinsicM1".
    /// * Therefore the final transformation matrix can be calculated from _final = extrinsicM1 * inv(extrinsicM2)
    /// </summary>
    public bool UpdateTransformation(Camera the_cam)
    {
      Matrix extrinsicM1 = Matrix.Identity(4, 4);
      ExtrinsicCameraParameters ecp_moved = null;
      ExtrinsicCalibration ec_moved = null;
      bool ret_value = true;
      bool foundA = false;
      bool foundB = false;

      if (_ecp_A != null && _cpattern != null)
      {
        Emgu.CV.Image<Gray, Byte> gray_img = the_cam.Frame().Convert<Gray, Byte>();
        System.Drawing.PointF[] currentImagePointsA;
        System.Drawing.PointF[] currentImagePointsB;

        //try to find both marker patterns
        foundA = _cpattern.PatternA.FindPattern(gray_img, out currentImagePointsA);
        foundB = _cpattern.PatternB.FindPattern(gray_img, out currentImagePointsB);

        if (foundA == true)
        {
          // transformation based on pattern A
          ec_moved = new ExtrinsicCalibration(_cpattern.PatternA.ObjectPoints, the_cam.Intrinsics);
          ecp_moved = ec_moved.Calibrate(currentImagePointsA);

          if (ecp_moved != null)
          {
            //extract current extrinsic matrix and use initial extrinsic matrix A
            extrinsicM1 = ExtractExctrinsicMatrix(ecp_moved);
            _logger.Info("UpdateTransformation: Transformation found.");
          }
          else
          {
            _logger.Warn("UpdateTransformation: Extrinsics of moved marker system not found.");
            ret_value = false;
          }
        }
        else if (foundB == true)
          {
            // transformation based on pattern B
            ec_moved = new ExtrinsicCalibration(_cpattern.PatternB.ObjectPoints, the_cam.Intrinsics);
            ecp_moved = ec_moved.Calibrate(currentImagePointsB);

            if (ecp_moved != null)
            {
              //extract current extrinsic matrix and use initial extrinsic matrix B
              extrinsicM1 = ExtractExctrinsicMatrix(ecp_moved) * _cpattern.TransformationMatrixBA;
              _logger.Info("UpdateTransformation: Transformation found.");
            }
            else
            {
              _logger.Warn("UpdateTransformation: Extrinsics of moved marker system not found.");
              ret_value = false;
            }
          }
          else
          {
            // if pattern can't be found: use identity warp matrix
            _logger.Warn("UpdateTransformation: Pattern not found.");
            ret_value = false;
          }

        //now calculate the final transformation matrix
        _final = _extrinsicMatrix_A * extrinsicM1.Inverse();
      }
      else
      {
        ret_value = false;
        _logger.Warn("UpdateTransformation: No Pattern or no Positioner Extrinsics have been chosen.");
      }
      return ret_value;
    }

    /// <summary>
    /// Set, Get the extrinsic parameters using a file dialog.
    /// If set: extract the new extrinsic matrix for pattern A
    /// </summary>
    [Editor(typeof(ExtrinsicTypeEditor),
            typeof(System.Drawing.Design.UITypeEditor))]
    public Emgu.CV.ExtrinsicCameraParameters PositionerPoseA
    {
      get
      {
        return _ecp_A;
      }
      set
      {
        _ecp_A = value;
        _extrinsicMatrix_A = ExtractExctrinsicMatrix(_ecp_A);
      }
    }

    /// <summary>
    /// Sets / Gets the used pattern using a file dialog.
    /// Pattern A is used as the main pattern of the positioner.
    /// If this pattern hides behind the scanning object,
    /// pattern B can be used to find the affine transformation.
    /// </summary>
    [Editor(typeof(PatternTypeEditor),
            typeof(System.Drawing.Design.UITypeEditor))]
    public Parsley.Core.BuildingBlocks.CompositePattern PositionerCompositePattern
    {
      get
      {
        return _cpattern;
      }
      set
      {
        _cpattern = value;
      }
    }

    /// <summary>
    /// Set/Get the angular position of the positioner in degrees unit.
    /// </summary>
    [Browsable(false)]
    public double Angle
    {
      get { return _angle_degrees; }
      set {_angle_degrees = value;}
    }
  }
}
