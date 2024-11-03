using System.Numerics;
using SwarmSimulation.Algorithms.Foraging;
using SwarmSimulation.Algorithms.Foraging.LewyWalk.States;

namespace SwarmSimulation.Algorithms.Agents.Foraging
{
    public class LwForagingAgent : ForagingAgent
    {
        public IState<LwForagingAgent> State { get; set; }
        public LwForagingAgent(Vector2 position, float size, float perceptionRange, int maxResourceCapacity) 
            : base(position, size, perceptionRange, maxResourceCapacity)
        {
            State = new LwExploring(this);
        }
    }
}