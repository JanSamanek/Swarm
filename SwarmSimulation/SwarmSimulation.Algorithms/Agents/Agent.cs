using System.Collections.Generic;
using System.Numerics;
using SwarmSimulation.Engine.Collision;
using SwarmSimulation.Engine.Entity;
using SwarmSimulation.Environment;
using SwarmSimulation.Environment.Obstacles;
using SwarmSimulation.Utilities;

namespace SwarmSimulation.Algorithms.Agents
{
    public abstract class Agent : SimulationObject
    {
        protected Agent(Vector2 position, float size, float perceptionRange)
        {
            Collider = new CircleCollider(position, size, ObjectId);
            Position = position;
            Size = size;
            PerceptionRange = perceptionRange;
            IsStatic = false;                       
        }
        public float Size { get; }
        public float PerceptionRange { get; }
        public List<Agent> Neighbors { get; set; } = new List<Agent>();
        
        public void Move(Vector2 controlInputVelocity, Vector2 collisionVelocity)
        {
            Velocity = controlInputVelocity + collisionVelocity;
            ControlInput = controlInputVelocity;
            Position += Velocity * SimulationTimeManager.GetDeltaTime();
        }
        
        public IEnumerable<IObstacle> DetectObstacles()
        {
            var obstaclesInRange = new List<IObstacle>();

            foreach (var obstacle in Arena.Instance.Obstacles)
            {
                var distanceVector = obstacle.GetDistanceVectorFromBorder(Position);
                if (distanceVector.Length() < PerceptionRange)
                {
                    obstaclesInRange.Add(obstacle);
                }
            }
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