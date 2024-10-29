using System.Collections.Generic;

namespace SwarmSimulation.Engine.Collision
{
    public static class ColliderManager
    {
        private static readonly List<Collider> Colliders = new List<Collider>();

        public static void RegisterCollider(Collider collider)
        {
            if (!Colliders.Contains(collider))
            {
                Colliders.Add(collider);
            }
        }
        
        public static void UnregisterCollider(Collider collider)
        {
            if (Colliders.Contains(collider))
            {
                Colliders.Remove(collider);
            }
        }

        public static void Update()
        {
            foreach (var collider in Colliders)
            {
                collider.CheckCollisions(Colliders);
            }
        }
    }
}