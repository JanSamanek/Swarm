using System.Numerics;

namespace SwarmSimulation.Algorithms.Agents
{
    public class BasicAgent : Agent
    {
        public BasicAgent(Vector2 position, float size, float perceptionRange) 
            : base(position, size, perceptionRange)
        {
        }
    }
}