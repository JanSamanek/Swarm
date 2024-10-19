using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using SwarmSimulation.Core.Algorithms;
using SwarmSimulation.Settings;

namespace SwarmSimulation.Core
{
    public class Swarm
    {
        public List<Agent> Agents { get; } = new List<Agent>();
        private int AgentCounter { get; set; }

        public void CreateAgent(Vector2 position, int perceptionRange)
        {
            Agents.Add(new Agent(AgentCounter++, position, perceptionRange));
        }

        public void MoveToLineFormation(LineFormationAlgorithmSettings algorithmSettings)
        {
            var algorithm = AlgorithmFactory.Get<LineFormationAlgorithm, LineFormationAlgorithmSettings>();
            UpdatePositions(algorithm, algorithmSettings);
        }
        public void Disperse(DispersionAlgorithmSettings algorithmSettings)
        {
            var algorithm = AlgorithmFactory.Get<DispersionAlgorithm, DispersionAlgorithmSettings>();
            UpdatePositions(algorithm, algorithmSettings);
        }
        
        private void UpdatePositions<T>(IAlgorithm<T> algorithm, T settings)
        {
            UpdateNeighbours();
            foreach (var agent in Agents)
            {
                var controlInput = algorithm.CalculateControlInput(agent, settings);
                agent.Move(controlInput);
            }
        }
        
        private void UpdateNeighbours()
        {
            foreach (var agentToUpdate in Agents)
            {
                agentToUpdate.Neighbors = new List<Agent>();
                foreach (var agent in Agents.Where(agent => agentToUpdate.Id != agent.Id))
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