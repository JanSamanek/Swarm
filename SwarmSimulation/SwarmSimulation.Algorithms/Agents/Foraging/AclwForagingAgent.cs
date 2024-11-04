using System;
using System.Numerics;
using SwarmSimulation.Algorithms.Foraging;
using SwarmSimulation.Algorithms.Foraging.AdaptiveLewyWalk.States;

namespace SwarmSimulation.Algorithms.Agents.Foraging
{
    public class AclwForagingAgent : ForagingAgent
    {
        public IState<AclwForagingAgent> State { get; set; }
        public int TicksFromLastSuccessfulExploration { get; set; } 
        public bool IsPerformingLongFlight { get; set; }
        
        public AclwForagingAgent(Vector2 position, float size, float perceptionRange, int maxResourceCapacity) 
            : base(position, size, perceptionRange, maxResourceCapacity)
        {
            State = new AclwExploring(this);
        }
        
    }
}