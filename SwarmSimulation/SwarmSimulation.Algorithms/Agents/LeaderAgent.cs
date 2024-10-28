using System.Numerics;
using SwarmSimulation.Agents;

namespace SwarmSimulation.Algorithms.Agents
{
    public class LeaderAgent : AgentCore, IAgent
    {
        public LeaderAgent(int id, Vector2 position, float perceptionRange)
            : base(id, position, perceptionRange)
        {
            
        }
    }
}