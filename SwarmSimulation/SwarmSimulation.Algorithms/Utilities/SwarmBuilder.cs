using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using SwarmSimulation.Algorithms.Agents;
using SwarmSimulation.Environment;

namespace SwarmSimulation.Algorithms.Utilities
{
    public class SwarmBuilder
    {
        private int _perceptionRange;
        private float _agentSize;
        private int _maxResourceCapacity;
        private IEnumerable<Vector2> _positions = Enumerable.Empty<Vector2>();
        private AgentsType _agentsType;
        private readonly List<LeaderAgent> _leaderAgents = new List<LeaderAgent>();

        public SwarmBuilder SetAgentSize(float agentRadius)
        {
            _agentSize = agentRadius;
            return this;
        }

        public SwarmBuilder SetPerceptionRange(int perceptionRange)
        {
            _perceptionRange = perceptionRange;
            return this;
        }

        public SwarmBuilder SetMaxResourceCapacity(int maxResourceCapacity)
        {
            _maxResourceCapacity = maxResourceCapacity;
            return this;
        }

        public SwarmBuilder SetPositions(IEnumerable<Vector2> positions)
        {
            _positions = positions;
            return this;
        }

        public SwarmBuilder SetAgentType(AgentsType type)
        {
            _agentsType = type;
            return this;
        }

        public SwarmBuilder AddLeaderToSwarm(LeaderAgent leader)
        {
            _leaderAgents.Add(leader);
            return this;
        }

        public Swarm BuildInNest(Nest nest, int agentCount)
        {
            var positions = new HashSet<Vector2>();

            while (positions.Count < agentCount)
            {
                var randomPosition = nest.GetRandomPositionInNest();
                positions.Add(randomPosition);
            }

            _positions = positions;
            var swarm = Build();
            return swarm;
        }
        
        public Swarm Build()
        {
            var swarm = new Swarm();
            switch (_agentsType)
            {
                case AgentsType.Basic:
                {
                    foreach (var position in _positions)
                    {
                        swarm.Agents.Add(new BasicAgent(position, _agentSize, _perceptionRange));
                    } 
                    break;
                }
                case AgentsType.Foraging:
                {
                    foreach (var position in _positions)
                    {
                        swarm.Agents.Add(new ForagingAgent(position, _agentSize, _perceptionRange,
                            _maxResourceCapacity));
                    }
                    break;
                }
                default:
                    throw new ArgumentOutOfRangeException();
            }

            foreach (var leader in _leaderAgents)
            {
                swarm.Agents.Add(leader);
            }
            return swarm;
        }
    }
    
}