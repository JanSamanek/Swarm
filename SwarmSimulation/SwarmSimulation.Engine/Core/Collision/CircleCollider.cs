using System;
using System.Numerics;

namespace SwarmSimulation.Engine.Core.Collision
{
    public class CircleCollider : Collider
    {
        public float Radius { get; }

        public CircleCollider(Vector2 center, float radius, int objectId)
        {
            Position = center;
            Radius = radius;
            ObjectId = objectId;
        }
        
        public override bool IsColliding(Collider other)
        {
            switch (other)
            {
                case CircleCollider circle:
                {
                    return Intersects(circle);
                }
                case RectangleCollider rectangle:
                {
                    return rectangle.IsColliding(this);
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
                    var direction = Vector2.Normalize(circle.Position - Position);
                    return  direction;
                }
                case RectangleCollider rectangle:
                {
                    var closest = rectangle.GetClosestPointTo(this);
                    var direction = Vector2.Normalize(closest - Position);
                    return direction;
                }
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private bool Intersects(CircleCollider circle)
        {
            var distance = Vector2.Distance(Position, circle.Position);
            return distance < Radius + circle.Radius;
        }
    }
}