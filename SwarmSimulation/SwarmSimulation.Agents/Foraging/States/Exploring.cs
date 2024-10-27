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

        public Exploring(ForagingAgent agent)
        {
            OnEnter(agent);
        }
        
        // TODO: incorporate settings
        private void OnEnter(ForagingAgent agent)
        {
            var direction = GenerateNewDirection();
            var length = (float) LewyRandom.Next(1, 100);
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
        
        private Vector2 GenerateNewDirection()
        {
            var randomAngle = (float) (_random.NextDouble() * 2 * Math.PI);
            var baseVector = new Vector2(0, 1);
            return Vector2.Normalize(baseVector.Rotate(randomAngle));
        }
    }
}