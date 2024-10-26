using System.Numerics;
using SwarmSimulation.Core.Agents.Contracts;
using SwarmSimulation.Environment;

namespace SwarmSimulation.Core.Agents.Implementation
{
    public class ForagingAgent : Agent, IAgent
    {
        public bool CarriesResource { get; set; }
        
        protected ForagingAgent(int id, Vector2 position, float perceptionRange) 
            : base(id, position, perceptionRange)
        {
        }

        public void Harvest(Resource resource)
        {
            resource.Capacity -= 1;
            CarriesResource = true;
            // TODO: Implement async wait
        }
    }
}