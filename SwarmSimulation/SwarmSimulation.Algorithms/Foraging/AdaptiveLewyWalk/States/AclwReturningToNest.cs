using System.Linq;
using SwarmSimulation.Algorithms.Agents.Foraging;
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
            var resources = agent.DetectResources().ToList();
            if (resources.Any())
            {
                agent.TicksFromLastSuccessfulExploration = 0;
                agent.State = new AclwHarvesting(agent, resources.First());
                return;
            }
            
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