using System.Numerics;

namespace SwarmSimulation.Environment
{
    public class Resource
    {
        public Vector2 Position { get; set; }
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
            Arena.Instance.Resources.Remove(this);
        }
    }
}