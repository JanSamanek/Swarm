using System.Numerics;
using SwarmSimulation.Algorithms.Agents;

namespace SwarmSimulation.Algorithms.CustomFormation
{
    public class FormationAlgorithm : IAlgorithm<FormationAlgorithmInput>
    {
        private readonly FormationAlgorithmSettings _settings; 
        public FormationAlgorithm(FormationAlgorithmSettings settings)
        {
            _settings = settings;
        }
        public Vector2 CalculateControlInput(Agent agent, FormationAlgorithmInput input)
        {
            var controlInput = Vector2.Zero;
            foreach (var neighbour in agent.Neighbors)
            {
                var desiredDistance = input.DesiredInterAgentDistances[agent.Id, neighbour.Id];
                var distanceVector = neighbour.Position - agent.Position;
                controlInput += _settings.ApfGain * distanceVector / distanceVector.Length() *
                                (1 - desiredDistance / distanceVector.Length());
            }
            return controlInput;
        }
    }
}