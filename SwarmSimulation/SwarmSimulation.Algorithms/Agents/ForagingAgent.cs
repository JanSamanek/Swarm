using System.Collections.Generic;
using System.Numerics;
using SwarmSimulation.Algorithms.Foraging.States;
using SwarmSimulation.Environment;

namespace SwarmSimulation.Algorithms.Agents
{
    public class ForagingAgent : Agent
    {
        public bool CarriesResource { get; private set; }
        public IState State { get; set; }
        public Vector2 Target { get; set; }
        
        public ForagingAgent(Vector2 position, float size, float perceptionRange) 
            : base(position, size, perceptionRange)
        {
            State = new Exploring(this);
        }

        public bool Harvest(Resource resource)
        {
            resource.Harvest();
            CarriesResource = true;
            return true;
        }

        public bool DropResource()
        {
            CarriesResource = false;
            return true;
        }
        
        public IEnumerable<Resource> DetectResources()
        {
            var resourcesInRange = new List<Resource>();
            foreach (var resource in Arena.Instance.Resources)
            {
                var distance = Vector2.Distance(resource.Position, Position);
                if (distance < PerceptionRange)
                {
                    resourcesInRange.Add(resource);
                }
            }
            return resourcesInRange;
        }
    }
}