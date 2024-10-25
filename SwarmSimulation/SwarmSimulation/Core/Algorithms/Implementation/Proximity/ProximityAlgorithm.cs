using System;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using SwarmSimulation.Core.Agents.Contracts;
using SwarmSimulation.Core.Agents.Implementation;
using SwarmSimulation.Core.Algorithms.Contracts;
using SwarmSimulation.Utilities;

namespace SwarmSimulation.Core.Algorithms.Implementation.Proximity
{
    public class ProximityAlgorithm : IAlgorithm<ProximityAlgorithmInput>
    {
        private readonly ProximityAlgorithmSettings _settings;
        public ProximityAlgorithm(ProximityAlgorithmSettings settings)
        {
            _settings = settings;
        }
        public Vector2 CalculateControlInput(IAgent agent, ProximityAlgorithmInput input)
        {
            var toCalculateFrom = agent.Neighbors
                .OrderBy(neighbour => Vector2.Distance(neighbour.Position, agent.Position))
                .Take(input.NeighboursToCalculateFrom)
                .ToList();

            var nearbyLeaders = agent.Neighbors.OfType<LeaderAgent>().ToList();
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