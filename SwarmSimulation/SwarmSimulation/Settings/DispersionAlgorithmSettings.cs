namespace SwarmSimulation.Settings
{
    public class DispersionAlgorithmSettings
    {
        public int DesiredDistance { get; set; }
        public float DampingCoefficient { get; set; }
        public float StiffnessCoefficient { get; set; }
        public int NeighboursToCalculateFrom {get; set;}
    }
}