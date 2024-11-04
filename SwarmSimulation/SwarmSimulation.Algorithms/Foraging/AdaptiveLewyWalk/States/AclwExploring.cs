using System;
using System.Linq;
using MathNet.Numerics;
using SwarmSimulation.Algorithms.Agents.Foraging;
using SwarmSimulation.Algorithms.Utilities.Generator;
using SwarmSimulation.Utilities;

namespace SwarmSimulation.Algorithms.Foraging.AdaptiveLewyWalk.States
{
    public class AclwExploring : IState<AclwForagingAgent>
    {
        private static float _maxFlightLength;
        private static float _longFlightThreshold;
        private static float _lewyScale;
        private static int _maxExplorationAttempts;
        private static float _brownianToLewyTime;

        private static float _beta;
        private static int _brownianToLewyTicks;
        
        public AclwExploring(AclwForagingAgent agent)
        {
            _brownianToLewyTicks = (int)(_brownianToLewyTime / SimulationTimeManager.GetDeltaTime());
            _beta = (float) 2 / _brownianToLewyTicks;
            OnEnter(agent);
        }
        
        private void OnEnter(AclwForagingAgent agent)
        {
            var lewyParameter = UpdateLewyParameter(agent.TicksFromLastSuccessfulExploration);
            var flight = LewyMotionGenerator.GenerateFlight(lewyParameter, 1, _maxFlightLength, _lewyScale);
            agent.IsPerformingLongFlight = flight.Length() >= _longFlightThreshold;
            agent.Target = agent.Position + flight;
        }

        public void Execute(AclwForagingAgent agent)
        {
            agent.TicksFromLastSuccessfulExploration++;
            
            var resources = agent.DetectResources().ToList();
            if (resources.Any())
            {
                agent.TicksFromLastSuccessfulExploration = 0;
                agent.State = new AclwHarvesting(agent, resources.First());
                return;
            }

            if (agent.UnsuccessfulExplorationAttempts >= _maxExplorationAttempts)
            {
                agent.State = new AclwReturningToNest(agent);
            }
            
            if (agent.HasApproachedTarget(agent.Target) || agent.DetectCollision())
            {
                agent.UnsuccessfulExplorationAttempts++;
                agent.State = new AclwExploring(agent);
            }
        }
        
        private static float UpdateLewyParameter(int ticksFromLastSuccessfulExploration)
        {
            var lewyParameter = Math.Max(1,
                SpecialFunctions.Erfc(_beta * (ticksFromLastSuccessfulExploration - _brownianToLewyTicks)));
            return (float) lewyParameter;
        }

        public static void Configure(AclwForagingAlgorithmSettings settings)
        {
            _maxExplorationAttempts = settings.MaxExploringAttempts;
            _brownianToLewyTime = settings.BrownianToLewyTimeSeconds;
            _maxFlightLength = settings.MaxFlightLength;
            _longFlightThreshold = settings.LongFlightThreshold;
            _lewyScale = settings.LewyScale;
        }
    }
}