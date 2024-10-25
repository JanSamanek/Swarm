using System;
using System.Drawing;
using System.Numerics;
using System.Windows.Forms;
using SwarmSimulation.Core;
using SwarmSimulation.Core.Agents.Implementation;
using SwarmSimulation.Core.Algorithms;
using SwarmSimulation.Core.Algorithms.Contracts;
using SwarmSimulation.Core.Algorithms.Implementation.CustomFormation;
using SwarmSimulation.Core.Algorithms.Implementation.MoveToTarget;
using SwarmSimulation.Visualization;

namespace SwarmSimulation.Simulations
{
    public sealed class ArrowFormationSimulation : BaseForm
    {
        private Swarm _swarm;
        private int _leaderId;
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
            _swarm = new Swarm();
            _leaderId = _swarm.AddLeader(new Vector2(500,300), perceptionRange);
            _swarm.AddAgent(new Vector2(480,320), perceptionRange);
            _swarm.AddAgent(new Vector2(520,320), perceptionRange);
            _swarm.AddAgent(new Vector2(460,340), perceptionRange);
            _swarm.AddAgent(new Vector2(540,340), perceptionRange);
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
            AlgorithmExecutor.ExecuteAlgorithmOn<RegularAgent, FormationAlgorithmInput>(_swarm,
                _arrowFormationAlgorithm, formationAlgorithmInput);

            var moveToTargetAlgorithmInput = new MoveToTargetAlgorithmInput
            {
                Speed = 15.0f,
                TargetPosition = new Vector2(350, 150)
            };
            AlgorithmExecutor.ExecuteAlgorithmOn(_swarm, _leaderId, _moveToTargetAlgorithm, moveToTargetAlgorithmInput);
            
            PictureBox.Invalidate();
        }
        
        protected override void OnPaint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.Black);
            SwarmRenderer.DrawAgents(_swarm, e.Graphics, (10, 10));
        }
    }
}