using System.Collections.Generic;
using System.Linq;
using SwarmSimulation.Engine.Entity;

namespace SwarmSimulation.Engine.Collision
{
    public abstract class Collider : SimulationComponent
    {
        private readonly List<Collider> _collidedWith = new List<Collider>();
        public int Id { get; protected set; }
        private void AddCollision(Collider other)
        {
            if (!_collidedWith.Contains(other))
            {
                _collidedWith.Add(other);
            }
        }

        private void RemoveCollision(Collider other)
        { 
            if (_collidedWith.Contains(other))
            {
                _collidedWith.Remove(other);
            }
        }

        public abstract bool IsColliding(Collider other);
        public  IReadOnlyList<Collider> UpdateCollisions(IEnumerable<Collider> otherColliders)
        {
            foreach (var other in otherColliders)
            {
                if (other != this)
                {
                    if (IsColliding(other))
                    {
                        AddCollision(other);
                    }
                    else
                    {
                        RemoveCollision(other);
                    }
                }
            }
            return _collidedWith.AsReadOnly();
        }

        public bool HasCollided()
        {
            return _collidedWith.Any();
        }
    }
}