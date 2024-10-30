using System;
using System.Numerics;
using SwarmSimulation.Utilities.Mathematics;

namespace SwarmSimulation.Engine.Collision
{
    public class RectangleCollider : Collider
    { 
        public float Width { get; }
        public float Height { get; }
        
        public RectangleCollider(Vector2 center, float width, float height, int objectId)
        {
            Position = center;    
            Width = width;
            Height = height;
            Id = objectId;
        }
        public override bool IsColliding(Collider other)
        {
            switch (other)
            {
                case RectangleCollider rectangle:
                {
                    return Intersects(rectangle);
                }
                case CircleCollider circle:
                {
                    return Intersects(circle);
                }
                default:
                    return false;
            }
        }
        
        private bool Intersects(CircleCollider circle)
        {
            var closestX = MathUtils.Clamp(circle.Position.X, Position.X - Width/2, Position.X +  Width/2);
            var closestY = MathUtils.Clamp(circle.Position.Y, Position.Y - Height/2, Position.Y + Height/2);
    
            var distanceX = circle.Position.X - closestX;
            var distanceY = circle.Position.Y - closestY;
            var vector = new Vector2(distanceX, distanceY);
            
            var distanceSquared = vector.LengthSquared();
            var radiusSquared = circle.Radius * circle.Radius;
            return distanceSquared <= radiusSquared;
        }

        private bool Intersects(RectangleCollider rectangle)
        {
            var xOverlap = Math.Abs(Position.X - rectangle.Position.X) <= Width / 2 + rectangle.Width / 2;
            var yOverlap = Math.Abs(Position.Y - rectangle.Position.Y) <= Height / 2  + rectangle.Height / 2;
            return xOverlap && yOverlap;
        }
    }
}