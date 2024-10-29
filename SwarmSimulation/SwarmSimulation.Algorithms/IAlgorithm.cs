using System.Numerics;
using SwarmSimulation.Algorithms.Agents;

namespace SwarmSimulation.Algorithms
{
    public interface IAlgorithm<in TInput>
    {
        Vector2 CalculateControlInput(IAgent agent, TInput input);
    }
}