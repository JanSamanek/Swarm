using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace SwarmSimulation.Engine.Collision
{
    public abstract class Collider
    {
        private readonly List<Collider> _collidedWith = new List<Collider>();
        public int ObjectId { get; protected set; }
        public Vector2 Position { get; protected set; }
        
        public abstract bool IsColliding(Collider other);
        public abstract Vector2 GetDirectionTo(Collider other);
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
        public  IReadOnlyList<Collider> CheckCollisions(IEnumerable<Collider> otherColliders)
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
        public void UpdatePosition(Vector2 position)
        {
            Position = position;
        }
    }
}