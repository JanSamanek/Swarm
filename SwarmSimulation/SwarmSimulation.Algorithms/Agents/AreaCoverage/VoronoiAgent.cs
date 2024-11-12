using System.Numerics;
using SwarmSimulation.Algorithms.AreaCoverage.Lloyd.Utilities;

namespace SwarmSimulation.Algorithms.Agents.AreaCoverage
{
    public class VoronoiAgent : Agent
    {
        public VoronoiAgent(Vector2 position, float size, float perceptionRange) 
            : base(position, size, perceptionRange)
        {
            
        }

        public Cell VoronoiCell { get; set; }
    }
}