using System.Numerics;

namespace SwarmSimulation.Algorithms.Agents.Formation
{
    public class BasicAgent : Agent
    {
        public BasicAgent(Vector2 position, float size, float perceptionRange) 
            : base(position, size, perceptionRange)
        {
        }
    }
}