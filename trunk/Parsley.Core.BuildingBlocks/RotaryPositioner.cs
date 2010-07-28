using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MathNet.Numerics.LinearAlgebra;
using System.ComponentModel;
using Parsley.Core.Extensions;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Windows.Forms;

namespace Parsley.Core.BuildingBlocks {

  [Serializable]
  public class RotaryPositioner : ISerializable {
    private Matrix _ecp;
    private Matrix _final;
    private double _angle_degrees;
    private string _ecp_filename;

    public RotaryPositioner() {
      _final = Matrix.Identity(4, 4);
      _ecp = Matrix.Identity(4,4);
      _ecp_filename = "not-set";
    }

    public RotaryPositioner(SerializationInfo info, StreamingContext context)
    {
      _ecp = Matrix.Create((double[][])info.GetValue("ecp", typeof(double[][])));
      _final = Matrix.Create((double[][])info.GetValue("final", typeof(double[][])));
      _angle_degrees = (double)info.GetValue("angle", typeof(double));
      _ecp_filename = (string)info.GetValue("filename", typeof(string));
    }

    public void GetObjectData(SerializationInfo info, StreamingContext context) {
      info.AddValue("ecp", _ecp.GetArray());
      info.AddValue("final", _final.GetArray());
      info.AddValue("angle", _angle_degrees);
      info.AddValue("filename", _ecp_filename);
    }

    public double Angle {
      get { return _angle_degrees; }
      set {
        _angle_degrees = value;
        UpdateTransformation();
      }
    }

    private void UpdateTransformation() {
      double angle_rad = _angle_degrees / 180.0 * Math.PI;
      Matrix rz = Matrix.Identity(4, 4);
      rz[0, 0] = Math.Cos(angle_rad);
      rz[0, 1] = Math.Sin(angle_rad);
      rz[1, 0] = -Math.Sin(angle_rad);
      rz[1, 1] = Math.Cos(angle_rad);

      _final = _ecp * rz * _ecp.Inverse();
    }

    [Editor(typeof(ExtrinsicsFileNameEditor),
    typeof(System.Drawing.Design.UITypeEditor))]
    public string RotaryPose {
      get {
        return _ecp_filename;
      }
      set {
        Emgu.CV.ExtrinsicCameraParameters ecp = LoadExtrinsics(value);
        if (ecp != null) {
          _ecp_filename = value;
          _ecp = Matrix.Identity(4, 4);
          for (int i = 0; i < 3; ++i) {
            for (int j = 0; j < 4; ++j) {
              _ecp[i, j] = ecp.ExtrinsicMatrix[i, j];
            }
          }
        }
      }
    }

    public void TransformPoints(List<Vector> points) {
      for (int i = 0; i < points.Count; ++i) {
        points[i] = _final.Multiply(points[i].ToHomogeneous(1).ToColumnMatrix()).GetColumnVector(0).ToNonHomogeneous();
      }
    }


    private Emgu.CV.ExtrinsicCameraParameters LoadExtrinsics(string filename) {
      Emgu.CV.ExtrinsicCameraParameters ecp = null;
      try {
        using (Stream s = File.Open(filename, FileMode.Open)) {
          if (s != null) {
            IFormatter formatter = new BinaryFormatter();
            ecp = formatter.Deserialize(s) as Emgu.CV.ExtrinsicCameraParameters;
            s.Close();
          }
        }
      } catch (Exception) { }
      return ecp;
    }

    internal class ExtrinsicsFileNameEditor : System.Windows.Forms.Design.FileNameEditor {
      protected override void InitializeDialog(OpenFileDialog openFileDialog) {
        base.InitializeDialog(openFileDialog);
        openFileDialog.Filter = "Extrinsic Files|*.ecp";
      }
    }
  }
}
