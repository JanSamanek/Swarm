using System.Numerics;

namespace SwarmSimulation.Algorithms.AreaCoverage.Lloyd.Utilities
{
    public class Line
    {
        private float _slope;
        private float _intercept;

        public Line(float slope, float intercept)
        {
            _slope = slope;
            _intercept = intercept;
        }

        public Vector2 GetIntersection(Line line)
        {
            
        }
    }
}