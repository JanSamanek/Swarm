using System.Collections.Generic;
using System.Numerics;
using SwarmSimulation.Engine.Collider;
using SwarmSimulation.Environment.Obstacles;

namespace SwarmSimulation.Algorithms.Agents
{
    public interface IAgent
    {
        int Id  { get; }
        Vector2 Position { get; }
        float Size { get;}
        ICollider Collider { get; set; } 
        Vector2 Velocity { get; }
        float PerceptionRange { get; }
        List<IAgent> Neighbors { get; set; }
        
        void Move(Vector2 controlInput);
        IEnumerable<IObstacle> DetectObstacles();
        bool DetectCollision();
        bool HasApproachedTarget(Vector2 target, float tolerance = 5);
    }
}