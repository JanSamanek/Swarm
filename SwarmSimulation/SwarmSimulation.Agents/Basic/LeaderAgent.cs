using System.Numerics;

namespace SwarmSimulation.Agents.Basic
{
    public class LeaderAgent : Agent, IAgent
    {
        public LeaderAgent(int id, Vector2 position, float perceptionRange)
            : base(id, position, perceptionRange)
        {
            
        }
    }
}