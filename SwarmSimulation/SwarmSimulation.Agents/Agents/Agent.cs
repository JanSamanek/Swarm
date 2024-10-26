using System.Collections.Generic;
using System.Numerics;
using System.Threading.Tasks;
using SwarmSimulation.Agents.Agents.Contracts;
using SwarmSimulation.Environment;
using SwarmSimulation.Environment.Obstacles;
using SwarmSimulation.Utilities;

namespace SwarmSimulation.Agents.Agents
{
    public class Agent
    {
        protected Agent(int id, Vector2 position, float perceptionRange)
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
            var obstaclesInRange = new List<IObstacle>();
            Parallel.ForEach(Arena.Instance.Obstacles, obstacle =>
            {
                var distanceVector = obstacle.GetDistanceVectorToAgent(Position);
                if (distanceVector.Length() < PerceptionRange)
                {
                    obstaclesInRange.Add(obstacle);
                }
            });
            return obstaclesInRange;
        }

        public IEnumerable<Resource> DetectResources()
        {
            var resourcesInRange = new List<Resource>();
            Parallel.ForEach(Arena.Instance.Resources, resource =>
            {
                var distance = (resource.Position - Position).Length();
                if (distance < PerceptionRange)
                {
                    resourcesInRange.Add(resource);
                }
            });
            return resourcesInRange;
        }
    }
}