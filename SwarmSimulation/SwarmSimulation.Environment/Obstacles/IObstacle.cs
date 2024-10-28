using System.Numerics;

namespace SwarmSimulation.Environment.Obstacles
{
    public interface IObstacle
    {
        Vector2 GetDistanceVectorFromBorder(Vector2 point);
        bool IsPointInside(Vector2 point);
    }
}