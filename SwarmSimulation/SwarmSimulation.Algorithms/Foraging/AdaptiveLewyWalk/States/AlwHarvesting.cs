using SwarmSimulation.Algorithms.Agents.Foraging;
using SwarmSimulation.Algorithms.Foraging.LewyWalk.States;
using SwarmSimulation.Environment;

namespace SwarmSimulation.Algorithms.Foraging.AdaptiveLewyWalk.States
{
    public class AlwHarvesting : IState<AlwForagingAgent>
    {
        private readonly Resource _resource;
        public AlwHarvesting(AlwForagingAgent agent, Resource resource)
        {
            _resource = resource;
            OnEnter(agent);
        }

        private void OnEnter(AlwForagingAgent agent)
        {
            agent.Target = _resource.Position;
        }

        public void Execute(AlwForagingAgent agent)
        {
            agent.TicksFromLastSuccessfulExploration = 0;
            if (_resource.IsConsumed)
            {
                agent.UnsuccessfulExplorationAttempts = 0;
                agent.State = new AlwExploring(agent);
            }

            if (!agent.HasApproachedTarget(_resource.Position))
            {
                return;
            }
            
            agent.Harvest(_resource);
            if (agent.HasReachedMaxCapacity)
            {
                agent.UnsuccessfulExplorationAttempts = 0;
                agent.State = new AlwReturningToNest(agent);
            }
            else
            {
                agent.UnsuccessfulExplorationAttempts = 0;
                agent.State = new AlwExploring(agent);
            }
        }
    }
}