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
                var objectId = collider.Id;
                var body = SimulationObjectManager.GetRigidBody(objectId);
                
                var collidedWith = collider.UpdateCollisions(colliders);
                
                var collisionVelocity = Vector2.Zero;
                foreach (var collisionId in collidedWith.Select(c => c.Id))
                {
                    var collisionBody = SimulationObjectManager.GetRigidBody(collisionId);

                    var direction = Vector2.Normalize(collisionBody.Position - body.Position);
                    collisionVelocity += body.GetVelocityFromCollision(collisionBody, direction);
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