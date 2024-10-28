using System;
using System.Numerics;
using SwarmSimulation.Agents;
using SwarmSimulation.Agents.Foraging;
using SwarmSimulation.Agents.Foraging.States;
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
                new MoveToTargetAlgorithm(settings.MoveToTargetAlgorithmSettings);
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
        
        private Vector2 GenerateNewDirection()
        {
            var randomAngle = (float) (_random.NextDouble() * 2 * Math.PI);
            var baseVector = new Vector2(0, 1);
            return Vector2.Normalize(baseVector.Rotate(randomAngle));
        }
    }
    
}