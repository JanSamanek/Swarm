using SwarmSimulation.Algorithms.Agents;

namespace SwarmSimulation.Algorithms.Foraging.States
{
    public interface IState
    {
        void Execute(ForagingAgent agent);
    }
}