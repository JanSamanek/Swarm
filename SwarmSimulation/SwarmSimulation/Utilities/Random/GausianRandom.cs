using System;

namespace SwarmSimulation.Utilities.Random
{
    public static class GaussianRandom
    {
        private static readonly System.Random Random = new System.Random();

        // Box-Muller transform
        public static double Next(double mean = 0.0, double standardDeviation = 1.0)
        {
            var u1 = Random.NextDouble();
            var u2 = Random.NextDouble();

            var randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2);

            return mean + standardDeviation * randStdNormal;
        }
    }
}