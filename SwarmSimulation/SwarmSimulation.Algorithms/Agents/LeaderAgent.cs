using System.Numerics;

namespace SwarmSimulation.Algorithms.Agents
{
    public class LeaderAgent : AgentCore
    {
        public LeaderAgent(int id, Vector2 position, float size, float perceptionRange)
            : base(id, position, size, perceptionRange)
        {
            
        }
    }
}