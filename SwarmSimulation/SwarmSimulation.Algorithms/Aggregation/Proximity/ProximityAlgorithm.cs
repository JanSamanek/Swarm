using System;
using System.Linq;
using System.Numerics;
using SwarmSimulation.Algorithms.Agents;
using SwarmSimulation.Algorithms.Agents.Formation;
using SwarmSimulation.Utilities;

namespace SwarmSimulation.Algorithms.Aggregation.Proximity
{
    public class ProximityAlgorithm : IAlgorithm<ProximityAlgorithmInput>
    {
        private readonly ProximityAlgorithmSettings _settings;
        public ProximityAlgorithm(ProximityAlgorithmSettings settings)
        {
            _settings = settings;
        }
        public Vector2 CalculateControlInput(Agent agent, ProximityAlgorithmInput input)
        {
            var toCalculateFrom = agent.Neighbours
                .OrderBy(neighbour => Vector2.Distance(neighbour.Position, agent.Position))
                .Take(input.NeighboursToCalculateFrom)
                .ToList();

            var nearbyLeaders = agent.Neighbours.OfType<LeaderAgent>().ToList();
            if (nearbyLeaders.Any())
            {
                var random = new Random();
                var randomIndex = random.Next(nearbyLeaders.Count);
                var selectedLeader = nearbyLeaders[randomIndex];
                toCalculateFrom.Add(selectedLeader);
            }
            
            var acceleration = Vector2.Zero;
            foreach (var neighbour in toCalculateFrom)
            {
                var distanceVector = agent.Position - neighbour.Position;
                var desiredDistanceVector = Vector2.Normalize(distanceVector) * input.DesiredDistance;
                var relativeVelocity = agent.Velocity - neighbour.Velocity;

                var stiffnessCoefficient = neighbour is LeaderAgent
                    ? _settings.LeaderStiffnessCoefficient
                    : _settings.InterAgentStiffnessCoefficient;
                var dampingCoefficient = _settings.DampingCoefficient;
                
                var springContribution = stiffnessCoefficient * (desiredDistanceVector - distanceVector);
                var damperContribution =+ dampingCoefficient * relativeVelocity;
                
                acceleration += springContribution + damperContribution;
            }
            var dt = SimulationTimeManager.GetDeltaTime();
            var controlInput = acceleration * dt;
            return controlInput;
        }
    }
}