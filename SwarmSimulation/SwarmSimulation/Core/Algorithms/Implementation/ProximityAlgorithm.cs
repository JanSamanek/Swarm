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
        public void ConfigureSettings(ProximityAlgorithmSettings settings)
        {
            Settings = settings;
        }

        public Vector2 CalculateControlInput(RegularAgent agent, ProximityAlgorithmInput input)
        {
            var closestNeighbours = agent.Neighbors
                .OrderBy(neighbour => Vector2.Distance(neighbour.Position, agent.Position))
                .Take(input.NeighboursToCalculateFrom);
            
            var acceleration = Vector2.Zero;
            foreach (var neighbour in closestNeighbours)
            {
                var distanceVector = agent.Position - neighbour.Position;
                var desiredDistanceVector = Vector2.Normalize(distanceVector) * input.DesiredDistance;
                var relativeVelocity = agent.Velocity - neighbour.Velocity;
                acceleration += Settings.StiffnessCoefficient * (desiredDistanceVector-distanceVector) 
                                + Settings.DampingCoefficient * relativeVelocity;
            }
            var dt = SimulationTimeManager.GetDeltaTime();
            var controlInput = acceleration * dt;
            return controlInput;
        }
    }
}