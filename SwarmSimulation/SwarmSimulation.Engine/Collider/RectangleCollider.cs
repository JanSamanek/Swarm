using System;
using System.Numerics;
using SwarmSimulation.Utilities.Mathematics;

namespace SwarmSimulation.Engine.Collider
{
    public class RectangleCollider : ICollider
    { 
        public Vector2 Center { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
        
        public RectangleCollider(Vector2 center, float width, float height)
        {
            Center = center;    
            Width = width;
            Height = height;
            ColliderManager.Colliders.Add(this);
        }
        public bool Intersects(ICollider other)
        {
            switch (other)
            {
                case RectangleCollider rectangle:
                {
                    var xOverlap = Math.Abs(Center.X - rectangle.Center.X) <= Width / 2 + rectangle.Width / 2;
                    var yOverlap = Math.Abs(Center.Y - rectangle.Center.Y) <= Height / 2  + rectangle.Height / 2;
                    return xOverlap && yOverlap;
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
            var closestX = MathUtils.Clamp(circle.Center.X, Center.X - Width/2, Center.X +  Width/2);
            var closestY = MathUtils.Clamp(circle.Center.Y, Center.Y - Height/2, Center.Y + Height/2);
    
            var distanceX = circle.Center.X - closestX;
            var distanceY = circle.Center.Y - closestY;
            var vector = new Vector2(distanceX, distanceY);
            
            var distanceSquared = vector.LengthSquared();
            var radiusSquared = circle.Radius * circle.Radius;
            return distanceSquared <= radiusSquared;
        }
    }
}