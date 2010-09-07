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
  public class MarkerPositioner : ISerializable{

    private Emgu.CV.ExtrinsicCameraParameters _ecp_A;
    private Parsley.Core.CalibrationPatterns.CompositePattern _cpattern;
    private Matrix _extrinsicMatrix_A;
    private Matrix _final;
    private readonly ILog _logger;
    private bool _firstCallUpdateTransformation;

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
      _firstCallUpdateTransformation = true;
    }

    public MarkerPositioner(SerializationInfo info, StreamingContext context)
    {
      _final = Matrix.Create((double[][])info.GetValue("final", typeof(double[][])));
      _cpattern = (Parsley.Core.CalibrationPatterns.CompositePattern)info.GetValue("patternC", typeof(Parsley.Core.CalibrationPattern));

      _logger = LogManager.GetLogger(typeof(MarkerPositioner));
      _firstCallUpdateTransformation = true;
    }

    public void GetObjectData(SerializationInfo info, StreamingContext context) {
      info.AddValue("final", _final.GetArray());
      info.AddValue("patternC", _cpattern);
    }

    /// <summary>
    /// Transforms 3D-Points from the Camera Coordinate System into the Marker Coordinate System
    /// The transformation matrix "_final" is calculated in "UpdateTransformation"
    /// </summary>
    /// <param name="points"> List of Vectors, containing the 3d points which should be transformed</param>
    public void TransformPoints(ref List<Vector> points)
    {
      points = MatrixTransformation.TransformVectorToVector(_final, 1.0, points).ToList<Vector>();
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
    /// to perform an extrinsic calibration. Note, that every kind of pattern can be used.
    /// 
    /// The transformation matrix is calculated as follows:
    /// * If 'UpdateTransformation' is being called for the first time, an extrinsic calibration is performed in order to find
    ///   the initial orientation of the used pattern.
    /// * If the initial orientation has already been found, the extrinsic calibration is performed again. Afterwards
    ///   the current orientation is available, represented by the extrinsic matrix.
    /// * Form the extrinsic matrix (4x3) (current position) to a homogeneous 4x4 matrix.
    /// * The final transformation matrix is calculated as follows: _final = initial * current.Inverse();
    /// </summary>
    public bool UpdateTransformation(Camera the_cam)
    {
      Matrix extrinsicM1 = Matrix.Identity(4, 4);
      ExtrinsicCameraParameters ecp_pattern = null;
      ExtrinsicCalibration ec_pattern = null;
      Emgu.CV.Image<Gray, Byte> gray_img = null;
      System.Drawing.PointF[] currentImagePoints;

      //first call: calculate extrinsics for initial position
      if (_firstCallUpdateTransformation == true && _cpattern != null)
      {
         gray_img = the_cam.Frame().Convert<Gray, Byte>();
         //set the patterns property: intrinsic parameters
         _cpattern.IntrinsicParameters = the_cam.Intrinsics;

         if (_cpattern.FindPattern(gray_img, out currentImagePoints))
         {
           try
           {
             //extr. calibration (initial position)
             ec_pattern = new ExtrinsicCalibration(_cpattern.ObjectPoints, the_cam.Intrinsics);
             ecp_pattern = ec_pattern.Calibrate(currentImagePoints);

             if (ecp_pattern != null)
             {
               _ecp_A = ecp_pattern;
               _extrinsicMatrix_A = ExtractExctrinsicMatrix(_ecp_A);

               _logger.Info("Initial Position found.");
               _firstCallUpdateTransformation = false;
             }
           }
           catch (Exception e)
           {
             _logger.Warn("Initial Position - Caught Exception: {0}.", e);
             _firstCallUpdateTransformation = true;
             _ecp_A = null;
             return false; 
           }
         }
         else
         {
           _logger.Warn("Pattern not found.");
           _firstCallUpdateTransformation = true;
           _ecp_A = null;

           return false; 
         }
      }

      //if initial position and pattern are available - calculate the transformation
      if (_ecp_A != null && _cpattern != null)
      {
        gray_img = the_cam.Frame().Convert<Gray, Byte>();

        //try to find composite pattern
        if (_cpattern.FindPattern(gray_img, out currentImagePoints))
        {
          //extrinsic calibration in order to find the current orientation
          ec_pattern = new ExtrinsicCalibration(_cpattern.ObjectPoints, the_cam.Intrinsics);
          ecp_pattern = ec_pattern.Calibrate(currentImagePoints);

          if (ecp_pattern != null)
          {
            //extract current extrinsic matrix
            extrinsicM1 = ExtractExctrinsicMatrix(ecp_pattern);
            _logger.Info("UpdateTransformation: Transformation found.");
          }
          else
          {
            _logger.Warn("UpdateTransformation: Extrinsics of moved marker system not found.");
            return false;
          }
        }
        else
        {
          _logger.Warn("UpdateTransformation: Pattern not found.");
          return false;
        }

        //now calculate the final transformation matrix
        _final = _extrinsicMatrix_A * extrinsicM1.Inverse();
        return true;
      }
      else
      {
        _logger.Warn("UpdateTransformation: No Pattern has been chosen.");
        return false;
      }
    }

    /// <summary>
    /// Sets / Gets the used pattern using a file dialog.
    /// Pattern A is used as the main pattern of the positioner.
    /// If this pattern hides behind the scanning object,
    /// pattern B can be used to find the transformation.
    /// </summary>
    [Description("Set the positioners' Composite Pattern.")]
    [Editor(typeof(Parsley.Core.CalibrationPatterns.PatternTypeEditor),
            typeof(System.Drawing.Design.UITypeEditor))]
    public Parsley.Core.CalibrationPatterns.CompositePattern PositionerCompositePattern
    {
      get
      {
        return _cpattern;
      }
      set
      {
        _cpattern = value;
        _firstCallUpdateTransformation = true;
      }
    }
  }
}
