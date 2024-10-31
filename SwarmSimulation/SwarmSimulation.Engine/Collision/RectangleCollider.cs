using System;
using System.Numerics;
using SwarmSimulation.Utilities;

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
            ObjectId = objectId;
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

        public override Vector2 GetDirectionTo(Collider other)
        {
            switch (other)
            {
                case CircleCollider circle:
                {
                    return -circle.GetDirectionTo(this);
                }
                case RectangleCollider rectangle:
                {
                    return Vector2.Zero;
                }
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private bool Intersects(CircleCollider circle)
        {
            var closest = GetClosestPointTo(circle);
    
            var distanceX = circle.Position.X - closest.X;
            var distanceY = circle.Position.Y - closest.Y;
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

        public Vector2 GetClosestPointTo(CircleCollider circle)
        {
            var closestX = MathUtils.Clamp(circle.Position.X, Position.X - Width/2, Position.X +  Width/2);
            var closestY = MathUtils.Clamp(circle.Position.Y, Position.Y - Height/2, Position.Y + Height/2);
            return new Vector2(closestX, closestY);
        }
    }
}