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
    public abstract class AgentCore : IAgent
    {
        protected AgentCore(int id, Vector2 position, float size, float perceptionRange)
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
        public float Size { get; private set; }
        public Collider Collider { get; set; } 
        public Vector2 Velocity { get; private set; } = Vector2.Zero;
        public float PerceptionRange { get; }
        public List<IAgent> Neighbors { get; set; } = new List<IAgent>();
        
        // TODO: remove bug position Nan, Nan => parallel computing??
        public void Move(Vector2 controlInput)
        {
            var dt = SimulationTimeManager.GetDeltaTime();
            
            Velocity = controlInput;
            Position += controlInput * dt;
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