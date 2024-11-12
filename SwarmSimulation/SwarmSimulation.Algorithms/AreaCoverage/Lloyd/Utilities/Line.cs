using System.Numerics;

namespace SwarmSimulation.Algorithms.AreaCoverage.Lloyd.Utilities
{
    public class Line
    {
        public float Slope { get; }
        public float Intercept { get; }

        public Line(float slope, float intercept)
        {
            Slope = slope;
            Intercept = intercept;
        }

        public Vector2 GetIntersection(Line other)
        {
            var xIntersect = (other.Intercept - Intercept) / (Slope - other.Slope);
            var yIntersect = Slope * xIntersect + Intercept;
            return new Vector2(xIntersect, yIntersect);
        }
    }
}