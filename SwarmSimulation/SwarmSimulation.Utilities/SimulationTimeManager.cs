using System;

namespace SwarmSimulation.Utilities
{
    public static class SimulationTimeManager
    {
        private static int? _fps = null; 
        public static float GetDeltaTime()
        {
            if (_fps.HasValue)
            {
                return  1 / (float) _fps.Value;
            }
  
            throw new Exception("Not a valid fps value");
        }

        public static void SetFps(int fps)
        {
            _fps = fps;
        }
    }
}