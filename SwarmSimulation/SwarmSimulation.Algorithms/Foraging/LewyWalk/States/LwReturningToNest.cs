using SwarmSimulation.Algorithms.Agents.Foraging;
using SwarmSimulation.Environment;

namespace SwarmSimulation.Algorithms.Foraging.LewyWalk.States
{
    public class LwReturningToNest : IState<LwForagingAgent>
    {
        public LwReturningToNest(LwForagingAgent agent)
        {
            OnEnter(agent);
        }
        
        private static void OnEnter(LwForagingAgent agent)
        {
            agent.Target = Arena.Instance.Nest.GetRandomPositionInNest();
        }

        public void Execute(LwForagingAgent agent)
        {
            if (!agent.HasApproachedTarget(agent.Target)) 
                return;

            var droppedResource = agent.DropResource();
            if (droppedResource)
            {
                agent.UnsuccessfulExplorationAttempts = 0;
                agent.State = new LwExploring(agent);
            }
        }
    }
}