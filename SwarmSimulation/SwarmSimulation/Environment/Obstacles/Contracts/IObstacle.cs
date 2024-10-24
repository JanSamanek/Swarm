using System.Drawing;
using System.Numerics;

namespace SwarmSimulation.Environment.Obstacles.Contracts
{
    public interface IObstacle
    {
        Vector2 GetDistanceVectorToAgent(Vector2 agentPosition);
    }
}