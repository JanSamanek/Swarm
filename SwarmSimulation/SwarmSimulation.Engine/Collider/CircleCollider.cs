using System.Numerics;

namespace SwarmSimulation.Engine.Collider
{
    public class CircleCollider : ICollider
    {
        public float Radius { get; set; }
        public Vector2 Center { get; set; }

        public CircleCollider(Vector2 center, float radius)
        {
            Center = center;
            Radius = radius;
            ColliderManager.Colliders.Add(this);
        }
        
        public bool Intersects(ICollider other)
        {
            switch (other)
            {
                case CircleCollider circle:
                {
                    var distance = Vector2.Distance(Center, circle.Center);
                    return distance < Radius + circle.Radius;
                }
                case RectangleCollider rectangle:
                {
                    return rectangle.Intersects(this);
                }
                default:
                    return false;
            }
        }
    }
}