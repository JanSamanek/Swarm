using System.Collections.Generic;
using System.Numerics;
using SwarmSimulation.Agents.Agents.Basic;
using SwarmSimulation.Agents.Agents.Contracts;
using SwarmSimulation.Core.Agents.Implementation.Basic;

namespace SwarmSimulation.Agents
{
    public class Swarm
    {
        public List<IAgent> Agents { get; } = new List<IAgent>();
        private int AgentCounter { get; set; }

        public RegularAgent AddAgent(Vector2 position, float perceptionRange)
        {
            var regular = new RegularAgent(AgentCounter++, position, perceptionRange);
            Agents.Add(regular);
            return regular;
        }
        
        public IEnumerable<RegularAgent> AddAgents(IEnumerable<Vector2> positions, float perceptionRange)
        {
            var regularAgents = new List<RegularAgent>();
            foreach (var position in positions)
            {
                var regular = AddAgent(position, perceptionRange);
                regularAgents.Add(regular);
            }
            return regularAgents;
        }
        public LeaderAgent AddLeader(Vector2 position, float perceptionRange)
        {
            var leader = new LeaderAgent(AgentCounter++, position, perceptionRange);
            Agents.Add(leader);
            return leader;
        }
    }
}