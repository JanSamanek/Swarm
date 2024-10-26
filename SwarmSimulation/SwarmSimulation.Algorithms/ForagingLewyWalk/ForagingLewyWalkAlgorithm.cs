using System.Numerics;
using SwarmSimulation.Agents.Agents.Contracts;

namespace SwarmSimulation.Algorithms.ForagingLewyWalk
{
    public class ForagingLewyWalkAlgorithm : IAlgorithm<ForagingLewyWalkAlgorithmInput>
    {
        private ForagingLewyWalkAlgorithmSettings _settings;

        public ForagingLewyWalkAlgorithm(ForagingLewyWalkAlgorithmSettings settings)
        {
            _settings = settings;
        }

        public Vector2 CalculateControlInput(IAgent agent, ForagingLewyWalkAlgorithmInput input)
        {
            throw new System.NotImplementedException();
        }
    }
}