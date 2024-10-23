using System.Numerics;
using SwarmSimulation.Core.Agents.Implementation;
using SwarmSimulation.Core.Algorithms.Contracts;

namespace SwarmSimulation.Core.Algorithms.Implementation.CustomFormation
{
    public class FormationAlgorithm : IAlgorithm<FormationAlgorithmSettings, FormationAlgorithmInput>
    {
        public FormationAlgorithmSettings Settings { get; set; }
        public FormationAlgorithm(FormationAlgorithmSettings settings)
        {
            Settings = settings;
        }
        public Vector2 CalculateControlInput(RegularAgent agent, FormationAlgorithmInput input)
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