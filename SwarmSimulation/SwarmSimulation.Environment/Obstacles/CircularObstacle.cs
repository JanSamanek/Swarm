using System.Numerics;

namespace SwarmSimulation.Environment.Obstacles
{
    public class CircularObstacle : IObstacle
    {
        public float Radius { get; }
        public Vector2 Center { get; }

        public CircularObstacle(Vector2 center, float radius)
        {
            Radius = radius;
            Center = center;
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