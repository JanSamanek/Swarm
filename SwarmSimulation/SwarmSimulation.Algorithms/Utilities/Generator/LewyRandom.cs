using System;

namespace SwarmSimulation.Algorithms.Utilities.Generator
{
    public static class LewyRandom
    {
        // Mantegna's algorithm
        public static double Next(float lewyParameter, float min, float max, float scale=1,  int n=150)
        {
            var z = 0.0;
            for (var i = 0; i < n; i++)
            {
                var a = GaussianRandom.Next(mean:0.0f, standardDeviation:1.0f);
                var b = GaussianRandom.Next(mean:0.0f, standardDeviation:1.0f);
                
                var m = a / Math.Pow(Math.Abs(b), 1/lewyParameter);

                z += m / Math.Pow(n, 1/lewyParameter);
            }

            z = Math.Min(Math.Abs(scale * z), max);
            z = Math.Max(Math.Abs(scale * z), min);
            return z;
        }
    }
}