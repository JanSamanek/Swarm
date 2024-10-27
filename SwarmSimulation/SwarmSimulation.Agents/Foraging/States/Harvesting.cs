using SwarmSimulation.Environment;

namespace SwarmSimulation.Agents.Foraging.States
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
            if (!agent.HasApproachedTarget(_resource.Position)) 
                return;
            
            var harvested = agent.Harvest(_resource);
            if (harvested)
            {
                agent.State = new ReturningToNest(agent);
            }
        }
    }
}