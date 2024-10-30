using System;
using System.Numerics;
using SwarmSimulation.Engine.Entity;

namespace SwarmSimulation.Engine.Physics
{
    public class RigidBody : SimulationComponent
    {
        private readonly float _mass;
        private readonly bool _isStatic;
        private Vector2 _velocity = Vector2.Zero;
        public int Id { get; set; }

        public RigidBody(Vector2 position, float mass, int objectId, bool isStatic = false)
        {
            Position = position;
            _mass = mass;
            Id = objectId;
            _isStatic = isStatic;
        }

        public void UpdateVelocity(Vector2 velocity)
        {
            _velocity = velocity;
        }
        
        // TODO: collision of rectangular as well
        public Vector2 GetVelocityFromCollision(RigidBody other, Vector2 normalDirection)
        {
            if (_isStatic)
            {
                return Vector2.Zero;
            }

            var velocityNormal = _velocity * normalDirection; 
            var otherVelocityNormal = other._velocity * normalDirection;
            
            var tangentDirection = new Vector2(-normalDirection.Y, normalDirection.X);
            var velocityTangent = _velocity * tangentDirection;

            var collisionVelocityTangent = velocityTangent;
            var collisionVelocityNormal = (velocityNormal * (_mass - other._mass) + 2 * other._mass * otherVelocityNormal) /
                                    (_mass + other._mass); 
            
            var collisionVelocity = collisionVelocityNormal + collisionVelocityTangent;
            return collisionVelocity;
        }
    }
}