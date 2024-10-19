using System.Linq;
using System.Numerics;
using SwarmSimulation.Settings;
using SwarmSimulation.Utilities;

namespace SwarmSimulation.Core.Algorithms
{
    public class DispersionAlgorithm : IAlgorithm<DispersionAlgorithmSettings>
    { 
        public Vector2 CalculateControlInput(Agent agent, DispersionAlgorithmSettings settings)
        {
            var closestNeighbours = agent.Neighbors
                .OrderBy(neighbour => Vector2.Distance(neighbour.Position, agent.Position))
                .Take(settings.NeighboursToCalculateFrom);
            
            var acceleration = Vector2.Zero;
            foreach (var neighbour in closestNeighbours)
            {
                var distanceVector = agent.Position - neighbour.Position;
                var desiredDistanceVector = Vector2.Normalize(distanceVector) * settings.DesiredDistance;
                var relativeVelocity = agent.Velocity - neighbour.Velocity;
                acceleration += settings.StiffnessCoefficient * (desiredDistanceVector-distanceVector) 
                                + settings.DampingCoefficient * relativeVelocity;
            }
            var dt = SimulationTimeManager.GetDeltaTime();
            var controlInput = acceleration * dt;
            return controlInput;
        }
    }
}