using System;
using System.Numerics;

namespace SwarmSimulation.Environment
{
    public class Nest
    {
        private readonly Random _random = new Random();
        public Vector2 Center { get; }
        public float Width { get; } 
        public float Height { get; }

        public Nest(Vector2 center, float width, float height)
        {
            Center = center;
            Width = width;
            Height = height;
        }


        public Vector2 GetRandomPositionInNest()
        {
            var x = Width/2 * ((float) _random.NextDouble() * 2 -1);
            var y = Height/2 * ((float) _random.NextDouble() * 2 -1);
            return new Vector2(Center.X + x, Center.Y + y);
        }
    }
}