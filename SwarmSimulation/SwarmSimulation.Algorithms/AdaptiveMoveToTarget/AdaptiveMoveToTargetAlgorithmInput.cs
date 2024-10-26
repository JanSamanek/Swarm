using SwarmSimulation.Algorithms.MoveToTarget;
using SwarmSimulation.Algorithms.ObstacleAvoidanceAPF;

namespace SwarmSimulation.Algorithms.AdaptiveMoveToTarget
{
    public class AdaptiveMoveToTargetAlgorithmInput
    {
        public ObstacleAvoidanceAlgorithmInput ObstacleAvoidanceAlgorithmInput { get; set; }
        public MoveToTargetAlgorithmInput MoveToTargetAlgorithmInput { get; set; }
    }
}