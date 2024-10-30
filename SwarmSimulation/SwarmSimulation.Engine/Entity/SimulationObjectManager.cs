using System.Collections.Generic;
using System.Linq;
using SwarmSimulation.Engine.Collision;
using SwarmSimulation.Engine.Physics;

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

        public static IEnumerable<Collider> GetColliders()
        {
            return SimulationObjects.Select(s => s.Collider);
        }
        
        public static Collider GetCollider(int objectId) 
        {
            var simulationObject = SimulationObjects.Find(sim => sim.Id == objectId);
            return simulationObject.Collider;
        }
        
        public static RigidBody GetRigidBody(int objectId) 
        {
            var simulationObject = SimulationObjects.Find(sim => sim.Id == objectId);
            return simulationObject.Body;
        }
    }
}