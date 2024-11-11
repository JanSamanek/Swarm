using System;
using System.Collections.Generic;
using System.Numerics;
using SwarmSimulation.Algorithms.Agents;
using SwarmSimulation.Algorithms.AreaCoverage.Lloyd.Utilities;
using SwarmSimulation.Utilities;

namespace SwarmSimulation.Algorithms.AreaCoverage.Lloyd
{
    public class LloydsAlgorithm : IAlgorithm<LloydsAlgorithmInput>
    {
        public Vector2 CalculateControlInput(Agent agent, LloydsAlgorithmInput input)
        {
            
        }

        private Cell CreateVoronoi(Agent agent, Cell cell)
        {
            foreach (var neighbour in agent.Neighbours)
            {
                var distanceVector = neighbour.Position - agent.Position;
                var midPointFromAgent = distanceVector / 2;
                var perpendicularBisectorDirection = Vector2.Normalize(distanceVector.Rotate((float)Math.PI / 2));
            }
        }
        
        
    }
}