using System;

namespace SwarmSimulation.Utilities.Random
{
    public static class LewyRandom
    {
        public static double Next(float lewyParameter, int n=100)
        {
            var z = 0.0;
            for (var i = 0; i < n; i++)
            {
                var a = GaussianRandom.Next();
                var b = GaussianRandom.Next();
                
                var m = a / Math.Pow(Math.Abs(b), 1/lewyParameter);

                z += m / Math.Pow(n, 1/lewyParameter);
            }
            return z;
        }
    }
}