
namespace SwarmSimulation.Core.Algorithms
{
    public static class AlgorithmFactory
    {
        public static IAlgorithm<TSettings> Get<TAlgorithm, TSettings>()
            where TAlgorithm : IAlgorithm<TSettings>, new()
        {
            return new TAlgorithm();
        }
    }
}