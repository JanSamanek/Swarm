using System.Collections.Concurrent;

namespace SwarmSimulation.Environment.Utilities
{
    public static class GarbageCollector
    {
        public static ConcurrentBag<Resource> ResourceGarbage { get; private set; }  = new ConcurrentBag<Resource>();

        public static void ClearGarbage()
        {
            foreach (var resource in ResourceGarbage)
            {
                Arena.Instance.Resources.Remove(resource);
            }
            ResourceGarbage = new ConcurrentBag<Resource>();
        }
    }
}