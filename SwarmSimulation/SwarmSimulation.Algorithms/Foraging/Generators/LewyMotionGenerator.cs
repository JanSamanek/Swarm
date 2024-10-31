using System;
using System.Numerics;
using SwarmSimulation.Utilities;

namespace SwarmSimulation.Algorithms.Foraging.Generators
{
    public static class LewyMotionGenerator
    {
        public static Vector2 GenerateFlight(float lewyParameter, float min, float max, float lewyScale)
        {
            var direction = GenerateNewDirection();
            var length = GetFlightLength(lewyParameter, min, max, lewyScale);
            var flight = direction * length;
            return flight;
        }
        
        private static Vector2 GenerateNewDirection()
        {
            var random = new Random();
            var randomAngle = (float) (random.NextDouble() * 2 * Math.PI);
            return Vector2.Normalize(Vector2.One).Rotate(randomAngle);
        }

        private static float GetFlightLength(float lewyParameter, float min, float max, float lewyScale)
        {
            return (float) LewyRandom.Next(lewyParameter, min, max, lewyScale);
        }
    }
}