using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using SwarmSimulation.Core.Agents.Contracts;
using SwarmSimulation.Core.Agents.Implementation;
using SwarmSimulation.Core.Algorithms.Contracts;

namespace SwarmSimulation.Core.Algorithms
{
    public static class AlgorithmExecutor
    {
        public static void ExecuteAlgorithmOn<TAgent, TInput>(Swarm swarm, IAlgorithm<TInput> algorithm, TInput input)
        where TAgent : IAgent
        {
            UpdateNeighbours(swarm);
            foreach (var agent in swarm.Agents.OfType<TAgent>())
            {
                ExecuteAlgorithm(agent, algorithm, input);
            }
        }

        public static void ExecuteAlgorithmOn<TInput>(Swarm swarm, int agentId, IAlgorithm<TInput> algorithm,
            TInput input)
        {
            var agent = swarm.Agents.FirstOrDefault(a => a.Id == agentId);
            if (agent == null)
            {
                throw new Exception($"Agent with id {agentId} not found");
            }
            UpdateNeighbours(swarm);
            ExecuteAlgorithm(agent, algorithm, input);
        }

        private static void ExecuteAlgorithm<TInput>(IAgent agent, IAlgorithm<TInput> algorithm, TInput input)
        {
            var controlInput = algorithm.CalculateControlInput( agent, input);
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