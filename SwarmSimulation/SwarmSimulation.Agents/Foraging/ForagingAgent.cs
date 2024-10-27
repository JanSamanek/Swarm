using System.Collections.Generic;
using System.Numerics;
using System.Threading.Tasks;
using SwarmSimulation.Agents.Foraging.States;
using SwarmSimulation.Environment;

namespace SwarmSimulation.Agents.Foraging
{
    public class ForagingAgent : Agent, IAgent
    {
        public bool CarriesResource { get; set; }
        public IState State { get; set; }
        public Vector2 Target { get; set; }
        
        protected ForagingAgent(int id, Vector2 position, float perceptionRange) 
            : base(id, position, perceptionRange)
        {
            State = new Exploring(this);
        }

        public bool Harvest(Resource resource)
        {
            resource.Capacity -= 1;
            CarriesResource = true;
            return true;
            // TODO: Implement async wait
        }

        public bool DropResource()
        {
            CarriesResource = false;
            return true;
            // TODO: Implement async wait
        }
        
        public IEnumerable<Resource> DetectResources()
        {
            var resourcesInRange = new List<Resource>();
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

        public bool HasApproachedTarget(Vector2 target, float tolerance=5)
        {
            return Vector2.Distance(Position, target) <= tolerance;
        }
    }
}