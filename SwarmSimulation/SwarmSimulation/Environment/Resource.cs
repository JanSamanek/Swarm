using System.Numerics;

namespace SwarmSimulation.Environment
{
    public class Resource
    {
        public Vector2 Position { get; set; }
        public int Capacity { get; set; }
     
        public Resource(Vector2 position ,int capacity)
        {
            Position = position;
            Capacity = capacity;
        }
    }
}