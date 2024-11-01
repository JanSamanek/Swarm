using SwarmSimulation.Algorithms.Agents;
using SwarmSimulation.Environment;

namespace SwarmSimulation.Algorithms.Foraging.States
{
    public class Harvesting : IState
    {
        private readonly Resource _resource;
        public Harvesting(ForagingAgent agent, Resource resource)
        {
            _resource = resource;
            OnEnter(agent);
        }

        private void OnEnter(ForagingAgent agent)
        {
            agent.Target = _resource.Position;
        }

        public void Execute(ForagingAgent agent)
        {
            if (_resource.IsConsumed)
            {
                agent.State = new Exploring(agent);
            }

            if (!agent.HasApproachedTarget(_resource.Position))
            {
                return;
            }
            
            agent.Harvest(_resource);
            if (agent.HasReachedMaxCapacity)
            {
                agent.State = new ReturningToNest(agent);
            }
            else
            {
                agent.State = new Exploring(agent);
            }
        }
    }
}