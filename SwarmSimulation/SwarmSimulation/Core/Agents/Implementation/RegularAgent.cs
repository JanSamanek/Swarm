using System.Numerics;
using SwarmSimulation.Core.Agents.Contracts;

namespace SwarmSimulation.Core.Agents.Implementation
{
    public class RegularAgent : Agent, IAgent
    {
        public RegularAgent(int id, Vector2 position, int perceptionRange) 
            : base(id, position, perceptionRange)
        {
        }
    }
}