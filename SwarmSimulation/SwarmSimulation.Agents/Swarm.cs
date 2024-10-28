using System.Collections.Generic;

namespace SwarmSimulation.Agents
{
    public class Swarm
    {
        public List<IAgent> Agents { get; } = new List<IAgent>();
    }
}