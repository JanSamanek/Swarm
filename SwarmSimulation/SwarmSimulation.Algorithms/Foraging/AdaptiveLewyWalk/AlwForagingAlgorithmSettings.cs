namespace SwarmSimulation.Algorithms.Foraging.AdaptiveLewyWalk
{
    public class AlwForagingAlgorithmSettings
    {
        public float BrownianToLewyTimeSeconds { get; set; }
        public float MaxFlightLength { get; set; }
        public float LewyScale { get; set; }
        public int MaxExploringAttempts { get; set; }
    }
}