using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MathNet.Numerics.LinearAlgebra;

namespace Parsley.Core {

  public interface IRansacModel {
    /// <summary>
    /// Return the number of required samples to estimate initial model parameters.
    /// </summary>
    int RequiredSamples {
      get;
    }

    /// <summary>
    /// Build model from initial inlier hypothesis
    /// </summary>
    /// <param name="initial">Samples to fit model to.</param>
    /// <returns>True if all free parameters are set; false if fit failed.</returns>
    bool Build(IEnumerable<Vector> initial);

    /// <summary>
    /// Determine unsigned distance of query point to model.
    /// </summary>
    /// <param name="x">Query point.</param>
    /// <returns>Distance to query point</returns>
    double DistanceTo(Vector x);

    /// <summary>
    /// Re-fit model using the provided consensus set.
    /// </summary>
    /// <param name="consensus_set">Consensus set with at least RequiredSamples samples </param>
    void Fit(IEnumerable<Vector> consensus_set);
  }

  /// <summary>
  /// Additional ransac model construction constraints interface
  /// </summary>
  /// <typeparam name="T">Model to apply constraint to</typeparam>
  public interface IRansacModelConstraint {
    bool Test(IRansacModel model);
  }

  /// <summary>
  /// Implements "Random Sample Consensus" Algorithm to estimate parameters of
  /// a given model from samples with outliers.
  /// </summary>
  public class Ransac<T> where T : IRansacModel, new() {

    /// <summary>
    /// A hypothesis of the Ransac algorithm holding required variables.
    /// </summary>
    public class Hypothesis {
      private T _model;
      private List<int> _consensus_ids;
      private Vector[] _samples;

      public Hypothesis(Vector[] samples) {
        _samples = samples;
        _model = new T();
        _consensus_ids = new List<int>();
      }

      /// <summary>
      /// Access the model
      /// </summary>
      public T Model {
        get { return _model; }
      }

      /// <summary>
      /// Access the consesus set
      /// </summary>
      public IEnumerable<Vector> ConsensusSet {
        get {
          foreach (int id in _consensus_ids) {
            yield return _samples[id];
          }
        }
      }

      /// <summary>
      /// Access the consensus ids
      /// </summary>
      public List<int> ConsensusIds {
        get { return _consensus_ids; }
      }
    }

    private Vector[] _samples;
    private List<Hypothesis> _hyps;
    private Random _r;

    /// <summary>
    /// Initialize Ransac
    /// </summary>
    /// <param name="samples">Samples</param>
    public Ransac(IEnumerable<Vector> samples) {
      _samples = samples.ToArray<Vector>();
      _hyps = new List<Ransac<T>.Hypothesis>();
      _r = new Random();
    }

    /// <summary>
    /// Run Ransac
    /// </summary>
    /// <param name="max_iter">Maximum number of iterations to generate hypothesis</param>
    /// <param name="max_distance">Maximum distance threshold to qualify sample as inlier</param>
    public Hypothesis Run(int max_iter, double max_distance, int min_consensus_size, IRansacModelConstraint constraints) {
      Hypothesis final = null;
      int i = 0;
      while (i < max_iter && final == null) {
        Hypothesis h = new Ransac<T>.Hypothesis(_samples);
        this.BuildHypothesis(h, max_distance, constraints);
        if (h.ConsensusIds.Count >= min_consensus_size) {
          final = h;
        }
        ++i;
      }
      return final;
    }

    /// <summary>
    /// Deals with a singe hypothesis
    /// </summary>
    /// <param name="h">Hypothesis</param>
    /// <param name="max_distance">Maximum distance threshold to qualify sample as inlier</param>
    /// <param name="constraints">Additional constraints</param>
    private void BuildHypothesis(Ransac<T>.Hypothesis h, double max_distance, IRansacModelConstraint constraints) {
      // Initial fit
      T model = h.Model;
      
      if (!model.Build(this.ChooseRandom(model.RequiredSamples))) {
        return;
      }

      if (constraints != null && !constraints.Test(model)) {
        return;
      }

      // Model parameters estimated, determine consensus set
      this.DetermineConsensusSet(h, max_distance);

      // Refit model using regression method and consensus set
      if (h.ConsensusIds.Count >= model.RequiredSamples) {
        model.Fit(h.ConsensusSet);
        h.ConsensusIds.Clear();
        this.DetermineConsensusSet(h, max_distance);
      }
    }

    private IEnumerable<Vector> ChooseRandom(int count) {
      int max_id = _samples.Length - 1;
      for (int i = 0; i < count; ++i) {
        yield return _samples[_r.Next(max_id)];
      }
    }

    private void DetermineConsensusSet(Ransac<T>.Hypothesis h, double max_distance) {
      T model = h.Model;
      for (int i = 0; i < _samples.Length; ++i) {
        double dist = model.DistanceTo(_samples[i]);
        if (dist <= max_distance) {
          h.ConsensusIds.Add(i);
        }
      }
    }


  }
}
