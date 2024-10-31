using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace SwarmSimulation.Engine.Collision
{
    public abstract class Collider
    {
        public bool HasCollided { get; private set; }
        public int ObjectId { get; protected set; }
        public Vector2 Position { get; protected set; }
        
        public abstract bool IsColliding(Collider other);
        public abstract Vector2 GetDirectionTo(Collider other);
        public  IReadOnlyCollection<Collider> CheckCollisions(IEnumerable<Collider> otherColliders)
        {
            var collidedWith = new List<Collider>();
            foreach (var other in otherColliders)
            {
                if (other != this)
                {
                    if (IsColliding(other))
                    {
                        collidedWith.Add(other);
                    }
                }
            }
            HasCollided = collidedWith.Any();
            return collidedWith.AsReadOnly();
        }
        public void UpdatePosition(Vector2 position)
        {
            Position = position;
        }
    }
}