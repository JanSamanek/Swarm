using SwarmSimulation.Algorithms.MoveToTarget;

namespace SwarmSimulation.Algorithms.Foraging.LewyWalk
{
    public class ForagingLewyWalkAlgorithmSettings
    {
        public float LewyParameter { get; set; }
        public float MaxFlightLength { get; set; }
        public float LewyScale { get; set; }
        public int MaxExploringAttempts { get; set; }
    }
}