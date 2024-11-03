using SwarmSimulation.Algorithms.Other.MoveToTarget;
using SwarmSimulation.Algorithms.Other.ObstacleAvoidanceAPF;

namespace SwarmSimulation.Algorithms.Other.AdaptiveMoveToTarget
{
    public class AdaptiveMoveToTargetAlgorithmInput
    {
        public ObstacleAvoidanceAlgorithmInput ObstacleAvoidanceAlgorithmInput { get; set; }
        public MoveToTargetAlgorithmInput MoveToTargetAlgorithmInput { get; set; }
    }
}