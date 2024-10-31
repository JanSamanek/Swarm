using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using SwarmSimulation.Engine.Collision;

namespace SwarmSimulation.Engine.Entity
{
    public static class SimulationObjectManager
    {
        private static readonly ConcurrentBag<SimulationObject> SimulationObjects =
            new ConcurrentBag<SimulationObject>();
        
        public static void Register(SimulationObject simulationObject)
        {
            if (!SimulationObjects.Contains(simulationObject))
            {
                SimulationObjects.Add(simulationObject);
            }
        }

        public static SimulationObject GetSimulationObject(int objectId)
        {
            return SimulationObjects.FirstOrDefault(simulationObject => simulationObject.ObjectId == objectId);
        }
        
        public static IEnumerable<SimulationObject> GetSimulationObjects()
        {
            return SimulationObjects;
        }
    }
}