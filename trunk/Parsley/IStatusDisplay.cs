using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Parsley {
  public enum Status {
    Ok,
    Error
  };

  public interface IStatusDisplay {
    void UpdateStatus(string message, Status status);
  }
}
