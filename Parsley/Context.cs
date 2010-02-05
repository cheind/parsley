using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;

namespace Parsley {

  /// <summary>
  /// Provides access to Parsley objects
  /// </summary>
  public class Context {
    private Core.BuildingBlocks.World _world;
    private Core.BuildingBlocks.FrameGrabber _fg;
    private Core.BuildingBlocks.RenderLoop _rl;
    private UI.Concrete.ROIHandler _roi_handler;
    private List<Core.BuildingBlocks.ReferencePlane> _references;

    public event EventHandler<EventArgs> OnConfigurationLoaded;

    public Context(
      Core.BuildingBlocks.World world,
      Core.BuildingBlocks.FrameGrabber fg,
      Core.BuildingBlocks.RenderLoop rl, 
      UI.Concrete.ROIHandler roi_handler) 
    {
      _world = world;
      _fg = fg;
      _rl = rl;
      _roi_handler = roi_handler;
      _references = new List<Parsley.Core.BuildingBlocks.ReferencePlane>();
    }

    public void SaveBinary(string filepath) {
      using (Stream s = File.OpenWrite(filepath)) {
        if (s != null) {
          IFormatter formatter = new BinaryFormatter();
          formatter.Serialize(s, _world);
          s.Close();
        }
      }
    }

    public void LoadBinary(string filepath) {
      using (Stream s = File.Open(filepath, FileMode.Open)) {
        if (s != null) {
          // Make sure the frame grabber is offline and 
          // the old camera is disposed before loading a new one
          // with a potentially equal device_id => conflict
          _fg.Stop();
          _fg.Camera.Dispose();

          IFormatter formatter = new BinaryFormatter();
          Core.BuildingBlocks.World new_world = formatter.Deserialize(s) as Core.BuildingBlocks.World;
          s.Close();

          Core.BuildingBlocks.Camera old_camera = _fg.Camera;
          _fg.Camera = new_world.Camera;
          _world = new_world;
          _fg.Start();

          if (OnConfigurationLoaded != null) {
            OnConfigurationLoaded(this, new EventArgs());
          }

          // Todo: Update aspect ratio of 3d viewer
        }
      }
    }

    /// <summary>
    /// Get/set world components
    /// </summary>
    public Core.BuildingBlocks.World World {
      get { return _world; }
      set { _world = value; }
    }

    /// <summary>
    /// Access the frame grabber
    /// </summary>
    public Core.BuildingBlocks.FrameGrabber FrameGrabber {
      get { return _fg; }
    }


    /// <summary>
    /// Access the render-loop
    /// </summary>
    public Core.BuildingBlocks.RenderLoop RenderLoop {
      get { return _rl; }
    }

    /// <summary>
    /// Access the reference planes
    /// </summary>
    public List<Core.BuildingBlocks.ReferencePlane> ReferencePlanes {
      get { return _references; }
    }

    /// <summary>
    /// Access the viewer
    /// </summary>
    public Draw3D.Viewer Viewer {
      get { return _rl.Viewer; }
    }

    /// <summary>
    /// Access the entity that manages ROIs
    /// </summary>
    public UI.Concrete.ROIHandler ROIHandler {
      get { return _roi_handler; }
    }
  }
}
