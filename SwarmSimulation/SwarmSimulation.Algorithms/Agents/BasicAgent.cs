using System.Numerics;
using SwarmSimulation.Agents;

namespace SwarmSimulation.Algorithms.Agents
{
    public class BasicAgent : AgentCore, IAgent
    {
        public BasicAgent(int id, Vector2 position, float perceptionRange) 
            : base(id, position, perceptionRange)
        {
        }
    }
}