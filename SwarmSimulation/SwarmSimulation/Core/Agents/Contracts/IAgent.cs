using System.Collections.Generic;
using System.Numerics;
using SwarmSimulation.Core.Agents.Implementation;

namespace SwarmSimulation.Core.Agents.Contracts
{
    public interface IAgent
    {
        int Id  { get; }
        Vector2 Position { get; }
        Vector2 Velocity { get; }
        int PerceptionRange { get; }
        List<IAgent> Neighbors { get; set; }
        void Move(Vector2 controlInput);
    }
}