using System;
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

        public int Harvest(int amount)
        {
            _capacity -= amount;
            if (_capacity > 0)
            {
                return amount;
            }
            
            Destroy();
            var amountHarvested = amount - Math.Abs(_capacity);
            return amountHarvested;
        }

        private void Destroy()
        {
            IsConsumed = true;
            GarbageCollector.ResourceGarbage.Add(this);
        }
    }
}