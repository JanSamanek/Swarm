using System.Collections.Generic;
using System.Numerics;
using SwarmSimulation.Environment.Obstacles;

namespace SwarmSimulation.Environment
{
    public class Arena
    {
        private static Arena _instance;
        public static Arena Instance => _instance ?? (_instance = new Arena());
        public Nest Nest { get; private set; }
        public List<IObstacle> Obstacles { get; } = new List<IObstacle>(); 
        public List<Resource> Resources { get; } = new List<Resource>();

        public void Initialize(Vector2 position, float width, float height, float padding=5)
        {
            var left = position.X + padding/2 - width / 2;
            AddRectangularObstacle(new Vector2(left, position.Y), padding, height);
            var right = position.X - padding / 2 + width/2;
            AddRectangularObstacle(new Vector2(right, position.Y), padding, height);
            var top = position.Y - padding/2 + height / 2;
            AddRectangularObstacle(new Vector2(position.X, top), width, padding);
            var bottom = position.Y + padding/2 - height / 2;
            AddRectangularObstacle(new Vector2(position.X, bottom), width, padding);
        }

        public void AddNest(Vector2 center, float width, float height)
        {
            Nest = new Nest(center, width, height);
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

        public void GenerateResources(int count)
        {
            
        }
    }
}