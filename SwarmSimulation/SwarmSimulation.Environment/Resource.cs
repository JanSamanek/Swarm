using System.Numerics;
using SwarmSimulation.Environment.Utilities;

namespace SwarmSimulation.Environment
{
    public class Resource
    {
        public Vector2 Position { get; }
        public bool IsConsumed { get; private set; }
        private int _capacity;
        
        public Resource(Vector2 position ,int capacity)
        {
            Position = position;
            _capacity = capacity;
        }

        public void Harvest()
        {
            _capacity -= 1;
            if (_capacity <= 0)
            {
                Destroy();
            }
        }

        private void Destroy()
        {
            IsConsumed = true;
            GarbageCollector.ResourceGarbage.Add(this);
            // Arena.Instance.Resources.Remove(this);
        }
    }
}