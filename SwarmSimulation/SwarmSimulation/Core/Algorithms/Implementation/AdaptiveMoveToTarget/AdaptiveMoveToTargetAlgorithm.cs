using System.Numerics;
using SwarmSimulation.Core.Agents.Contracts;
using SwarmSimulation.Core.Algorithms.Contracts;
using SwarmSimulation.Core.Algorithms.Implementation.MoveToTarget;
using SwarmSimulation.Core.Algorithms.Implementation.ObstacleAvoidanceAPF;

namespace SwarmSimulation.Core.Algorithms.Implementation.AdaptiveMoveToTarget
{
    public class AdaptiveMoveToTargetAlgorithm : IAlgorithm<AdaptiveMoveToTargetAlgorithmInput>
    {
        private readonly IAlgorithm<ObstacleAvoidanceAlgorithmInput> _obstacleAvoidanceAlgorithm;
        private readonly IAlgorithm<MoveToTargetAlgorithmInput> _moveToTargetAlgorithm;

        public AdaptiveMoveToTargetAlgorithm(AdaptiveMoveToTargetAlgorithmSettings settings)
        {
            _obstacleAvoidanceAlgorithm = new ObstacleAvoidanceAlgorithm(settings.ObstacleAvoidanceAlgorithmSettings);
            _moveToTargetAlgorithm = new MoveToTargetAlgorithm(settings.MoveToTargetAlgorithmSettings);
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