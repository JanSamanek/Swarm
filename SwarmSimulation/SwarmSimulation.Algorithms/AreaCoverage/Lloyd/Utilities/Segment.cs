using System;
using System.Numerics;

namespace SwarmSimulation.Algorithms.AreaCoverage.Lloyd.Utilities
{
    public class Segment
    {
        public Segment(Vector2 point1, Vector2 point2)
        {
            _point1 = point1;
            _point2 = point2;
        }

        private Vector2 _point1;
        private Vector2 _point2;
        
        public void Update(Vector2 point1, Vector2 point2)
        {
            _point1 = point1;
            _point2 = point2;
        }

        public bool IsPointOnSegment(Vector2 point)
        {
            return point.X >= Math.Min(_point1.X, _point2.X) && point.X <= Math.Max(_point1.X, _point2.X) &&
                   point.Y >= Math.Min(_point1.Y, _point2.Y) && point.Y <= Math.Max(_point1.Y, _point2.Y);
        }
        public Line GetLine()
        {
            var slope = GetSlope();
            var intercept = GetIntercept(slope);
            return new Line(slope, intercept);
        }
        
        private float GetSlope()
        {
            return (_point2.Y - _point1.Y) / (_point2.X - _point1.X);
        }

        private float GetIntercept(float slope)
        {
            return _point1.Y - slope * _point1.X;
        }
    }
    
}