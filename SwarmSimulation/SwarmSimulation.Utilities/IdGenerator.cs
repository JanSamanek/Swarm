namespace SwarmSimulation.Utilities
{
    public static class IdGenerator
    {
        private static int _nextId = 0;
        public static int GetNextId()
        {
            return _nextId++;
        }
    }
}