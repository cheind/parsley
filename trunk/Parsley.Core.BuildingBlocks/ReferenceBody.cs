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
using System.ComponentModel;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace Parsley.Core.BuildingBlocks {

  /// <summary>
  /// This class is used to set up the reference body (laboratory setup) for the scanning algorithm.
  /// Left, Right and Ground planes can be defined for the scanning algorithm (finding laser line and extracting points).
  /// The parameter _numberOfReferencePlanes is used to determine which planes should be used only for the scanning process.
  /// As an example: if _numberOfReferencePlanes = 2, left and right plane are used for the scanning algorithm, but
  /// left, right and ground plane are still used to filter wrong detected points (e.g. located beneath a given plane)
  /// </summary>
  [Serializable]
  public class ReferenceBody {
    List<Plane> _planes;
    List<Emgu.CV.ExtrinsicCameraParameters> _ecps;
    int _numberOfReferencePlanes;

    public ReferenceBody() {
      _planes = new List<Plane>();
      _planes.Add(null);
      _planes.Add(null);
      _planes.Add(null);
      _ecps = new List<Emgu.CV.ExtrinsicCameraParameters>();
      _ecps.Add(null);
      _ecps.Add(null);
      _ecps.Add(null);

      _numberOfReferencePlanes = 2;
    }

    /// <summary>
    /// Get the reference planes used for laser line detection and point extraction
    /// </summary>
    [Browsable(false)]
    public IList<Plane> ReferencePlanes {
      get {
        List<Plane> temp = _planes.GetRange(0, _numberOfReferencePlanes); 
        return temp.AsReadOnly(); 
      }
    }

    /// <summary>
    /// Get all defined planes (e.g. for the filtering mechanism)
    /// </summary>
    [Browsable(false)]
    public IList<Plane> AllPlanes
    {
      get { return _planes.AsReadOnly(); }
    }

    [Description("Set the Extrinsics describing the left plane of the reference body.")]
    [Editor(typeof(ExtrinsicTypeEditor),
            typeof(System.Drawing.Design.UITypeEditor))]
    public Emgu.CV.ExtrinsicCameraParameters LeftPlane {
      get {
        return _ecps[0];
      }
      set {
        if (value != null) {
          _ecps[0] = value;
          _planes[0] = new Plane(value);
        }
      }
    }

    [Description("Set the Extrinsics describing the right plane of the reference body.")]
    [Editor(typeof(ExtrinsicTypeEditor),
            typeof(System.Drawing.Design.UITypeEditor))]
    public Emgu.CV.ExtrinsicCameraParameters RightPlane {
      get {
        return _ecps[1];
      }
      set {
        if (value != null) {
          _ecps[1] = value;
          _planes[1] = new Plane(value);
        }
      }
    }

    [Description("Set the Extrinsics describing the ground plane of the reference body.")]
    [Editor(typeof(ExtrinsicTypeEditor),
            typeof(System.Drawing.Design.UITypeEditor))]
    public Emgu.CV.ExtrinsicCameraParameters GroundPlane
    {
      get
      {
        if (_ecps.Count == 2)
        {
          _ecps.Add(null);
          _planes.Add(null);
        }
        return _ecps[2];
      }
      set
      {
        if(value != null)
          _ecps[2] = value;
          _planes[2] = new Plane(value);
      }
    }

    [Description("Set the number of planes which should be used for the laser line detection.")]
    public int NumberOfReferencePlanes
    {
      get
      {
        return _numberOfReferencePlanes;
      }

      set
      {
        if (value >= 2 && value < 4)
        {
          _numberOfReferencePlanes = value;
        }
      }
    }
  }
}
