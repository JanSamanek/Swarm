using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using SwarmSimulation.Environment.Obstacles;

namespace SwarmSimulation.Environment
{
    public class Arena
    {
        private static Arena _instance;
        public Vector2 Center;
        public int Width;
        public  int Height;
        public static Arena Instance => _instance ?? (_instance = new Arena());
        public Nest Nest { get; set; }
        public List<IObstacle> Obstacles { get; } = new List<IObstacle>(); 
        public List<Resource> Resources { get; } = new List<Resource>();
    }
}