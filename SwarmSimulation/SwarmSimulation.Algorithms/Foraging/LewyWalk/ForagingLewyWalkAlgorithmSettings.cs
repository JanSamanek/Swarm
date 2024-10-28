using SwarmSimulation.Algorithms.AdaptiveMoveToTarget;

namespace SwarmSimulation.Algorithms.Foraging.LewyWalk
{
    public class ForagingLewyWalkAlgorithmSettings
    {
        public float LewyParameter { get; set; }
        public float MaxFlightLength { get; set; }
        public AdaptiveMoveToTargetAlgorithmSettings AdaptiveMoveToTargetAlgorithmSettings { get; set; }
    }
}