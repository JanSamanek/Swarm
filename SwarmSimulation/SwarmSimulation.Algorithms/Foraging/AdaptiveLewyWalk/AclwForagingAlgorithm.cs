using System;
using System.Linq;
using System.Numerics;
using SwarmSimulation.Algorithms.Agents;
using SwarmSimulation.Algorithms.Agents.Foraging;
using SwarmSimulation.Algorithms.Foraging.AdaptiveLewyWalk.States;
using SwarmSimulation.Algorithms.Other.MoveToTarget;

namespace SwarmSimulation.Algorithms.Foraging.AdaptiveLewyWalk
{
    public class AclwForagingAlgorithm : IAlgorithm<AclwForagingAlgorithmInput>
    {
        private readonly MoveToTargetAlgorithm _moveToTargetAlgorithm;
        private readonly AclwForagingAlgorithmSettings _settings;
        
        public AclwForagingAlgorithm(AclwForagingAlgorithmSettings settings)
        {
            _moveToTargetAlgorithm =
                new MoveToTargetAlgorithm();
            _settings = settings;
            AclwExploring.Configure(settings);
        }

        public Vector2 CalculateControlInput(Agent agent, AclwForagingAlgorithmInput input)
        {
            var foragingAgent = (AclwForagingAgent) agent;
            foragingAgent.State.Execute(foragingAgent);
            
            var moveInput = new MoveToTargetAlgorithmInput
            {
                Speed = input.MoveSpeed,
                TargetPosition = foragingAgent.Target,
            };
            var controlInput = _moveToTargetAlgorithm.CalculateControlInput(foragingAgent, moveInput);

            if (!foragingAgent.IsPerformingLongFlight)
            {
                controlInput += LongFlightRepulsion(foragingAgent);
            }
            
            return controlInput;
        }

        private Vector2 LongFlightRepulsion(AclwForagingAgent foragingAgent)
        {
            var controlInput = Vector2.Zero;
            
            var longFlightNeighborsCount =
                foragingAgent.Neighbors.Count(n => ((AclwForagingAgent)n).IsPerformingLongFlight);
            if (longFlightNeighborsCount == 0)
            {
                return controlInput;
            }
            
            foreach (var neighbor in foragingAgent.Neighbors)
            {
                var foragingNeighbor = (AclwForagingAgent) neighbor;
                if (foragingNeighbor.IsPerformingLongFlight)
                {
                    var distanceVector = foragingAgent.Position - neighbor.Position;
                    var distance = Vector2.Distance(foragingAgent.Position, foragingNeighbor.Position);

                    controlInput += _settings.RepulsionGain * distanceVector / (float)Math.Pow(distance, 3) *
                                    (1 / distance - 1 / foragingAgent.PerceptionRange);
                }
            }
            
            return controlInput/ longFlightNeighborsCount;
        }
    }
}