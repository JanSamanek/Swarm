using System.Collections.Generic;
using System.Numerics;
using SwarmSimulation.Core;
using SwarmSimulation.Environment.Obstacles;
using SwarmSimulation.Environment.Obstacles.Contracts;
using SwarmSimulation.Environment.Obstacles.Implementation;
using SwarmSimulation.Properties;

namespace SwarmSimulation.Environment
{
    public class Arena
    {
        private static Arena _instance;
        public static Arena Instance => _instance ?? (_instance = new Arena());
        private int _width;
        private int _height;
        public List<IObstacle> Obstacles { get; set; } = new List<IObstacle>(); 
        public List<Resource> Resources { get; set; } = new List<Resource>();
        
        public void SetSize(int width, int height)
        {
            _width = width;
            _height = height;
        }
        
        public void AddCircularObstacle(Vector2 center, int radius)
        {
            Obstacles.Add(new CircularObstacle(center, radius));
        }

        public void AddRectangularObstacle(Vector2 center, float width, float height)
        {
            Obstacles.Add(new RectangularObstacle(center, width, height));
        }

        public void AddResource(Vector2 position, int capacity)
        {
            Resources.Add(new Resource(position, capacity));
        }
    }
}