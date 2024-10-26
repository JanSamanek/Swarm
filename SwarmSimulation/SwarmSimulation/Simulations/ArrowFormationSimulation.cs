using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using System.Windows.Forms;
using SwarmSimulation.Algorithms;
using SwarmSimulation.Algorithms.CustomFormation;
using SwarmSimulation.Algorithms.MoveToTarget;
using SwarmSimulation.Agents.Agents.Contracts;
using SwarmSimulation.Visualization;

namespace SwarmSimulation.Simulations
{
    public sealed class ArrowFormationSimulation : BaseForm
    {
        private Agents.Swarm _swarm;
        private IEnumerable<IAgent> _regularAgents;
        private IAgent _leader;
        private IAlgorithm<FormationAlgorithmInput> _arrowFormationAlgorithm;
        private IAlgorithm<MoveToTargetAlgorithmInput> _moveToTargetAlgorithm;

        public ArrowFormationSimulation()
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
            
             var moveToTargetAlgorithmSettings = new MoveToTargetAlgorithmSettings
             {
                 TargetPositionTolerance = 1f
             };
             _moveToTargetAlgorithm = new MoveToTargetAlgorithm(moveToTargetAlgorithmSettings);
             
            const float perceptionRange = 200;
            _swarm = new Agents.Swarm();
            _leader= _swarm.AddLeader(new Vector2(500,300), perceptionRange);
            var positions = new List<Vector2>
            {
                new Vector2(480, 320),
                new Vector2(520, 320),
                new Vector2(460, 340),
                new Vector2(540, 340)
            };
            _regularAgents = _swarm.AddAgents(positions, perceptionRange);
        }

        protected override void UpdateSimulation(object sender, EventArgs e)
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
            SwarmController.ExecuteAlgorithm(_swarm, _regularAgents, _arrowFormationAlgorithm, formationAlgorithmInput);

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
            SwarmRenderer.DrawAgents(_swarm, e.Graphics, (10, 10));
        }
    }
}