using SwarmSimulation.Algorithms.Agents.Foraging;
using SwarmSimulation.Algorithms.Foraging.LewyWalk.States;
using SwarmSimulation.Environment;

namespace SwarmSimulation.Algorithms.Foraging.AdaptiveLewyWalk.States
{
    public class AclwHarvesting : IState<AclwForagingAgent>
    {
        private readonly Resource _resource;
        public AclwHarvesting(AclwForagingAgent agent, Resource resource)
        {
            _resource = resource;
            OnEnter(agent);
        }

        private void OnEnter(AclwForagingAgent agent)
        {
            agent.IsPerformingLongFlight = false;
            agent.Target = _resource.Position;
        }

        public void Execute(AclwForagingAgent agent)
        {
            agent.TicksFromLastSuccessfulExploration = 0;
            if (_resource.IsConsumed)
            {
                agent.UnsuccessfulExplorationAttempts = 0;
                agent.State = new AclwExploring(agent);
            }

            if (!agent.HasApproachedTarget(_resource.Position))
            {
                return;
            }
            
            agent.Harvest(_resource);
            if (agent.HasReachedMaxCapacity)
            {
                agent.UnsuccessfulExplorationAttempts = 0;
                agent.State = new AclwReturningToNest(agent);
            }
            else
            {
                agent.UnsuccessfulExplorationAttempts = 0;
                agent.State = new AclwExploring(agent);
            }
        }
    }
}