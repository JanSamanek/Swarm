using System.Numerics;
using SwarmSimulation.Agents;

namespace SwarmSimulation.Algorithms.MoveToTarget
{
    public class MoveToTargetAlgorithm : IAlgorithm<MoveToTargetAlgorithmInput>
    {
        private readonly MoveToTargetAlgorithmSettings _settings; 
        public MoveToTargetAlgorithm(MoveToTargetAlgorithmSettings settings)
        {
            _settings = settings;
        }

        public Vector2 CalculateControlInput(IAgent agent, MoveToTargetAlgorithmInput input)
        {
            var distanceVector = input.TargetPosition - agent.Position;
            if (distanceVector.Length() < _settings.TargetPositionTolerance)
            {
                return Vector2.Zero;
            }
            var direction = Vector2.Normalize(distanceVector);
            return input.Speed * direction;
        }
    }
}