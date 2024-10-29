using System;
using System.Numerics;
using SwarmSimulation.Algorithms.Agents;

namespace SwarmSimulation.Algorithms.ObstacleAvoidanceAPF
{
    public class ObstacleAvoidanceAlgorithm : IAlgorithm<ObstacleAvoidanceAlgorithmInput>
    {
        private readonly ObstacleAvoidanceAlgorithmSettings _settings;
        
        public ObstacleAvoidanceAlgorithm(ObstacleAvoidanceAlgorithmSettings settings)
        {
            _settings = settings;
        }

        public Vector2 CalculateControlInput(IAgent agent, ObstacleAvoidanceAlgorithmInput input)
        {
            var obstacles = agent.DetectObstacles();
            var controlInput = Vector2.Zero;
            foreach (var obstacle in obstacles)
            {
                var distanceVector = obstacle.GetDistanceVectorFromBorder(agent.Position);
                controlInput += CalculateApf(distanceVector, input.Distance);
            }

            foreach (var neighbor in agent.Neighbors)
            {
                var distanceVector = agent.Position - neighbor.Position;
                controlInput += CalculateApf(distanceVector, input.Distance);
            }
            return controlInput;
        }

        private Vector2 CalculateApf(Vector2 distanceVector, float threshold)
        {
            var direction = Vector2.Normalize(distanceVector);
            return _settings.ApfGain * direction * (float) Math.Pow(1 / distanceVector.Length() - 1 / threshold, 2);
        }
    }
}