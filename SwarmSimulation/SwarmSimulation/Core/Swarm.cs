using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using SwarmSimulation.Core.Algorithms;
using SwarmSimulation.Core.Algorithms.Inputs;
using SwarmSimulation.Core.Algorithms.Settings;

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

        public void MoveToLineFormation(IAlgorithm<LineFormationAlgorithmSettings, LineFormationAlgorithmInput> algorithm, 
            LineFormationAlgorithmInput input)
        {
            UpdatePositions(algorithm, input);
        }
        
        public void Disperse(IAlgorithm<DispersionAlgorithmSettings, DispersionAlgorithmInput> algorithm, 
            DispersionAlgorithmInput input)
        {
            UpdatePositions(algorithm, input);
        }
        
        private void UpdatePositions<TSettings, TInput>(IAlgorithm<TSettings,TInput> algorithm, TInput input)
        {
            UpdateNeighbours();
            foreach (var agent in Agents)
            {
                var controlInput = algorithm.CalculateControlInput(agent, input);
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