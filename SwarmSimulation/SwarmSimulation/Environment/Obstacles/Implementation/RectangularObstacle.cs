using System.Numerics;
using SwarmSimulation.Environment.Obstacles.Contracts;

namespace SwarmSimulation.Environment.Obstacles.Implementation
{
    public class RectangularObstacle : IObstacle
    {
        public RectangularObstacle(Vector2 center, float width, float height)
        {
            Center = center;
            Width = width;
            Height = height;
        }

        public Vector2 Center { get; }
        public float Width { get; }
        public float Height { get; }
        public Vector2 GetDistanceVectorToAgent(Vector2 agentPosition)
        {
            var distanceVectorToAgent = agentPosition - Center;
            var distanceFromBorder = Vector2.Zero;
            
            if (agentPosition.X > Center.X + Width / 2)
            {
                distanceFromBorder.X = distanceVectorToAgent.X - Width / 2;
            }
            else if (agentPosition.X < Center.X - Width / 2)
            {
                distanceFromBorder.X = distanceVectorToAgent.X + Width / 2;
            }

            if (agentPosition.Y > Center.Y + Height / 2)
            {
                distanceFromBorder.Y = distanceVectorToAgent.Y - Height / 2;
            }
            else if (agentPosition.Y < Center.Y - Height / 2)
            {
                distanceFromBorder.Y = distanceVectorToAgent.Y + Height / 2;
            }
            
            return distanceFromBorder;
        }
    }
}