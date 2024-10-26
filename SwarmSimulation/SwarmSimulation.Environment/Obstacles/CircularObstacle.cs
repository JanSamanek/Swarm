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

        public Vector2 GetDistanceVectorToAgent(Vector2 agentPosition)
        {
            var distanceVectorToAgent = agentPosition - Center;
            var direction = Vector2.Normalize(distanceVectorToAgent);
            var distanceFromBorder = distanceVectorToAgent - direction * Radius;
            return distanceFromBorder;
        }
    }
}