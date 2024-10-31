using System.Linq;
using System.Numerics;
using SwarmSimulation.Engine.Entity;

namespace SwarmSimulation.Engine
{
    public static class CollisionEngine
    {
        public static Vector2 GetCollisionVelocity(int objectId)
        {
            var colliders = SimulationObjectManager.GetSimulationObjects().Select(s => s.Collider);
            var simulationObject = SimulationObjectManager.GetSimulationObject(objectId);
            var collidedWith = simulationObject.Collider.CheckCollisions(colliders);
            
            var collisionVelocity = Vector2.Zero;
            foreach (var other in collidedWith)
            {
                var direction = -simulationObject.Collider.GetDirectionTo(other);
                const float correction = 1.05f;
                collisionVelocity += direction * simulationObject.ControlInput.Length() * correction;
            }
            return collisionVelocity;
        }
    }
}