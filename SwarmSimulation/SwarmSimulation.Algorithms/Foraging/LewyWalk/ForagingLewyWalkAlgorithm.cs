using System;
using System.Numerics;
using SwarmSimulation.Algorithms.Agents;
using SwarmSimulation.Algorithms.Foraging.States;
using SwarmSimulation.Algorithms.MoveToTarget;
using SwarmSimulation.Utilities.Extensions;

namespace SwarmSimulation.Algorithms.Foraging.LewyWalk
{
    public class ForagingLewyWalkAlgorithm : IAlgorithm<ForagingLewyWalkAlgorithmInput>
    {
        private readonly ForagingLewyWalkAlgorithmSettings _settings;
        private readonly MoveToTargetAlgorithm _moveToTargetAlgorithm;
        private readonly Random _random = new Random();
        
        public ForagingLewyWalkAlgorithm(ForagingLewyWalkAlgorithmSettings settings)
        {
            _settings = settings;
            _moveToTargetAlgorithm =
                new MoveToTargetAlgorithm();
            Exploring.ConfigureLewyWalk(settings.LewyParameter, settings.MaxFlightLength, settings.LewyScale);
        }

        public Vector2 CalculateControlInput(IAgent agent, ForagingLewyWalkAlgorithmInput input)
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