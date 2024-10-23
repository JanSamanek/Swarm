using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using SwarmSimulation.Core.Agents.Contracts;
using SwarmSimulation.Core.Agents.Implementation;
using SwarmSimulation.Core.Algorithms.Contracts;
using SwarmSimulation.Core.Algorithms.Inputs;
using SwarmSimulation.Core.Algorithms.Settings;

namespace SwarmSimulation.Core
{
    public class Swarm
    {
        public List<IAgent> Agents { get; } = new List<IAgent>();
        private int AgentCounter { get; set; }

        public RegularAgent AddAgent(Vector2 position, int perceptionRange)
        {
            var regular = new RegularAgent(AgentCounter++, position, perceptionRange);
            Agents.Add(regular);
            return regular;
        }
        public LeaderAgent AddLeader(Vector2 position, int perceptionRange)
        {
            var leader = new LeaderAgent(AgentCounter++, position, perceptionRange);
            Agents.Add(leader);
            return leader;
        }

        public void MoveToArrowFormation(
            IAlgorithm<FormationAlgorithmSettings, FormationAlgorithmInput> algorithm,
            FormationAlgorithmInput input)
        {
            UpdatePositions(algorithm, input);
        }
        
        public void MoveToLineFormation(IAlgorithm<LineFormationAlgorithmSettings, LineFormationAlgorithmInput> algorithm, 
            LineFormationAlgorithmInput input)
        {
            UpdatePositions(algorithm, input);
        }
        
        public void Disperse(IAlgorithm<ProximityAlgorithmSettings, ProximityAlgorithmInput> algorithm, 
            ProximityAlgorithmInput input)
        {
            UpdatePositions(algorithm, input);
        }

        public void FollowLeader(IAlgorithm<ProximityAlgorithmSettings, ProximityAlgorithmInput> algorithm, 
            ProximityAlgorithmInput input)
        {
            UpdatePositions(algorithm, input);
        }
        private void UpdatePositions<TSettings, TInput>(IAlgorithm<TSettings,TInput> algorithm, TInput input)
        {
            UpdateNeighbours();
            foreach (var agent in Agents.Where(a => a is RegularAgent))
            {
                var controlInput = algorithm.CalculateControlInput((RegularAgent) agent, input);
                agent.Move(controlInput);
            }
        }
        
        private void UpdateNeighbours()
        {
            foreach (var agentToUpdate in Agents)
            {
                agentToUpdate.Neighbors = new List<IAgent>();
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