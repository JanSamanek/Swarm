using SwarmSimulation.Algorithms.Agents.Foraging;
using SwarmSimulation.Algorithms.Foraging.LewyWalk.States;
using SwarmSimulation.Environment;

namespace SwarmSimulation.Algorithms.Foraging.AdaptiveLewyWalk.States
{
    public class AlwReturningToNest : IState<AlwForagingAgent>
    {
        public AlwReturningToNest(AlwForagingAgent agent)
        {
            OnEnter(agent);
        }
        
        private static void OnEnter(AlwForagingAgent agent)
        {
            agent.Target = Arena.Instance.Nest.GetRandomPositionInNest();
        }

        public void Execute(AlwForagingAgent agent)
        {
            agent.TicksFromLastSuccessfulExploration++;
            if (!agent.HasApproachedTarget(agent.Target)) 
                return;

            var droppedResource = agent.DropResource();
            if (droppedResource)
            {
                agent.UnsuccessfulExplorationAttempts = 0;
                agent.State = new AlwExploring(agent);
            }
        }
    }
}