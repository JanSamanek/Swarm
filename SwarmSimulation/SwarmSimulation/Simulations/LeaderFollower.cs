using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Windows.Forms;
using SwarmSimulation.Algorithms;
using SwarmSimulation.Algorithms.Agents;
using SwarmSimulation.Algorithms.Agents.Formation;
using SwarmSimulation.Algorithms.Aggregation.Proximity;
using SwarmSimulation.Algorithms.Other.MoveToTarget;
using SwarmSimulation.Algorithms.Utilities;
using SwarmSimulation.Environment;
using SwarmSimulation.Visualization;

namespace SwarmSimulation.Simulations
{
    public sealed class LeaderFollower : BaseForm
    {
        private Swarm _swarm;
        private LeaderAgent _leader;

        private IAlgorithm<ProximityAlgorithmInput> _leaderFollowerAlgorithm;
        private IAlgorithm<MoveToTargetAlgorithmInput> _moveToTargetAlgorithm;
        
        public LeaderFollower()
        {
            Text = @"Swarm line stretching simulation";
            StartSimulation();
        }

        protected override void InitializeSimulation()
        {
            
            var leaderFollowerAlgorithmSettings = new ProximityAlgorithmSettings
            {
                DampingCoefficient = 4f,
                InterAgentStiffnessCoefficient = 355.2f,
                LeaderStiffnessCoefficient = 288f
            };
            _leaderFollowerAlgorithm = new ProximityAlgorithm(leaderFollowerAlgorithmSettings);
            
            _moveToTargetAlgorithm = new MoveToTargetAlgorithm();
            
            var arenaBuilder = new ArenaBuilder();
            arenaBuilder.Initialize(new Vector2(500, 300), 1000, 600)
                .AddCircularObstacle(new Vector2(400, 400), 70)
                .AddCircularObstacle(new Vector2(400, 200), 70)
                .Build();
            
            var positions = new List<Vector2>
            {
                new Vector2(550, 300),
                new Vector2(525, 343),
                new Vector2(475, 343),
                new Vector2(450, 300),
                new Vector2(475, 256),
                new Vector2(525, 256)
            };
            _leader = new LeaderAgent(new Vector2(500, 300), 5, 100);
            
            var swarmBuilder = new SwarmBuilder();
            _swarm = swarmBuilder.AddLeaderToSwarm(_leader)
                .SetPositions(positions)
                .SetPerceptionRange(100)
                .SetAgentSize(5)
                .SetAgentType(AgentsType.Basic)
                .Build();
        }

        protected override void UpdateSimulation()
        {
            var proximityAlgorithmInput = new ProximityAlgorithmInput
            {
                DesiredDistance = 50,
                NeighboursToCalculateFrom = 3
            };
            SwarmController.ExecuteAlgorithm(_swarm, _swarm.Agents.OfType<BasicAgent>(), _leaderFollowerAlgorithm, proximityAlgorithmInput);

            var moveToTargetAlgorithmInput = new MoveToTargetAlgorithmInput
            {
                TargetPosition = new Vector2(200, 300),
                Speed = 35.0f
            };
            SwarmController.ExecuteAlgorithm(_swarm, _leader, _moveToTargetAlgorithm, moveToTargetAlgorithmInput);
            
            PictureBox.Invalidate();
        }
        
        protected override void OnPaint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.Black);
            SwarmRenderer.DrawAgents(_swarm, e.Graphics);
            ArenaRenderer.DrawArena(Arena.Instance, e.Graphics);
        }
    }
}