using System;
using System.Linq;
using System.Numerics;
using SwarmSimulation.Core.Agents;
using SwarmSimulation.Core.Agents.Implementation;
using SwarmSimulation.Core.Algorithms.Contracts;
using SwarmSimulation.Core.Algorithms.Inputs;
using SwarmSimulation.Core.Algorithms.Settings;
using SwarmSimulation.Utilities;

namespace SwarmSimulation.Core.Algorithms.Implementation
{
    public class ProximityAlgorithm : IAlgorithm<ProximityAlgorithmSettings, ProximityAlgorithmInput>
    {
        public ProximityAlgorithmSettings Settings { get; set; }
        public ProximityAlgorithm(ProximityAlgorithmSettings settings)
        {
            Settings = settings;
        }
        public Vector2 CalculateControlInput(RegularAgent agent, ProximityAlgorithmInput input)
        {
            var toCalculateFrom = agent.Neighbors
                .OrderBy(neighbour => Vector2.Distance(neighbour.Position, agent.Position))
                .Take(input.NeighboursToCalculateFrom)
                .ToList();

            var nearbyLeaders = agent.Neighbors.Where(a => a is LeaderAgent).ToList();
            if (nearbyLeaders.Any())
            {
                var random = new Random();
                var randomIndex = random.Next(nearbyLeaders.Count);
                var selectedLeader = (LeaderAgent) nearbyLeaders[randomIndex];
                toCalculateFrom.Add(selectedLeader);
            }
            
            var acceleration = Vector2.Zero;
            foreach (var neighbour in toCalculateFrom)
            {
                var distanceVector = agent.Position - neighbour.Position;
                var desiredDistanceVector = Vector2.Normalize(distanceVector) * input.DesiredDistance;
                var relativeVelocity = agent.Velocity - neighbour.Velocity;

                var stiffnessCoefficient = neighbour is LeaderAgent
                    ? Settings.LeaderStiffnessCoefficient
                    : Settings.InterAgentStiffnessCoefficient;
                var dampingCoefficient = Settings.DampingCoefficient;
                
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