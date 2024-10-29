using System.Numerics;
using SwarmSimulation.Engine.Collision;

namespace SwarmSimulation.Environment.Obstacles
{
    public class CircularObstacle : IObstacle
    {
        public float Radius { get; }
        public Vector2 Center { get; }
        public Collider Collider { get; }

        public CircularObstacle(Vector2 center, float radius)
        {
            Radius = radius;
            Center = center;
            Collider = new CircleCollider(center, radius);
        }

        public Vector2 GetDistanceVectorFromBorder(Vector2 point)
        {
            var vectorToPoint = point - Center;
            var direction = Vector2.Normalize(vectorToPoint);
            return vectorToPoint - Radius * direction;
        }

        public bool IsPointInside(Vector2 point)
        {
            var vectorToPoint = point - Center;
            var distanceSquared = vectorToPoint.LengthSquared();
            return distanceSquared < Radius * Radius;
        }
    }
}