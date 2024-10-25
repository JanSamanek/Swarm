using System.Numerics;
using SwarmSimulation.Core.Agents.Contracts;

namespace SwarmSimulation.Core.Agents.Implementation
{
    public class LeaderAgent : Agent, IAgent
    {
        public LeaderAgent(int id, Vector2 position, float perceptionRange)
            : base(id, position, perceptionRange)
        {
            
        }
    }
}