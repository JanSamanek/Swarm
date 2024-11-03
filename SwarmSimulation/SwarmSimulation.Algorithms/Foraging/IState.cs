using SwarmSimulation.Algorithms.Agents;
using SwarmSimulation.Algorithms.Agents.Foraging;

namespace SwarmSimulation.Algorithms.Foraging
{
    public interface IState<in TAgent>
    where TAgent : ForagingAgent
    {
        void Execute(TAgent agent);
    }
}