using System;
using System.Linq;
using System.Numerics;
using SwarmSimulation.Algorithms.Agents;
using SwarmSimulation.Algorithms.Foraging.Generators;
using SwarmSimulation.Utilities.Extensions;

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
            var direction = GenerateNewDirection();
            var length = GetFlightLength();
            agent.Target = agent.Position + direction * length;
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
        private static Vector2 GenerateNewDirection()
        {
            var random = new Random();
            var randomAngle = (float) (random.NextDouble() * 2 * Math.PI);
            return Vector2.Normalize(Vector2.One).Rotate(randomAngle);
        }

        private static float GetFlightLength()
        {
            return (float) LewyRandom.Next(_lewyParameter, 1, _maxFlightLength, _lewyScale);
        }
        public static void ConfigureLewyWalk(float lewyParameter, float maxFlightLength, float scale)
        {
            _maxFlightLength = maxFlightLength;
            _lewyParameter = lewyParameter;
            _lewyScale = scale;
        }
        
    }
}