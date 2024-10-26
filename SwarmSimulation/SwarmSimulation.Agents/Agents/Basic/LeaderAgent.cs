using System.Numerics;
using SwarmSimulation.Agents.Agents.Contracts;
using SwarmSimulation.Core.Agents.Implementation;

namespace SwarmSimulation.Agents.Agents.Basic
{
    public class LeaderAgent : Agent, IAgent
    {
        public LeaderAgent(int id, Vector2 position, float perceptionRange)
            : base(id, position, perceptionRange)
        {
            
        }
    }
}