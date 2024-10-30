using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using SwarmSimulation.Engine.Entity;

namespace SwarmSimulation.Engine
{
    public static class CollisionEngine
    {
        private static readonly Dictionary<int, Vector2> CollisionVelocity = new Dictionary<int, Vector2>();
        public static void Update()
        {
            CollisionVelocity.Clear();
            var colliders = SimulationObjectManager.GetColliders().ToList();
            foreach (var collider in colliders)
            {
                var objectId = collider.ObjectId;
                var simulationObject = SimulationObjectManager.GetSimulationObject(objectId);
                
                var collidedWith = collider.UpdateCollisions(colliders);
                
                var collisionVelocity = Vector2.Zero;
                foreach (var other in collidedWith)
                {
                    var direction = Vector2.Normalize(collider.Position - other.Position);
                    
                    collisionVelocity += direction * simulationObject.ControlInput.Length();
                }
                CollisionVelocity.Add(objectId, collisionVelocity);
            }
        }

        public static Vector2 GetCollisionVelocity(int objectId)
        {
            return CollisionVelocity[objectId];
        }
    }
}