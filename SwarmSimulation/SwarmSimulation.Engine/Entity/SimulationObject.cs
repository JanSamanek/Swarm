using System;
using System.Numerics;
using SwarmSimulation.Engine.Collision;
using SwarmSimulation.Engine.Physics;

namespace SwarmSimulation.Engine.Entity
{
    public abstract class SimulationObject
    {
        protected SimulationObject()
        {
            SimulationObjectManager.Register(this);
        }
        public int Id { get; } = Guid.NewGuid().GetHashCode();
        private Vector2 _velocity;
        public Vector2 Velocity 
        { 
            get => _velocity;
            protected set
            {
                _velocity = value;
                Body.UpdateVelocity(_velocity);
            }
        }
        
        private Vector2 _position;
        public Vector2 Position
        {
            get => _position;
            protected set
            {
                _position = value;
                Collider?.UpdatePosition(_position); 
                Body?.UpdatePosition(_position);
            }
        }
        public Collider Collider { get; protected set; }
        public RigidBody Body { get; protected set; }
    }
}