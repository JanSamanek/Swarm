using System.Collections.Generic;
using System.Numerics;
using SwarmSimulation.Environment;
using SwarmSimulation.Environment.Obstacles;

namespace SwarmSimulation.Agents.Agents.Contracts
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
        IEnumerable<Resource> DetectResources();
        
    }
}