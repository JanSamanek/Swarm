using System;
using System.Numerics;

namespace SwarmSimulation.Utilities
{
    public static class MathUtils
    {
        public static Vector2 Clip(Vector2 value, float min, float max)
        {
            var x = Math.Max(min, Math.Min(value.X, max));
            var y = Math.Max(min, Math.Min(value.Y, max));
            return new Vector2(x, y);
        }
    }
}