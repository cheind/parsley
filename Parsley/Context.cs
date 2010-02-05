using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
