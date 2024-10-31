using System;
using System.Numerics;
using SwarmSimulation.Algorithms.Agents;
using SwarmSimulation.Algorithms.Foraging.States;
using SwarmSimulation.Algorithms.MoveToTarget;

namespace SwarmSimulation.Algorithms.Foraging.LewyWalk
{
    public class ForagingLewyWalkAlgorithm : IAlgorithm<ForagingLewyWalkAlgorithmInput>
    {
        private readonly MoveToTargetAlgorithm _moveToTargetAlgorithm;
        
        public ForagingLewyWalkAlgorithm(ForagingLewyWalkAlgorithmSettings settings)
        {
            _moveToTargetAlgorithm =
                new MoveToTargetAlgorithm();
            Exploring.ConfigureLewyWalk(settings.LewyParameter, settings.MaxFlightLength, settings.LewyScale);
        }

        public Vector2 CalculateControlInput(Agent agent, ForagingLewyWalkAlgorithmInput input)
        {
            var foragingAgent = (ForagingAgent) agent;

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