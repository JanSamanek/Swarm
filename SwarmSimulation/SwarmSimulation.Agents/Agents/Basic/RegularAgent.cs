using System.Numerics;
using SwarmSimulation.Agents.Agents;
using SwarmSimulation.Agents.Agents.Contracts;

namespace SwarmSimulation.Core.Agents.Implementation.Basic
{
    public class RegularAgent : Agent, IAgent
    {
        public RegularAgent(int id, Vector2 position, float perceptionRange) 
            : base(id, position, perceptionRange)
        {
        }
    }
}