using SwarmSimulation.Core.Algorithms.Implementation.MoveToTarget;
using SwarmSimulation.Core.Algorithms.Implementation.ObstacleAvoidanceAPF;

namespace SwarmSimulation.Core.Algorithms.Implementation.AdaptiveMoveToTarget
{
    public class AdaptiveMoveToTargetAlgorithmInput
    {
        public ObstacleAvoidanceAlgorithmInput ObstacleAvoidanceAlgorithmInput { get; set; }
        public MoveToTargetAlgorithmInput MoveToTargetAlgorithmInput { get; set; }
    }
}