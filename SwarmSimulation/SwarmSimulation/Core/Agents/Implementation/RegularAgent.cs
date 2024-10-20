using System.Numerics;

namespace SwarmSimulation.Core.Agents.Implementation
{
    public class RegularAgent : Agent
    {
        public RegularAgent(int id, Vector2 position, int perceptionRange) 
            : base(id, position, perceptionRange)
        {
        }
    }
}