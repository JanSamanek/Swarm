using System.Numerics;

namespace SwarmSimulation.Core.Agents.Implementation
{
    public class LeaderAgent : Agent
    {
        public LeaderAgent(int id, Vector2 position, int perceptionRange)
            : base(id, position, perceptionRange)
        {
        }

        private Vector2 CalculateControlInputToTarget(Vector2 targetPosition, float speedToTarget)
        {
            if (targetPosition == Position)
            {
                return Vector2.Zero;
            }
            var direction = Vector2.Normalize(targetPosition - Position);
            return speedToTarget * direction;
        }
        
        public void MoveToTarget(Vector2 targetPosition, float speedToTarget)
        {
            var controlInput = CalculateControlInputToTarget(targetPosition, speedToTarget);
            Move(controlInput);
        }
    }
}