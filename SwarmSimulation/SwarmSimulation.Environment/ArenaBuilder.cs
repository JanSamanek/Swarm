using System;
using System.Linq;
using System.Numerics;
using SwarmSimulation.Environment.Obstacles;

namespace SwarmSimulation.Environment
{
    public class ArenaBuilder
    {
        private int _padding;
        public ArenaBuilder Initialize(Vector2 position, float width, float height, int padding=5)
        {
            Arena.Instance.Width = (int) width;
            Arena.Instance.Height = (int) height;
            _padding = padding;
            var left = position.X + (float) padding/2 - width / 2;
            AddRectangularObstacle(new Vector2(left, position.Y), padding, height);
            var right = position.X - (float) padding / 2 + width/2;
            AddRectangularObstacle(new Vector2(right, position.Y), padding, height);
            var top = position.Y - (float) padding/2 + height / 2;
            AddRectangularObstacle(new Vector2(position.X, top), width, padding);
            var bottom = position.Y + (float) padding/2 - height / 2;
            AddRectangularObstacle(new Vector2(position.X, bottom), width, padding);
            return this;
        }

        public ArenaBuilder AddNest(Vector2 center, float width, float height)
        {
            Arena.Instance.Nest = new Nest(center, width, height);
            return this;
        }
        
        public ArenaBuilder AddCircularObstacle(Vector2 center, int radius)
        {
            Arena.Instance.Obstacles.Add(new CircularObstacle(center, radius));
            return this;
        }

        public ArenaBuilder AddRectangularObstacle(Vector2 center, float width, float height)
        {
            Arena.Instance.Obstacles.Add(new RectangularObstacle(center, width, height));
            return this;
        }
        
        public ArenaBuilder GenerateResources(int count)
        {
            var random = new Random();

            while (Arena.Instance.Resources.Count < count)
            {
                var position = new Vector2(random.Next(_padding, Arena.Instance.Width - _padding),
                    random.Next(_padding, Arena.Instance.Height - _padding));
                var isValid = Arena.Instance.Obstacles.All(obstacle => !obstacle.IsPointInside(position));
                if (isValid)
                {
                    AddResource(position, 1);
                }
            }
            return this;
        }
        private ArenaBuilder AddResource(Vector2 position, int capacity)
        {
            Arena.Instance.Resources.Add(new Resource(position, capacity));
            return this;
        }

        public Arena Build()
        {
            return Arena.Instance;
        }
    }
}