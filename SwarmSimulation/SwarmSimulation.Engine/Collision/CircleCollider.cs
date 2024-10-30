using System.Numerics;

namespace SwarmSimulation.Engine.Collision
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

        private bool Intersects(CircleCollider circle)
        {
            var distance = Vector2.Distance(Position, circle.Position);
            return distance < Radius + circle.Radius;
        }
    }
}