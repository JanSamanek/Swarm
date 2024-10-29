using System.Numerics;

namespace SwarmSimulation.Engine.Collision
{
    public class CircleCollider : Collider
    {
        public float Radius { get; }

        public CircleCollider(Vector2 center, float radius)
        {
            Center = center;
            Radius = radius;
            ColliderManager.RegisterCollider(this);
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
            var distance = Vector2.Distance(Center, circle.Center);
            return distance < Radius + circle.Radius;
        }
    }
}