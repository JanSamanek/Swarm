using SwarmSimulation.Algorithms.Agents;
using SwarmSimulation.Environment;

namespace SwarmSimulation.Algorithms.Foraging.States
{
    public class ReturningToNest : IState
    {
        public ReturningToNest(ForagingAgent agent)
        {
            OnEnter(agent);
        }
        
        private static void OnEnter(ForagingAgent agent)
        {
            agent.Target = Arena.Instance.Nest.GetRandomPositionInNest();
        }

        public void Execute(ForagingAgent agent)
        {
            if (!agent.HasApproachedTarget(agent.Target)) 
                return;

            var droppedResource = agent.DropResource();
            if (droppedResource)
            {
                agent.State = new Exploring(agent);
            }
        }
    }
}