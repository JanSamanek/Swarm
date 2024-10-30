using System.Numerics;
using SwarmSimulation.Engine;
using SwarmSimulation.Engine.Collision;
using SwarmSimulation.Engine.Entity;
using SwarmSimulation.Engine.Physics;

namespace SwarmSimulation.Environment.Obstacles
{
    public class CircularObstacle : SimulationObject, IObstacle
    {
        public float Radius { get; }

        public CircularObstacle(Vector2 center, float radius)
        {
            Radius = radius;
            Position = center;
            Collider = new CircleCollider(center, radius, Id);
            Body = new RigidBody(center, 1, Id, isStatic:true);

        }

        public Vector2 GetDistanceVectorFromBorder(Vector2 point)
        {
            var vectorToPoint = point - Position;
            var direction = Vector2.Normalize(vectorToPoint);
            return vectorToPoint - Radius * direction;
        }

        public bool IsPointInside(Vector2 point)
        {
            var distanceSquared = Vector2.DistanceSquared(point, Position);
            var radiusSquared = Radius * Radius;
            return distanceSquared < radiusSquared;
        }
    }
}