using SwarmSimulation.Settings;

namespace SwarmSimulation.Utilities
{
    public static class SimulationTimeManager
    {
        public static float GetDeltaTime()
        {
            return  1 / (float) SimulationSettings.Fps;

        }
    }
}