using SwarmSimulation.Algorithms.Agents.Foraging;
using SwarmSimulation.Environment;

namespace SwarmSimulation.Algorithms.Foraging.LewyWalk.States
{
    public class LwHarvesting : IState<LwForagingAgent>
    {
        private readonly Resource _resource;
        public LwHarvesting(LwForagingAgent agent, Resource resource)
        {
            _resource = resource;
            OnEnter(agent);
        }

        private void OnEnter(LwForagingAgent agent)
        {
            agent.Target = _resource.Position;
        }

        public void Execute(LwForagingAgent agent)
        {
            if (_resource.IsConsumed)
            {
                agent.UnsuccessfulExplorationAttempts = 0;
                agent.State = new LwExploring(agent);
            }

            if (!agent.HasApproachedTarget(_resource.Position))
            {
                return;
            }
            
            agent.Harvest(_resource);
            if (agent.HasReachedMaxCapacity)
            {
                agent.State = new LwReturningToNest(agent);
            }
            else
            {
                agent.UnsuccessfulExplorationAttempts = 0;
                agent.State = new LwExploring(agent);
            }
        }
    }
}