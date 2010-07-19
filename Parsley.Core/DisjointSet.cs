using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Parsley.Core {

  /// <summary>
  /// Dense disjoint set.
  /// </summary>
  public class DisjointSet {

    public interface IContainer {
      void Initialize(int number_elements);
      void SetRank(int id, int rank);
      int GetRank(int id);
      void SetParent(int id, int parent_id);
      int GetParent(int id);
    };

    public class DenseContainer : IContainer {
      private int[] _elements;
      private int[] _ranks;

      public void Initialize(int number_of_elements) {
        _elements = new int[number_of_elements];
        _ranks = new int[number_of_elements];

        for (int i = 0; i < _elements.Length; ++i) {
          _elements[i] = -1;
        }
      }

      public void SetRank(int id, int rank) {
        _ranks[id] = rank; 
      }

      public int GetRank(int id) {
        return _ranks[id];
      }

      public void SetParent(int id, int parent_id) {
        _elements[id] = parent_id;
      }

      public int GetParent(int id) {
        return _elements[id];
      }
    }

    private IContainer _container;
    private int _count;

    public DisjointSet(DisjointSet.IContainer container) {
      _container = container;
    }

    public void Initialize(int number_of_elements) {
      _container.Initialize(number_of_elements);
      _count = number_of_elements;
    }

    public void MakeSet(int x) {
      _container.SetParent(x, x);
      _container.SetRank(x, 0);
    }

    public int FindRoot(int x) {
      if (x == -1) {
        return -1;
      } else if (_container.GetParent(x) == x) {
        return x;
      } else {
        _container.SetParent(x, FindRoot(_container.GetParent(x)));
        return _container.GetParent(x);
      }
    }

    public void Set(int x, int parent_id) {
      _container.SetParent(x, parent_id);
    }

    public void Merge(int x, int y) {
      int xroot = FindRoot(x);
      int yroot = FindRoot(y);
      int xrank = _container.GetRank(xroot);
      int yrank = _container.GetRank(yroot);

      if (xrank > yrank) {
        _container.SetParent(yroot, xroot);
      } else if (xrank < yrank) {
        _container.SetParent(xroot, yroot);
      } else if (xroot != yroot) {
        _container.SetParent(yroot, xroot);
        _container.SetRank(xroot, xrank + 1);
      }
    }

    public IEnumerable<int> Roots {
      get {
        List<int> indices = new List<int>();
        for (int i = 0; i < _count; ++i) {
          if (_container.GetParent(i) > -1 && FindRoot(i) == i) {
            indices.Add(i);
          }
        }
        return indices;
      }
    }

    public IEnumerable<List<int>> FindSetElements(IEnumerable<int> elements) {
      int num_roots = elements.Count();
      List<int> root_elements = new List<int>(elements.Select(value => { return FindRoot(value); }));

      List<List<int>> results = new List<List<int>>(num_roots);
      for (int i = 0; i < num_roots; ++i) {
        results.Add(new List<int>());
      }

      for (int i = 0; i < _count; ++i) {
        int e = FindRoot(_container.GetParent(i));
        int index = root_elements.IndexOf(e);
        if (index > -1) {
          results[index].Add(i);
        }
      }

      return results;
    }
  }
}
