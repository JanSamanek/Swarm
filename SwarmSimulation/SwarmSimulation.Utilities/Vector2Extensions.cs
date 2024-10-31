using System;
using System.Numerics;

namespace SwarmSimulation.Utilities
{
    public static class Vector2Extensions
    {
        public static Vector2 Rotate(this Vector2 vector, float angleInRadians)
        {
            var cos = (float)Math.Cos(angleInRadians);
            var sin = (float)Math.Sin(angleInRadians);

            return new Vector2(
                vector.X * cos - vector.Y * sin,
                vector.X * sin + vector.Y * cos
            );
        }
    }
}