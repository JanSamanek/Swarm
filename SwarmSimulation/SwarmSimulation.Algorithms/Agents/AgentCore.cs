using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Numerics;
using System.Threading.Tasks;
using SwarmSimulation.Agents;
using SwarmSimulation.Environment;
using SwarmSimulation.Environment.Obstacles;
using SwarmSimulation.Utilities;

namespace SwarmSimulation.Algorithms.Agents
{
    public class AgentCore
    {
        protected AgentCore(int id, Vector2 position, float perceptionRange)
        {
            Id = id;
            Position = position;
            PerceptionRange = perceptionRange;
        }
        public int Id { get; }
        public Vector2 Position { get; private set; }
        public Vector2 Velocity { get; private set; } = Vector2.Zero;
        public float PerceptionRange { get; }
        public List<IAgent> Neighbors { get; set; } = new List<IAgent>();
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
    }
}