using System;
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
            var halfWidth = Width / 2;
            var halfHeight = Height / 2;
            
            var distanceX = Math.Max(0, Math.Abs(distanceVectorToAgent.X) - halfWidth) * Math.Sign(distanceVectorToAgent.X);
            var distanceY = Math.Max(0, Math.Abs(distanceVectorToAgent.Y) - halfHeight) * Math.Sign(distanceVectorToAgent.Y);

            if (Math.Abs(distanceVectorToAgent.X) <= halfWidth)
                distanceX = -(halfWidth - Math.Abs(distanceVectorToAgent.X)) * Math.Sign(distanceVectorToAgent.X);

            if (Math.Abs(distanceVectorToAgent.Y) <= halfHeight)
                distanceY = -(halfHeight - Math.Abs(distanceVectorToAgent.Y)) * Math.Sign(distanceVectorToAgent.Y);

            return new Vector2(distanceX, distanceY);
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