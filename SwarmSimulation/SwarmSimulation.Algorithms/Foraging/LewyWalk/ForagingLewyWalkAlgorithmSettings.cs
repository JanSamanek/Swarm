using SwarmSimulation.Algorithms.MoveToTarget;

namespace SwarmSimulation.Algorithms.Foraging.LewyWalk
{
    public class ForagingLewyWalkAlgorithmSettings
    {
        public float LewyParameter { get; set; }
        public float MaxFlightLength { get; set; }
        public float LewyScale { get; set; }
        public MoveToTargetAlgorithmSettings MoveToTargetAlgorithmSettings { get; set; }
    }
}