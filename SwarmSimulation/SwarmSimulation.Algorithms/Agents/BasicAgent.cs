using System.Numerics;

namespace SwarmSimulation.Algorithms.Agents
{
    public class BasicAgent : Agent
    {
        public BasicAgent(int id, Vector2 position, float size, float perceptionRange) 
            : base(id, position, size, perceptionRange)
        {
        }
    }
}