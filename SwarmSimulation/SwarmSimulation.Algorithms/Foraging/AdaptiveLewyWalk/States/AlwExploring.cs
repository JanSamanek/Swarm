using System;
using System.Linq;
using SwarmSimulation.Algorithms.Agents.Foraging;
using SwarmSimulation.Algorithms.Foraging.LewyWalk.States;
using SwarmSimulation.Algorithms.Utilities.Generator;
using SwarmSimulation.Utilities;

namespace SwarmSimulation.Algorithms.Foraging.AdaptiveLewyWalk.States
{
    public class AlwExploring : IState<AlwForagingAgent>
    {
        private static float _maxFlightLength;
        private static float _lewyScale;
        private static int _maxExplorationAttempts;
        private static float _brownianToLewyTime;

        private static float _beta;
        private static int _brownianToLewyTicks;
        
        public AlwExploring(AlwForagingAgent agent)
        {
            _brownianToLewyTicks = (int)(_brownianToLewyTime / SimulationTimeManager.GetDeltaTime());
            _beta = (float) 2 / _brownianToLewyTicks;
            OnEnter(agent);
        }
        
        private void OnEnter(AlwForagingAgent agent)
        {
            var lewyParameter = UpdateLewyParameter(agent.TicksFromLastSuccessfulExploration);
            var flight = LewyMotionGenerator.GenerateFlight(lewyParameter, 1, _maxFlightLength, _lewyScale);
            agent.Target = agent.Position + flight;
        }

        public void Execute(AlwForagingAgent agent)
        {
            agent.TicksFromLastSuccessfulExploration++;
            
            var resources = agent.DetectResources().ToList();
            if (resources.Any())
            {
                agent.TicksFromLastSuccessfulExploration = 0;
                agent.State = new AlwHarvesting(agent, resources.First());
                return;
            }

            if (agent.UnsuccessfulExplorationAttempts >= _maxExplorationAttempts)
            {
                agent.State = new AlwReturningToNest(agent);
            }
            
            if (agent.HasApproachedTarget(agent.Target) || agent.DetectCollision())
            {
                agent.UnsuccessfulExplorationAttempts++;
                agent.State = new AlwExploring(agent);
            }
        }
        
        private float UpdateLewyParameter(int ticksFromLastSuccessfulExploration)
        {
            var lewyParameter =  Math.Max(1, -_beta * (ticksFromLastSuccessfulExploration - _brownianToLewyTicks));
            return lewyParameter;
        }

        public static void Configure(AlwForagingAlgorithmSettings settings)
        {
            _maxExplorationAttempts = settings.MaxExploringAttempts;
            _brownianToLewyTime = settings.BrownianToLewyTimeSeconds;
            _maxFlightLength = settings.MaxFlightLength;
            _lewyScale = settings.LewyScale;
        }
    }
}