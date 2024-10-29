using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using SwarmSimulation.Algorithms.Agents;
using SwarmSimulation.Environment.Utilities;

namespace SwarmSimulation.Algorithms.Utilities
{
    public static class SwarmController
    {
        public static void ExecuteAlgorithm<TInput>(Swarm swarm, IEnumerable<Agent> agents, IAlgorithm<TInput> algorithm, 
            TInput input)
        {
            UpdateNeighbours(swarm);
            Parallel.ForEach(agents, agent => ApplyAlgorithm(agent, algorithm, input));
            GarbageCollector.ClearHarvestedResources();
        }

        public static void ExecuteAlgorithm<TInput>(Swarm swarm, IAlgorithm<TInput> algorithm, 
            TInput input)
        {
            UpdateNeighbours(swarm);
            Parallel.ForEach(swarm.Agents, agent => ApplyAlgorithm(agent, algorithm, input));
            GarbageCollector.ClearHarvestedResources();
        }
        
        public static void ExecuteAlgorithm<TInput>(Swarm swarm, Agent agent, IAlgorithm<TInput> algorithm,
            TInput input)
        {
            UpdateNeighbours(swarm);
            ApplyAlgorithm(agent, algorithm, input);
        }
        
        private static void ApplyAlgorithm<TInput>(Agent agent, IAlgorithm<TInput> algorithm, TInput input)
        {            
            var controlInput = algorithm.CalculateControlInput(agent, input);
            agent.Move(controlInput);
        }
        
        private static  void UpdateNeighbours(Swarm swarm)
        {
            foreach (var agentToUpdate in swarm.Agents)
            {
                agentToUpdate.Neighbors = new List<Agent>();
                foreach (var agent in swarm.Agents.Where(agent => agentToUpdate.Id != agent.Id))
                {
                    var distance = Vector2.Distance(agent.Position, agentToUpdate.Position);
                    if (distance <= agentToUpdate.PerceptionRange)
                    {
                        agentToUpdate.Neighbors.Add(agent);
                    }
                }
            }
        }
    }
}