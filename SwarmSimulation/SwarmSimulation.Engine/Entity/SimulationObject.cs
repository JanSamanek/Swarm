using System.Numerics;
using SwarmSimulation.Engine.Collision;
using SwarmSimulation.Utilities;

namespace SwarmSimulation.Engine.Entity
{
    public abstract class SimulationObject
    {
        protected SimulationObject()
        {
            SimulationObjectManager.Register(this);
        }
        private Vector2 _position;
        public bool IsStatic { get; set; }
        public int ObjectId { get; } = IdGenerator.GetNextId();
        public Vector2 Velocity { get; protected set; } = Vector2.Zero;
        public Vector2 ControlInput { get; set; } = Vector2.Zero;
        public Vector2 Position
        {
            get => _position;
            protected set
            {
                _position = value;
                Collider?.UpdatePosition(_position); 
            }
        }
        public Collider Collider { get; protected set; }
    }
}