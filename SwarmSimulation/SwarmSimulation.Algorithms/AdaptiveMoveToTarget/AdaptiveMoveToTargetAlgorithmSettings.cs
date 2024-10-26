using SwarmSimulation.Algorithms.MoveToTarget;
using SwarmSimulation.Algorithms.ObstacleAvoidanceAPF;

namespace SwarmSimulation.Algorithms.AdaptiveMoveToTarget
{
    public class AdaptiveMoveToTargetAlgorithmSettings
    {
        public ObstacleAvoidanceAlgorithmSettings ObstacleAvoidanceAlgorithmSettings { get; set; }
        public MoveToTargetAlgorithmSettings MoveToTargetAlgorithmSettings { get; set; }
    }
}