using System.Numerics;
using SwarmSimulation.Algorithms.Agents;

namespace SwarmSimulation.Algorithms.Other.MoveToTarget
{
    public class MoveToTargetAlgorithm : IAlgorithm<MoveToTargetAlgorithmInput>
    {
        public Vector2 CalculateControlInput(Agent agent, MoveToTargetAlgorithmInput input)
        {
            if (agent.HasApproachedTarget(input.TargetPosition))
            {
                return Vector2.Zero;
            }

            var distanceVector = input.TargetPosition - agent.Position;
            var direction = Vector2.Normalize(distanceVector);
            return input.Speed * direction;
        }
    }
}