using System.Collections.Generic;
using System.Numerics;
using SwarmSimulation.Algorithms.Foraging.States;
using SwarmSimulation.Environment;

namespace SwarmSimulation.Algorithms.Agents
{
    public class ForagingAgent : Agent
    {
        // TODO: refactor swarm builder to enable this as constructor input
        private readonly int _maxResourceCapacity;
        private int _resourceCapacity;
        public bool CarriesResource { get; private set; }
        public bool HasReachedMaxCapacity { get; private set; }
        public IState State { get; set; }
        public Vector2 Target { get; set; }
        
        public ForagingAgent(Vector2 position, float size, float perceptionRange) 
            : base(position, size, perceptionRange)
        {
            _maxResourceCapacity = 5;
            State = new Exploring(this, 0);
        }

        public bool Harvest(Resource resource)
        {
            var capacityLeft = _maxResourceCapacity - _resourceCapacity;
            _resourceCapacity += resource.Harvest(capacityLeft);
            CarriesResource = true;
            if (_resourceCapacity == _maxResourceCapacity)
            {
                HasReachedMaxCapacity = true;
            }
            return true;
        }

        public bool DropResource()
        {
            _resourceCapacity = 0;
            CarriesResource = false;
            HasReachedMaxCapacity = false;
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