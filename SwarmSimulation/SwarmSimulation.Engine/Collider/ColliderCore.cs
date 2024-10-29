using System.Numerics;

namespace SwarmSimulation.Engine.Collider
{
    public class ColliderCore
    {
        public Vector2 Center { get; protected set; }

        public void UpdatePosition(Vector2 position)
        {
            Center = position;
        }
    }
}