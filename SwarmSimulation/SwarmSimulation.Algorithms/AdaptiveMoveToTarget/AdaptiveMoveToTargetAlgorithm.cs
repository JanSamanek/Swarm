using System.Numerics;
using SwarmSimulation.Algorithms.Agents;
using SwarmSimulation.Algorithms.MoveToTarget;
using SwarmSimulation.Algorithms.ObstacleAvoidanceAPF;

namespace SwarmSimulation.Algorithms.AdaptiveMoveToTarget
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

        public Vector2 CalculateControlInput(IAgent agent, AdaptiveMoveToTargetAlgorithmInput input)
        {
            var controlInput = Vector2.Zero;
            controlInput += _moveToTargetAlgorithm.CalculateControlInput(agent, input.MoveToTargetAlgorithmInput);
            controlInput += _obstacleAvoidanceAlgorithm.CalculateControlInput(agent, input.ObstacleAvoidanceAlgorithmInput);
            return controlInput;
        }
    }
}