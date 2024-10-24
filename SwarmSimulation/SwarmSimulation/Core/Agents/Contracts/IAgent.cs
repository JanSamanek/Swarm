using System.Collections.Generic;
using System.Numerics;
using SwarmSimulation.Core.Agents.Implementation;
using SwarmSimulation.Environment;
using SwarmSimulation.Environment.Obstacles.Contracts;

namespace SwarmSimulation.Core.Agents.Contracts
{
    public interface IAgent
    {
        int Id  { get; }
        Vector2 Position { get; }
        Vector2 Velocity { get; }
        float PerceptionRange { get; }
        List<IAgent> Neighbors { get; set; }
        void Move(Vector2 controlInput);
        IEnumerable<IObstacle> DetectObstacles();
        
    }
}