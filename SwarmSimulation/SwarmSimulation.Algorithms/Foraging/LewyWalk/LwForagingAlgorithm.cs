using System;
using System.Numerics;
using SwarmSimulation.Algorithms.Agents;
using SwarmSimulation.Algorithms.Agents.Foraging;
using SwarmSimulation.Algorithms.Foraging.LewyWalk.States;
using SwarmSimulation.Algorithms.Other.MoveToTarget;

namespace SwarmSimulation.Algorithms.Foraging.LewyWalk
{
    public class LwForagingAlgorithm : IAlgorithm<LwForagingAlgorithmInput>
    {
        private readonly MoveToTargetAlgorithm _moveToTargetAlgorithm;
        
        public LwForagingAlgorithm(LwForagingAlgorithmSettings settings)
        {
            _moveToTargetAlgorithm =
                new MoveToTargetAlgorithm();
            LwExploring.Configure(settings);
        }

        public Vector2 CalculateControlInput(Agent agent, LwForagingAlgorithmInput input)
        {
            if (!(agent is LwForagingAgent foragingAgent))
            {
                throw new Exception("Agent must be an LwForagingAgent");
            }
            
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