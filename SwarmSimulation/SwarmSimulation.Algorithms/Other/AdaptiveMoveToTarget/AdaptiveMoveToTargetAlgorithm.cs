using System.Numerics;
using SwarmSimulation.Algorithms.Agents;
using SwarmSimulation.Algorithms.Other.MoveToTarget;
using SwarmSimulation.Algorithms.Other.ObstacleAvoidanceAPF;

namespace SwarmSimulation.Algorithms.Other.AdaptiveMoveToTarget
{
    public class AdaptiveMoveToTargetAlgorithm : IAlgorithm<AdaptiveMoveToTargetAlgorithmInput>
    {
        private readonly IAlgorithm<ObstacleAvoidanceAlgorithmInput> _obstacleAvoidanceAlgorithm;
        private readonly IAlgorithm<MoveToTargetAlgorithmInput> _moveToTargetAlgorithm;

        public AdaptiveMoveToTargetAlgorithm(AdaptiveMoveToTargetAlgorithmSettings settings)
        {
            _obstacleAvoidanceAlgorithm = new ObstacleAvoidanceAlgorithm(settings.ObstacleAvoidanceAlgorithmSettings);
            _moveToTargetAlgorithm = new MoveToTargetAlgorithm();
        }

        public Vector2 CalculateControlInput(Agent agent, AdaptiveMoveToTargetAlgorithmInput input)
        {
            var controlInput = Vector2.Zero;
            controlInput += _moveToTargetAlgorithm.CalculateControlInput(agent, input.MoveToTargetAlgorithmInput);
            controlInput += _obstacleAvoidanceAlgorithm.CalculateControlInput(agent, input.ObstacleAvoidanceAlgorithmInput);
            return controlInput;
        }
    }
}