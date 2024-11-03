using System;
using System.Numerics;
using SwarmSimulation.Algorithms.Foraging;
using SwarmSimulation.Algorithms.Foraging.AdaptiveLewyWalk.States;

namespace SwarmSimulation.Algorithms.Agents.Foraging
{
    public class AlwForagingAgent : ForagingAgent
    {
        public IState<AlwForagingAgent> State { get; set; }
        public int TicksFromLastSuccessfulExploration { get; set; } 
        public AlwForagingAgent(Vector2 position, float size, float perceptionRange, int maxResourceCapacity) 
            : base(position, size, perceptionRange, maxResourceCapacity)
        {
            State = new AlwExploring(this);
        }
        
    }
}