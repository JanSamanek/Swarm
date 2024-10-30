using System.Collections.Generic;
using System.Linq;
using SwarmSimulation.Engine.Collision;

namespace SwarmSimulation.Engine.Entity
{
    public static class SimulationObjectManager
    {
        public static readonly List<SimulationObject> SimulationObjects = new List<SimulationObject>();
        
        public static void Register(SimulationObject simulationObject)
        {
            if (!SimulationObjects.Contains(simulationObject))
            {
                SimulationObjects.Add(simulationObject);
            }
        }
        
        public static void Unregister(SimulationObject simulationObject)
        {
            if (SimulationObjects.Contains(simulationObject))
            {
                SimulationObjects.Remove(simulationObject);
            }
        }

        public static SimulationObject GetSimulationObject(int objectId)
        {
            return SimulationObjects.FirstOrDefault(simulationObject => simulationObject.ObjectId == objectId);
        }
        public static IEnumerable<Collider> GetColliders()
        {
            return SimulationObjects.Select(s => s.Collider);
        }
        
        public static Collider GetCollider(int objectId) 
        {
            var simulationObject = SimulationObjects.FirstOrDefault(sim => sim.ObjectId == objectId);
            return simulationObject?.Collider;
        }
    }
}