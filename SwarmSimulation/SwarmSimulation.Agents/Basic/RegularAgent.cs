using System.Numerics;

namespace SwarmSimulation.Agents.Basic
{
    public class RegularAgent : Agent, IAgent
    {
        public RegularAgent(int id, Vector2 position, float perceptionRange) 
            : base(id, position, perceptionRange)
        {
        }
    }
}