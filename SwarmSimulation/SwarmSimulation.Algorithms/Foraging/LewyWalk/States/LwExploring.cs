using System.Linq;
using SwarmSimulation.Algorithms.Agents.Foraging;
using SwarmSimulation.Algorithms.Utilities.Generator;

namespace SwarmSimulation.Algorithms.Foraging.LewyWalk.States
{
    public class LwExploring : IState<LwForagingAgent>
    {
        private static float _lewyParameter;
        private static float _maxFlightLength;
        private static float _lewyScale;
        
        private static int _maxExplorationAttempts;
        
        public LwExploring(LwForagingAgent agent)
        {
            OnEnter(agent);
        }
        
        private void OnEnter(LwForagingAgent agent)
        {
            var flight = LewyMotionGenerator.GenerateFlight(_lewyParameter, 1, _maxFlightLength, _lewyScale);
            agent.Target = agent.Position + flight;
        }

        public void Execute(LwForagingAgent agent)
        {
            var resources = agent.DetectResources().ToList();
            if (resources.Any())
            {
                agent.State = new LwHarvesting(agent, resources.First());
                return;
            }

            if (agent.UnsuccessfulExplorationAttempts >= _maxExplorationAttempts)
            {
                agent.State = new LwReturningToNest(agent);
            }
            
            if (agent.HasApproachedTarget(agent.Target) || agent.DetectCollision())
            {
                agent.UnsuccessfulExplorationAttempts++;
                agent.State = new LwExploring(agent);
            }
        }

        public static void Configure(LwForagingAlgorithmSettings settings)
        {
            _maxFlightLength = settings.MaxFlightLength;
            _lewyParameter = settings.LewyParameter;
            _lewyScale = settings.LewyScale;
            _maxExplorationAttempts = settings.MaxExploringAttempts;
        }
        
    }
}