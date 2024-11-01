using System.Linq;
using SwarmSimulation.Algorithms.Agents;
using SwarmSimulation.Algorithms.Foraging.Generators;

namespace SwarmSimulation.Algorithms.Foraging.States
{
    public class Exploring : IState
    {
        private static float _lewyParameter;
        private static float _maxFlightLength;
        private static float _lewyScale;
        
        public Exploring(ForagingAgent agent)
        {
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

            if (agent.HasApproachedTarget(agent.Target) || agent.DetectCollision())
            {
                agent.State = new Exploring(agent);
            }
        }

        public static void ConfigureLewyWalk(float lewyParameter, float maxFlightLength, float scale)
        {
            _maxFlightLength = maxFlightLength;
            _lewyParameter = lewyParameter;
            _lewyScale = scale;
        }
        
    }
}