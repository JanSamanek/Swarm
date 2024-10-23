using System.Numerics;
using SwarmSimulation.Core.Agents.Implementation;
using SwarmSimulation.Core.Algorithms.Contracts;

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
            throw new System.NotImplementedException();
        }
    }
}