namespace SwarmSimulation.Algorithms.Foraging.AdaptiveLewyWalk
{
    public class AclwForagingAlgorithmSettings
    {
        public float BrownianToLewyTimeSeconds { get; set; }
        public float MaxFlightLength { get; set; }
        public float LongFlightThreshold { get; set; }
        public float LewyScale { get; set; }
        public int MaxExploringAttempts { get; set; }
        public float RepulsionGain { get; set; }
    }
}