using SwarmSimulation.Algorithms.Agents.Foraging;
using SwarmSimulation.Algorithms.Foraging.LewyWalk.States;
using SwarmSimulation.Environment;

namespace SwarmSimulation.Algorithms.Foraging.AdaptiveLewyWalk.States
{
    public class AclwReturningToNest : IState<AclwForagingAgent>
    {
        public AclwReturningToNest(AclwForagingAgent agent)
        {
            OnEnter(agent);
        }
        
        private static void OnEnter(AclwForagingAgent agent)
        {
            agent.IsPerformingLongFlight = false;
            agent.Target = Arena.Instance.Nest.GetRandomPositionInNest();
        }

        public void Execute(AclwForagingAgent agent)
        {
            agent.TicksFromLastSuccessfulExploration++;
            if (!agent.HasApproachedTarget(agent.Target)) 
                return;

            var droppedResource = agent.DropResource();
            if (droppedResource)
            {
                agent.UnsuccessfulExplorationAttempts = 0;
                agent.State = new AclwExploring(agent);
            }
        }
    }
}