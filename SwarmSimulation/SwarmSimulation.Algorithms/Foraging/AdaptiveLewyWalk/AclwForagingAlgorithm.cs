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
            if (!(agent is AclwForagingAgent foragingAgent))
            {
                throw new ArgumentException("Agent must be an AclwForagingAgent");
            }

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
            
            var longFlightNeighboursCount =
                foragingAgent.Neighbours.Count(n => ((AclwForagingAgent)n).IsPerformingLongFlight);
            if (longFlightNeighboursCount == 0)
            {
                return controlInput;
            }
            
            foreach (var neighbour in foragingAgent.Neighbours)
            {
                var foragingNeighbour = (AclwForagingAgent) neighbour;
                if (foragingNeighbour.IsPerformingLongFlight)
                {
                    var distanceVector = foragingAgent.Position - neighbour.Position;
                    var distance = Vector2.Distance(foragingAgent.Position, foragingNeighbour.Position);

                    controlInput += _settings.RepulsionGain * distanceVector / (float)Math.Pow(distance, 3) *
                                    (1 / distance - 1 / foragingAgent.PerceptionRange);
                }
            }
            
            return controlInput/ longFlightNeighboursCount;
        }
    }
}