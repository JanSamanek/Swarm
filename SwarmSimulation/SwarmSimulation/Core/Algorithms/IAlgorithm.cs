using System.Numerics;

namespace SwarmSimulation.Core.Algorithms
{
    public interface IAlgorithm<TSettings, in TInput>
    {
        TSettings Settings { get; set; }
        void ConfigureSettings(TSettings settings);
        Vector2 CalculateControlInput(Agent agent, TInput input);
    }
}