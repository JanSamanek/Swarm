using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;
using SwarmSimulation.Engine.Collider;
using SwarmSimulation.Environment;
using SwarmSimulation.Environment.Obstacles;
using SwarmSimulation.Utilities;

namespace SwarmSimulation.Algorithms.Agents
{
    public class AgentCore : IAgent
    {
        protected AgentCore(int id, Vector2 position, float size, float perceptionRange)
        {
            Id = id;
            Position = position;
            Collider = new CircleCollider(position, size);
            Size = size;
            PerceptionRange = perceptionRange;
        }
        public int Id { get; }
        public Vector2 Position { get; private set; }
        public float Size { get; private set; }
        public ICollider Collider { get; set; } 
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
            var collided = false;
            Parallel.ForEach(ColliderManager.Colliders, (collider, state) =>
            {
                if (ReferenceEquals(Collider, collider))
                {
                    return;
                }
                if (Collider.Intersects(collider))
                {
                    collided = true;
                    state.Stop();
                }
            });
            return collided;
        }
        
        public bool HasApproachedTarget(Vector2 target, float tolerance=5)
        {
            return Vector2.Distance(Position, target) <= tolerance;
        }
    }
}