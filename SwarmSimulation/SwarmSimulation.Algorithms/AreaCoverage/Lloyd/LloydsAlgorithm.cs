using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using SwarmSimulation.Algorithms.Agents;
using SwarmSimulation.Algorithms.Agents.AreaCoverage;
using SwarmSimulation.Algorithms.AreaCoverage.Lloyd.Utilities;
using SwarmSimulation.Utilities;

namespace SwarmSimulation.Algorithms.AreaCoverage.Lloyd
{
    public class LloydsAlgorithm : IAlgorithm<LloydsAlgorithmInput>
    {
        public Vector2 CalculateControlInput(Agent agent, LloydsAlgorithmInput input)
        {
            if (!(agent is VoronoiAgent voronoiAgent))
            {
                throw new Exception("Agent is not a VoronoiAgent");
            }

            if (voronoiAgent.VoronoiCell == null)
            {
                voronoiAgent.VoronoiCell = InitializeVoronoi(voronoiAgent);
            }
        }

        private Cell UpdateVoronoi(VoronoiAgent agent)
        {
            var voronoi = agent.VoronoiCell;
            foreach (var neighbour in agent.Neighbours)
            {
                var midPointFromAgent = (neighbour.Position - agent.Position) / 2;
                var bisectorDirection = Vector2.Normalize(midPointFromAgent.Rotate((float)Math.PI / 2));
                var slope = bisectorDirection.X / bisectorDirection.Y;
                var intercept = midPointFromAgent.Y - midPointFromAgent.X * slope;
                var bisector = new Line(slope, intercept);
                
            }
        }

        private Cell InitializeVoronoi(Agent agent, float padding = 1f)
        {
            // coordinate system x to the right, y down, from the top left corner
            var leftMost = agent.Neighbours.OrderBy(n => n.Position.X).First().Position.X - padding;
            var rightMost = agent.Neighbours.OrderByDescending(n => n.Position.X).First().Position.X + padding;
            var topMost = agent.Neighbours.OrderBy(n => n.Position.Y).First().Position.Y - padding;
            var bottomMost = agent.Neighbours.OrderByDescending(n => n.Position.Y).First().Position.Y + padding;

            if (leftMost > agent.Position.X)
            {
                leftMost = agent.Position.X - -agent.PerceptionRange - padding;
            }

            if (rightMost < agent.Position.X)
            {
                rightMost = agent.Position.X + agent.PerceptionRange + padding;
            }

            if (topMost > agent.Position.Y)
            {
                topMost = agent.Position.Y - -agent.PerceptionRange - padding;
            }

            if (bottomMost < agent.Position.Y)
            {
                bottomMost = agent.Position.Y + agent.PerceptionRange + padding;
            }
            
            var voronoi = new Cell();
            // order important!
            voronoi.AddVertex(new Vector2(leftMost, topMost));
            voronoi.AddVertex(new Vector2(rightMost, topMost));
            voronoi.AddVertex(new Vector2(rightMost, bottomMost));
            voronoi.AddVertex(new Vector2(leftMost, bottomMost));
            
            return voronoi;
        }

        private Vector2 CalculateCenterOfVoronoi(Cell voronoi)
        {
            
        }
        
    }
}