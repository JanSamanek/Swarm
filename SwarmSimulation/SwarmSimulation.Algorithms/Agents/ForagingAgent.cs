using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Numerics;
using System.Threading.Tasks;
using SwarmSimulation.Algorithms.Foraging.States;
using SwarmSimulation.Environment;

namespace SwarmSimulation.Algorithms.Agents
{
    public class ForagingAgent : AgentCore
    {
        public bool CarriesResource { get; private set; }
        public IState State { get; set; }
        public Vector2 Target { get; set; }
        
        public ForagingAgent(int id, Vector2 position, float size, float perceptionRange) 
            : base(id, position, size, perceptionRange)
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
            var resourcesInRange = new ConcurrentBag<Resource>();
            Parallel.ForEach(Arena.Instance.Resources, resource =>
            {
                var distance = (resource.Position - Position).Length();
                if (distance < PerceptionRange)
                {
                    resourcesInRange.Add(resource);
                }
            });
            return resourcesInRange;
        }
    }
}