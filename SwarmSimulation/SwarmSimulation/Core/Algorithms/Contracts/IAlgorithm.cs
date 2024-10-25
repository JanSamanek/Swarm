using System.Numerics;
using SwarmSimulation.Core.Agents.Contracts;

namespace SwarmSimulation.Core.Algorithms.Contracts
{
    public interface IAlgorithm<in TInput>
    {
        Vector2 CalculateControlInput(IAgent agent, TInput input);
    }
}