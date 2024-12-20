using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Windows.Forms;
using SwarmSimulation.Algorithms;
using SwarmSimulation.Algorithms.Agents;
using SwarmSimulation.Algorithms.Agents.Formation;
using SwarmSimulation.Algorithms.Aggregation.CustomFormation;
using SwarmSimulation.Algorithms.Other.MoveToTarget;
using SwarmSimulation.Algorithms.Utilities;
using SwarmSimulation.Visualization;

namespace SwarmSimulation.Simulations
{
    public sealed class ArrowFormation : BaseForm
    {
        private Swarm _swarm;
        private LeaderAgent _leader;
        private IAlgorithm<FormationAlgorithmInput> _arrowFormationAlgorithm;
        private IAlgorithm<MoveToTargetAlgorithmInput> _moveToTargetAlgorithm;

        public ArrowFormation()
        {
            Text = @"Swarm arrow formation simulation";
            StartSimulation();
        }
        
         protected override void InitializeSimulation()
         {
             
             var algorithmSettings = new FormationAlgorithmSettings
             {
                ApfGain = 135.0f
             };
             _arrowFormationAlgorithm = new FormationAlgorithm(algorithmSettings);
             
             _moveToTargetAlgorithm = new MoveToTargetAlgorithm();
             
            _leader = new LeaderAgent(new Vector2(500, 300), 5, 200);
            var positions = new List<Vector2>
            {
                new Vector2(480, 320),
                new Vector2(520, 320),
                new Vector2(460, 340),
                new Vector2(540, 340)
            };
            
            var swarmBuilder = new SwarmBuilder();
            _swarm = swarmBuilder.AddLeaderToSwarm(_leader)
                .SetPositions(positions)
                .SetPerceptionRange(200)
                .SetAgentSize(5)
                .SetAgentType(AgentsType.Basic)
                .Build();
                
        }

        protected override void UpdateSimulation()
        {
            var desiredInterAgentDistances = new [,]
            {
                { 0.0f, 28.28f, 28.28f, 56.57f, 56.57f },
                { 28.28f, 0.0f, 40.0f, 28.28f, 63.25f },
                { 28.28f, 40.0f, 0.0f, 63.25f, 28.28f },
                { 56.57f, 28.28f, 63.25f, 0.0f, 80.0f },
                { 56.57f, 63.25f, 28.28f, 80.0f, 0.0f },
            };
            
            var formationAlgorithmInput = new FormationAlgorithmInput
            {
                DesiredInterAgentDistances = desiredInterAgentDistances
            };
            SwarmController.ExecuteAlgorithm(_swarm, _swarm.Agents.OfType<BasicAgent>(), _arrowFormationAlgorithm,
                formationAlgorithmInput);

            var moveToTargetAlgorithmInput = new MoveToTargetAlgorithmInput
            {
                Speed = 15.0f,
                TargetPosition = new Vector2(350, 150)
            };
            SwarmController.ExecuteAlgorithm(_swarm, _leader, _moveToTargetAlgorithm, moveToTargetAlgorithmInput);
            
            PictureBox.Invalidate();
        }
        
        protected override void OnPaint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.Black);
            SwarmRenderer.DrawAgents(_swarm, e.Graphics);
        }
    }
}