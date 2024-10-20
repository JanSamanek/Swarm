
using SwarmSimulation.Core.Algorithms.Contracts;

namespace SwarmSimulation.Core.Algorithms
{
    public static class AlgorithmFactory
    {
        public static IAlgorithm<TSettings, TInput> Get<TAlgorithm, TSettings, TInput>()
            where TAlgorithm : IAlgorithm<TSettings, TInput>, new()
        {
            return new TAlgorithm();
        }
    }
}