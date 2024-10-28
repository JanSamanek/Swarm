using System.Collections.Generic;
using SwarmSimulation.Agents;

namespace SwarmSimulation.Algorithms.Agents
{
    public class Swarm
    {
        public List<IAgent> Agents { get; } = new List<IAgent>();
    }
}