using System.Collections.Generic;
using System.Numerics;
using SwarmSimulation.Core;
using SwarmSimulation.Environment.Obstacles;
using SwarmSimulation.Environment.Obstacles.Contracts;
using SwarmSimulation.Environment.Obstacles.Implementation;

namespace SwarmSimulation.Environment
{
    public class Arena
    {
        private static Arena _instance;
        public static Arena Instance => _instance ?? (_instance = new Arena());
        public int Width {get; set;}
        public int Height {get; set;}
        public List<IObstacle> Obstacles { get; set; } = new List<IObstacle>(); 
        
        public void SetSize(int width, int height)
        {
            Width = width;
            Height = height;
        }
        
        public void AddCircularObstacle(Vector2 center, int radius)
        {
            Obstacles.Add(new CircularObstacle(center, radius));
        }

        public void AddRectangularObstacle(Vector2 center, float width, float height)
        {
            Obstacles.Add(new RectangularObstacle(center, width, height));
        }

    }
}