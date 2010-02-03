using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Parsley.Core {
  public class SmoothByAverage : SmoothLaserLine {
    private int _window_size;

    public SmoothByAverage(int window_size) {
      _window_size = window_size;
    }

    public override void Smooth(float[] laser_pos) {
      float[] tmp = new float[laser_pos.Length];
      laser_pos.CopyTo(tmp, 0);
      for (int i = 0; i < laser_pos.Length; ++i) {
        laser_pos[i] = Average(i, tmp);
      }
    }

    float Average(int id, float[] positions) {
      int left_id = Math.Max(id - _window_size, 0);
      int right_id = Math.Min(id + _window_size, positions.Length);

      float sum = 0;
      int count = 0;
      for (int i = left_id; i < right_id; ++i) {
        if (positions[i] > 0) {
          sum += positions[i];
          count++;
        }
      }
      if (count > 0) {
        return sum / count;
      } else {
        return positions[id];
      }
    }
  }
}
