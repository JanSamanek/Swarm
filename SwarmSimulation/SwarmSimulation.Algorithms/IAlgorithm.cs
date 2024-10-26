using System.Numerics;
using SwarmSimulation.Agents.Agents.Contracts;

namespace SwarmSimulation.Algorithms
{
    public interface IAlgorithm<in TInput>
    {
        Vector2 CalculateControlInput(IAgent agent, TInput input);
    }
}