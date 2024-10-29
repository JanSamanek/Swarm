using System;
using System.Collections.Generic;
using System.Numerics;
using SwarmSimulation.Algorithms.Agents;
using SwarmSimulation.Environment;

namespace SwarmSimulation.Algorithms.Utilities
{
    public static class SwarmBuilder
    {
        public static Swarm CreateSwarmInNest<TAgent>(Nest nest, int agentCount, float agentSize, int perceptionRange) where TAgent : Agent
        {
            var positions = new HashSet<Vector2>();

            while (positions.Count < agentCount)
            {
                var randomPosition = nest.GetRandomPositionInNest();
                positions.Add(randomPosition);
            }
            
            return CreateSwarm<TAgent>(positions, agentSize, perceptionRange);
        }
        
        public static TAgent AddAgent<TAgent>(Swarm swarm, Vector2 position, float agentSize, float perceptionRange)
            where TAgent : Agent
        {
            var agent = (TAgent) Activator.CreateInstance(typeof(TAgent), swarm.Agents.Count, position, agentSize, perceptionRange);
            swarm.Agents.Add(agent);
            return agent;
        }

        public static IEnumerable<TAgent> AddAgents<TAgent>(Swarm swarm, IEnumerable<Vector2> positions,float agentSize, float perceptionRange)
            where TAgent : Agent
        {
            var agents = new List<TAgent>();
            foreach (var position in positions)
            {
                var agent = AddAgent<TAgent>(swarm, position, agentSize, perceptionRange);
                agents.Add(agent);
            }
            return agents;
        }

        public static Swarm CreateSwarm<TAgent>(Vector2 agentPosition, float agentSize, float perceptionRange)
            where TAgent : Agent
        {
            return CreateSwarm<TAgent>(new[] { agentPosition }, agentSize, perceptionRange);
        }
        
        public static Swarm CreateSwarm<TAgent>(IEnumerable<Vector2> positions, float agentSize, float perceptionRange)
            where TAgent : Agent
        {
            var swarm = new Swarm();
            foreach (var position in positions)
            {
                AddAgent<TAgent>(swarm, position, agentSize, perceptionRange);
            }
            return swarm;
        }
    }
    
}