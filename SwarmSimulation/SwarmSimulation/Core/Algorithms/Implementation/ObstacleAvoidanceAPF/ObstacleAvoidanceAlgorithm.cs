using System;
using System.Numerics;
using SwarmSimulation.Core.Agents.Implementation;
using SwarmSimulation.Core.Algorithms.Contracts;
using SwarmSimulation.Environment;

namespace SwarmSimulation.Core.Algorithms.Implementation.ObstacleAvoidanceAPF
{
    public class ObstacleAvoidanceAlgorithm : IAlgorithm<ObstacleAvoidanceAlgorithmSettings, ObstacleAvoidanceAlgorithmInput>
    {
        public ObstacleAvoidanceAlgorithmSettings Settings { get; set; }
        public ObstacleAvoidanceAlgorithm(ObstacleAvoidanceAlgorithmSettings settings)
        {
            Settings = settings;
        }

        public Vector2 CalculateControlInput(RegularAgent agent, ObstacleAvoidanceAlgorithmInput input)
        {
            var obstacles = agent.DetectObstacles();
            var controlInput = Vector2.Zero;
            foreach (var obstacle in obstacles)
            {
                var distanceVector = obstacle.GetDistanceVectorToAgent(agent.Position);
                controlInput += CalculateApf(distanceVector, input.ThresholdDistance);
            }

            foreach (var neighbor in agent.Neighbors)
            {
                var distanceVector = agent.Position - neighbor.Position;
                controlInput += CalculateApf(distanceVector, input.ThresholdDistance);
            }
            return controlInput;
        }

        private Vector2 CalculateApf(Vector2 distanceVector, float threshold)
        {
            var direction = Vector2.Normalize(distanceVector);
            return Settings.ApfGain * direction * (float) Math.Pow(1 / distanceVector.Length() - 1 / threshold, 2);
        }
    }
}