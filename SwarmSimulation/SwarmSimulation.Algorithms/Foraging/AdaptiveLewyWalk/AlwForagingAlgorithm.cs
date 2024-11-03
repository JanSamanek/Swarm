using System.Numerics;
using SwarmSimulation.Algorithms.Agents;
using SwarmSimulation.Algorithms.Agents.Foraging;
using SwarmSimulation.Algorithms.Foraging.AdaptiveLewyWalk.States;
using SwarmSimulation.Algorithms.Foraging.LewyWalk;
using SwarmSimulation.Algorithms.Foraging.LewyWalk.States;
using SwarmSimulation.Algorithms.Other.MoveToTarget;

namespace SwarmSimulation.Algorithms.Foraging.AdaptiveLewyWalk
{
    public class AlwForagingAlgorithm : IAlgorithm<AlwForagingAlgorithmInput>
    {
        private readonly MoveToTargetAlgorithm _moveToTargetAlgorithm;
        
        public AlwForagingAlgorithm(AlwForagingAlgorithmSettings settings)
        {
            _moveToTargetAlgorithm =
                new MoveToTargetAlgorithm();
            AlwExploring.Configure(settings);
        }

        public Vector2 CalculateControlInput(Agent agent, AlwForagingAlgorithmInput input)
        {
            var foragingAgent = (AlwForagingAgent) agent;

            foragingAgent.State.Execute(foragingAgent);
            
            var moveInput = new MoveToTargetAlgorithmInput
            {
                Speed = input.MoveSpeed,
                TargetPosition = foragingAgent.Target,
            };
            var controlInput = _moveToTargetAlgorithm.CalculateControlInput(foragingAgent, moveInput);
            return controlInput;
        } 
    }
}