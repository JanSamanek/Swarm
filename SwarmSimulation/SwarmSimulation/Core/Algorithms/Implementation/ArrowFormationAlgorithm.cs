using System;
using System.Numerics;
using SwarmSimulation.Core.Agents.Implementation;
using SwarmSimulation.Core.Algorithms.Contracts;
using SwarmSimulation.Core.Algorithms.Inputs;
using SwarmSimulation.Core.Algorithms.Settings;

namespace SwarmSimulation.Core.Algorithms.Implementation
{
    public class ArrowFormationAlgorithm : IAlgorithm<ArrowFormationAlgorithmSettings, ArrowFormationAlgorithmInput>
    {
        public ArrowFormationAlgorithmSettings Settings { get; set; }
        public void ConfigureSettings(ArrowFormationAlgorithmSettings settings)
        {
            Settings = settings;
        }

        public Vector2 CalculateControlInput(RegularAgent agent, ArrowFormationAlgorithmInput input)
        {
            var controlInput = Vector2.Zero;
            foreach (var neighbour in agent.Neighbors)
            {
                var desiredDistance = input.DesiredInterAgentDistances[agent.Id, neighbour.Id];
                var distanceVector = neighbour.Position - agent.Position;
                controlInput += Settings.ApfGain * distanceVector / distanceVector.Length() *
                                (1 - desiredDistance / distanceVector.Length());
            }
            return controlInput;
        }
    }
}