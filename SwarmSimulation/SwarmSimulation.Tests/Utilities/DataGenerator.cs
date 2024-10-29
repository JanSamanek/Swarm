using SwarmSimulation.Utilities.Random;

namespace SwarmSimulation.Tests.Utilities
{
    public static class DataGenerator
    {
        public static double[] GenerateLewyDistribution(float lewyParameter, float min, float max, int sampleSize=1000)
        {
            var data = new double[sampleSize];

            for (var i = 0; i < sampleSize; i++)
            {
                data[i] = LewyRandom.Next(lewyParameter, min, max);
            }
            return data;
        }
        
        public static double[] GenerateGaussianDistribution(double mean, double standardDeviation, int sampleSize=1000)
        {
            var data = new double[sampleSize];

            for (var i = 0; i < sampleSize; i++)
            {
                data[i] = GaussianRandom.Next(mean, standardDeviation);
            }
            return data;
        }
    }
}