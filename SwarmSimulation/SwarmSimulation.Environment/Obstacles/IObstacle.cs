using System.Numerics;

namespace SwarmSimulation.Environment.Obstacles
{
    public interface IObstacle
    {
        Vector2 GetDistanceVectorToAgent(Vector2 agentPosition);
    }
}