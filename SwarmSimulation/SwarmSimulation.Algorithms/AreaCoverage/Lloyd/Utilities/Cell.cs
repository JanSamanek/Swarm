using System.Collections.Generic;
using System.Numerics;

namespace SwarmSimulation.Algorithms.AreaCoverage.Lloyd.Utilities
{
    public class Cell
    {
        private readonly List<Vector2> _vertexPositions = new List<Vector2>();

        public void AddVertex(Vector2 vertex)
        {
            _vertexPositions.Add(vertex);
        }

        public List<Vector2> GetVertexPositions()
        {
            return _vertexPositions;
        }
    }
}