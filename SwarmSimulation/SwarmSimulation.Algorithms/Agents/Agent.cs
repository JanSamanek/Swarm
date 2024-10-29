using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;
using SwarmSimulation.Engine.Collision;
using SwarmSimulation.Environment;
using SwarmSimulation.Environment.Obstacles;
using SwarmSimulation.Utilities;

namespace SwarmSimulation.Algorithms.Agents
{
    public abstract class Agent
    {
        protected Agent(int id, Vector2 position, float size, float perceptionRange)
        {
            Id = id;
            Collider = new CircleCollider(position, size);
            Position = position;
            Size = size;
            PerceptionRange = perceptionRange;
        }
        public int Id { get; }
        private Vector2 _position;
        public Vector2 Position
        {
            get => _position;
            private set
            {
                _position = value;
                Collider.UpdatePosition(_position);
            }
        }
        public float Size { get; }
        public Collider Collider { get; set; } 
        public Vector2 Velocity { get; private set; } = Vector2.Zero;
        public float PerceptionRange { get; }
        public List<Agent> Neighbors { get; set; } = new List<Agent>();
        
        public void Move(Vector2 controlInput)
        {
            Velocity = controlInput;
            Position += controlInput * SimulationTimeManager.GetDeltaTime();;
        }
        
        public IEnumerable<IObstacle> DetectObstacles()
        {
            var obstaclesInRange = new ConcurrentBag<IObstacle>();
            Parallel.ForEach(Arena.Instance.Obstacles, obstacle =>
            {
                var distanceVector = obstacle.GetDistanceVectorFromBorder(Position);
                if (distanceVector.Length() < PerceptionRange)
                {
                    obstaclesInRange.Add(obstacle);
                }
            });
            return obstaclesInRange;
        }

        public bool DetectCollision()
        {
            return Collider.HasCollided();
        }
        
        public bool HasApproachedTarget(Vector2 target, float tolerance=5)
        {
            return Vector2.Distance(Position, target) <= tolerance;
        }
    }
}