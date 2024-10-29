using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace SwarmSimulation.Engine.Collision
{
    public abstract class Collider
    {
        private readonly List<Collider> _collidedWith = new List<Collider>();
        public Vector2 Center { get; protected set; }

        public void UpdatePosition(Vector2 position)
        {
            Center = position;
        }
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
        public void CheckCollisions(List<Collider> otherColliders)
        {
            Parallel.ForEach(otherColliders.Where(other => other != this), other =>
            {
                if (IsColliding(other))
                {
                    AddCollision(other);
                }
                else
                {
                    RemoveCollision(other);
                }
            });
        }

        public bool HasCollided()
        {
            return _collidedWith.Any();
        }

    }
}