using System.Collections.Generic;
using System.Numerics;

namespace SwarmSimulation.Algorithms.AreaCoverage.Lloyd.Utilities
{
    public class Cell
    {
        private Dictionary<> orderedSegments = new Dictionary<, >();
        private readonly List<Vector2> _vertexes = new List<Vector2>();

        public void AddVertex(Vector2 vertex)
        {
            _vertexes.Add(vertex);
        }

        public void RemoveVertex(Vector2 vertex)
        {
            _vertexes.Remove(vertex);
        }

        public List<Vector2> GetVertexes()
        {
            return _vertexes;
        }
    }
}