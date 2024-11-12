using System.Numerics;

namespace SwarmSimulation.Algorithms.AreaCoverage.Lloyd.Utilities
{
    public class Line
    {
        private readonly float _slope;
        private readonly float _intercept;

        public Line(float slope, float intercept)
        {
            _slope = slope;
            _intercept = intercept;
        }

        public Vector2 GetIntersection(Line other)
        {
            var xIntersect = (other._intercept - _intercept) / (_slope - other._slope);
            var yIntersect = _slope * xIntersect + _intercept;
            return new Vector2(xIntersect, yIntersect);
        }
    }
}