using System.Numerics;

namespace SwarmSimulation.Engine.Entity
{
    public abstract class SimulationComponent
    {
        public Vector2 Position { get; protected set; }
        public void UpdatePosition(Vector2 position)
        {
            Position = position;
        }
    }
}