using System;
using System.Collections.Generic;
using System.Numerics;
using SwarmSimulation.Environment;

namespace SwarmSimulation.Agents
{
    public static class SwarmBuilder
    {
        public static Swarm CreateSwarmInNest<TAgent>(Nest nest, int agentCount, int perceptionRange) where TAgent : IAgent
        {
            var positions = new HashSet<Vector2>();

            while (positions.Count < agentCount)
            {
                var randomPosition = nest.GetRandomPositionInNest();
                positions.Add(randomPosition);
            }
            
            return CreateSwarm<TAgent>(positions, perceptionRange);
        }
        
        public static TAgent AddAgent<TAgent>(Swarm swarm, Vector2 position, float perceptionRange)
            where TAgent : IAgent
        {
            var agent = (TAgent) Activator.CreateInstance(typeof(TAgent), swarm.Agents.Count, position, perceptionRange);
            swarm.Agents.Add(agent);
            return agent;
        }

        public static IEnumerable<TAgent> AddAgents<TAgent>(Swarm swarm, IEnumerable<Vector2> positions, float perceptionRange)
            where TAgent : IAgent
        {
            var agents = new List<TAgent>();
            foreach (var position in positions)
            {
                var agent = AddAgent<TAgent>(swarm, position, perceptionRange);
                agents.Add(agent);
            }
            return agents;
        }

        public static Swarm CreateSwarm<TAgent>(Vector2 agentPosition, float perceptionRange)
            where TAgent : IAgent
        {
            return CreateSwarm<TAgent>(new[] { agentPosition }, perceptionRange);
        }
        
        public static Swarm CreateSwarm<TAgent>(IEnumerable<Vector2> positions, float perceptionRange)
            where TAgent : IAgent
        {
            var swarm = new Swarm();
            foreach (var position in positions)
            {
                AddAgent<TAgent>(swarm, position, perceptionRange);
            }
            return swarm;
        }
    }
    
}