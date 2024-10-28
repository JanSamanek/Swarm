using System;
using System.Linq;
using System.Numerics;
using SwarmSimulation.Utilities.Extensions;
using SwarmSimulation.Utilities.Random;

namespace SwarmSimulation.Agents.Foraging.States
{
    public class Exploring : IState
    {
        private readonly Random _random = new Random();
        private static float _lewyParameter;
        private static float _maxFlightLength;
        private static float _lewyScale;
        
        public Exploring(ForagingAgent agent)
        {
            OnEnter(agent);
        }
        
        // TODO: incorporate settings
        private void OnEnter(ForagingAgent agent)
        {
            var direction = GenerateNewDirection();
            var length = (float) LewyRandom.Next(_lewyParameter, _maxFlightLength, _lewyScale);
            agent.Target = agent.Position + direction * length;
        }

        public void Execute(ForagingAgent agent)
        {
            var resources = agent.DetectResources().ToList();
            if (resources.Any())
            {
                agent.State = new Harvesting(agent, resources.First());
            }
        }

        public static void ConfigureLewyWalk(float lewyParameter, float maxFlightLength, float scale)
        {
            _maxFlightLength = maxFlightLength;
            _lewyParameter = lewyParameter;
            _lewyScale = scale;
        }
        
        private Vector2 GenerateNewDirection()
        {
            var randomAngle = (float) (_random.NextDouble() * 2 * Math.PI);
            var baseVector = new Vector2(0, 1);
            return Vector2.Normalize(baseVector.Rotate(randomAngle));
        }
    }
}