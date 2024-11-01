using System.Linq;
using SwarmSimulation.Algorithms.Agents;
using SwarmSimulation.Algorithms.Foraging.Generators;
using SwarmSimulation.Algorithms.Foraging.LewyWalk;

namespace SwarmSimulation.Algorithms.Foraging.States
{
    public class Exploring : IState
    {
        private static float _lewyParameter;
        private static float _maxFlightLength;
        private static float _lewyScale;
        
        private static int _maxExploringAttempts;
        private int _attempt;
        public Exploring(ForagingAgent agent, int attempt)
        {
            _attempt = attempt;
            OnEnter(agent);
        }
        
        private void OnEnter(ForagingAgent agent)
        {
            var flight = LewyMotionGenerator.GenerateFlight(_lewyParameter, 1, _maxFlightLength, _lewyScale);
            agent.Target = agent.Position + flight;
        }

        public void Execute(ForagingAgent agent)
        {
            var resources = agent.DetectResources().ToList();
            if (resources.Any())
            {
                agent.State = new Harvesting(agent, resources.First());
                return;
            }

            if (_attempt >= _maxExploringAttempts)
            {
                agent.State = new ReturningToNest(agent);
            }
            
            if (agent.HasApproachedTarget(agent.Target) || agent.DetectCollision())
            {
                agent.State = new Exploring(agent, ++_attempt);
            }
        }

        public static void Configure(ForagingLewyWalkAlgorithmSettings settings)
        {
            _maxFlightLength = settings.MaxFlightLength;
            _lewyParameter = settings.LewyParameter;
            _lewyScale = settings.LewyScale;
            _maxExploringAttempts = settings.MaxExploringAttempts;
        }
        
    }
}