namespace SwarmSimulation.Engine.Collider
{
    public interface ICollider
    {
        bool Intersects(ICollider other);
    }
}