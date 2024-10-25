using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using SwarmSimulation.Core.Agents.Contracts;
using SwarmSimulation.Core.Algorithms.Contracts;

namespace SwarmSimulation.Core.Algorithms
{
    public static class SwarmController
    {
        public static void ExecuteAlgorithm<TInput>(Swarm swarm, IEnumerable<IAgent> agents, IAlgorithm<TInput> algorithm, 
            TInput input)
        {
            UpdateNeighbours(swarm);
            foreach (var agent in agents)
            {
                ApplyAlgorithm(swarm, agent, algorithm, input);
            }
        }

        public static void ExecuteAlgorithm<TInput>(Swarm swarm, IAgent agent, IAlgorithm<TInput> algorithm,
            TInput input)
        {
            UpdateNeighbours(swarm);
            ApplyAlgorithm(swarm, agent, algorithm, input);
        }
        
        private static void ApplyAlgorithm<TInput>(Swarm swarm, IAgent agent, IAlgorithm<TInput> algorithm, TInput input)
        {            
            var controlInput = algorithm.CalculateControlInput(agent, input);
            agent.Move(controlInput);
        }
        
        private static  void UpdateNeighbours(Swarm swarm)
        {
            foreach (var agentToUpdate in swarm.Agents)
            {
                agentToUpdate.Neighbors = new List<IAgent>();
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