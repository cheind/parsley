using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Parsley.UI {
  public interface I2DInteractor {
    /// <summary>
    /// Place interactor on target
    /// </summary>
    /// <param name="target"></param>
    void InteractOn(Control target);

    /// <summary>
    /// Release target interaction
    /// </summary>
    void ReleaseInteraction();
  }
}
