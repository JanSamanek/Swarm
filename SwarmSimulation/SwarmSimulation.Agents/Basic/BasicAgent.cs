using System.Numerics;

namespace SwarmSimulation.Agents.Basic
{
    public class BasicAgent : AgentCore, IAgent
    {
        public BasicAgent(int id, Vector2 position, float perceptionRange) 
            : base(id, position, perceptionRange)
        {
        }
    }
}