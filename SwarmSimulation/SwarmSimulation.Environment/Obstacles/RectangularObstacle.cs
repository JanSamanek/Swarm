using System.Numerics;

namespace SwarmSimulation.Environment.Obstacles
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
        public Vector2 GetDistanceVectorFromBorder(Vector2 point)
        {
            var distanceVectorToAgent = point - Center;
            var distanceFromBorder = Vector2.Zero;
            
            if (point.X > Center.X + Width / 2)
            {
                distanceFromBorder.X = distanceVectorToAgent.X - Width / 2;
            }
            else if (point.X < Center.X - Width / 2)
            {
                distanceFromBorder.X = distanceVectorToAgent.X + Width / 2;
            }

            if (point.Y > Center.Y + Height / 2)
            {
                distanceFromBorder.Y = distanceVectorToAgent.Y - Height / 2;
            }
            else if (point.Y < Center.Y - Height / 2)
            {
                distanceFromBorder.Y = distanceVectorToAgent.Y + Height / 2;
            }
            
            return distanceFromBorder;
        }

        public bool IsPointInside(Vector2 point)
        {
            var halfWidth = Width / 2;
            var halfHeight = Height / 2;

            var left = Center.X - halfWidth;
            var right = Center.X + halfWidth;
            var top = Center.Y - halfHeight;
            var bottom = Center.Y + halfHeight;

            return point.X >= left && point.X <= right && point.Y >= bottom && point.Y <= top;
        }
    }
}