using System;
using System.Threading;

namespace SwarmSimulation.Algorithms.Foraging.Generators
{
    public static class GaussianRandom
    {
        private static readonly ThreadLocal<Random> Random =
            new ThreadLocal<Random>(() => new Random(Guid.NewGuid().GetHashCode()));

        // Box-Muller transform
        public static double Next(double mean = 0.0, double standardDeviation = 1.0)
        {
            var u1 = Random.Value.NextDouble();
            var u2 = Random.Value.NextDouble();

            var randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2);

            return mean + standardDeviation * randStdNormal;
        }
    }
}