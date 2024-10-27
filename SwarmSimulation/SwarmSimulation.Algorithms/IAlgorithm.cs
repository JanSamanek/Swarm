using System.Numerics;
using SwarmSimulation.Agents;

namespace SwarmSimulation.Algorithms
{
    public interface IAlgorithm<in TInput>
    {
        Vector2 CalculateControlInput(IAgent agent, TInput input);
    }
}