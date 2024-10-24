using System.Numerics;
using SwarmSimulation.Core.Agents.Contracts;

namespace SwarmSimulation.Core.Agents.Implementation
{
    public class LeaderAgent : Agent, IAgent
    {
        public LeaderAgent(int id, Vector2 position, float perceptionRange)
            : base(id, position, perceptionRange)
        {
        }

        private Vector2 CalculateControlInputToTarget(Vector2 targetPosition, float speedToTarget)
        {
            var distanceVector = targetPosition - Position;
            const int error = 2;
            if (distanceVector.Length() < error)
            {
                return Vector2.Zero;
            }
            var direction = Vector2.Normalize(distanceVector);
            return speedToTarget * direction;
        }
        
        public void MoveToTarget(Vector2 targetPosition, float speedToTarget)
        {
            var controlInput = CalculateControlInputToTarget(targetPosition, speedToTarget);
            Move(controlInput);
        }
    }
}