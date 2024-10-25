using SwarmSimulation.Core.Algorithms.Implementation.MoveToTarget;
using SwarmSimulation.Core.Algorithms.Implementation.ObstacleAvoidanceAPF;

namespace SwarmSimulation.Core.Algorithms.Implementation.AdaptiveMoveToTarget
{
    public class AdaptiveMoveToTargetAlgorithmSettings
    {
        public ObstacleAvoidanceAlgorithmSettings ObstacleAvoidanceAlgorithmSettings { get; set; }
        public MoveToTargetAlgorithmSettings MoveToTargetAlgorithmSettings { get; set; }
    }
}