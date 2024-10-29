using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using SwarmSimulation.Algorithms.Agents;
using SwarmSimulation.Utilities.Extensions;

namespace SwarmSimulation.Algorithms.LineAggregation
{
    public class LineAggregationAlgorithm : IAlgorithm<LineAggregationAlgorithmInput>
    {
        private readonly LineAggregationAlgorithmSettings _settings;
        public LineAggregationAlgorithm(LineAggregationAlgorithmSettings settings)
        {
            _settings = settings;
        }
        public Vector2 CalculateControlInput(Agent agent, LineAggregationAlgorithmInput input)
        {
            var orientationAngle = input.LineOrientationAngleInRadians;

            var adjacent = GetAdjacentAgents(agent, orientationAngle);
            
            var controlInputParallel = Vector2.Zero;
            var controlInputPerpendicular = Vector2.Zero;
            foreach (var neighbour in adjacent)
            {
                var distance = (neighbour.Position - agent.Position).Rotate(orientationAngle);
                var parallelDistance = new Vector2(0, distance.Y);
                var perpendicularDistance = new Vector2(distance.X, 0);
                
                controlInputParallel += _settings.GainParallel * parallelDistance;
                controlInputPerpendicular += _settings.GainPerpendicular 
                                             * (perpendicularDistance.Length() - input.DesiredDistance)
                                             * perpendicularDistance;
            }
            
            var controlInput = controlInputParallel + controlInputPerpendicular;
            return controlInput.Rotate(-orientationAngle);
        }
        private static IEnumerable<Agent> GetAdjacentAgents(Agent agent, float orientationAngle)
        {
            var closest = agent.Neighbors
                .OrderBy(neighbour 
                    => Math.Abs((neighbour.Position - agent.Position).Rotate(orientationAngle).X))
                .ToList();

            var closestRight = closest.FirstOrDefault(a => 
                a.Position.Rotate(orientationAngle).X > agent.Position.Rotate(orientationAngle).X);
            var closestLeft = closest.FirstOrDefault(a => 
                a.Position.Rotate(orientationAngle).X < agent.Position.Rotate(orientationAngle).X);

            var adjacent = new List<Agent>();
            if (closestRight != null)
            {
                adjacent.Add(closestRight);
            }

            if (closestLeft != null)
            {
                adjacent.Add(closestLeft);
            }
            return adjacent;
        }

    }
}