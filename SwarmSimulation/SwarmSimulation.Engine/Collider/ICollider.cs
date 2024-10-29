using System.Numerics;

namespace SwarmSimulation.Engine.Collider
{
    public interface ICollider
    {
        void UpdatePosition(Vector2 position);
        bool Intersects(ICollider other);
    }
}