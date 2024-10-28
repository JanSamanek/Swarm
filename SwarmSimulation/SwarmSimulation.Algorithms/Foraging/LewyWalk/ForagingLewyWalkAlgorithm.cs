using System;
using System.Numerics;
using SwarmSimulation.Agents;
using SwarmSimulation.Agents.Foraging;
using SwarmSimulation.Agents.Foraging.States;
using SwarmSimulation.Algorithms.AdaptiveMoveToTarget;
using SwarmSimulation.Algorithms.MoveToTarget;
using SwarmSimulation.Algorithms.ObstacleAvoidanceAPF;
using SwarmSimulation.Utilities.Extensions;

namespace SwarmSimulation.Algorithms.Foraging.LewyWalk
{
    public class ForagingLewyWalkAlgorithm : IAlgorithm<ForagingLewyWalkAlgorithmInput>
    {
        private readonly ForagingLewyWalkAlgorithmSettings _settings;
        private readonly AdaptiveMoveToTargetAlgorithm _adaptiveMoveToTargetAlgorithm;
        private readonly Random _random = new Random();
        
        public ForagingLewyWalkAlgorithm(ForagingLewyWalkAlgorithmSettings settings)
        {
            _settings = settings;
            _adaptiveMoveToTargetAlgorithm =
                new AdaptiveMoveToTargetAlgorithm(settings.AdaptiveMoveToTargetAlgorithmSettings);
            Exploring.ConfigureLewyWalk(settings.LewyParameter, settings.MaxFlightLength, settings.LewyScale);
        }

        public Vector2 CalculateControlInput(IAgent agent, ForagingLewyWalkAlgorithmInput input)
        {
            var foragingAgent = (ForagingAgent) agent;

            foragingAgent.State.Execute(foragingAgent);
            
            var moveInput = new AdaptiveMoveToTargetAlgorithmInput
            {
                ObstacleAvoidanceAlgorithmInput = new ObstacleAvoidanceAlgorithmInput {
                    Distance = input.ObstacleAvoidanceAlgorithmInput.Distance,
                },
                MoveToTargetAlgorithmInput = new MoveToTargetAlgorithmInput {
                    Speed = input.MoveSpeed,
                    TargetPosition = foragingAgent.Target,
                }
            };
            var controlInput = _adaptiveMoveToTargetAlgorithm.CalculateControlInput(foragingAgent, moveInput);
            
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