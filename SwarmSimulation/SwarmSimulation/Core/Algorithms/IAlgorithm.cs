using System.Numerics;

namespace SwarmSimulation.Core.Algorithms
{
    public interface IAlgorithm<in TSettings>
    {
        Vector2 CalculateControlInput(Agent agent, TSettings settings);
    }
}