using System;
using System.Drawing;
using System.Numerics;
using System.Windows.Forms;
using SwarmSimulation.Core;
using SwarmSimulation.Core.Agents.Implementation;
using SwarmSimulation.Core.Algorithms.Contracts;
using SwarmSimulation.Core.Algorithms.Implementation.CustomFormation;
using SwarmSimulation.Visualization;

namespace SwarmSimulation.Simulations
{
    public sealed class ArrowFormationSimulation : BaseForm
    {
        private Swarm _swarm;
        private LeaderAgent _leader;
        private IAlgorithm<FormationAlgorithmSettings, FormationAlgorithmInput> _arrowFormationAlgorithm;
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
            
            const int perceptionRange = 200;
            _swarm = new Swarm();
            _leader = _swarm.AddLeader(new Vector2(500,300), perceptionRange);
            _swarm.AddAgent(new Vector2(480,320), perceptionRange);
            _swarm.AddAgent(new Vector2(520,320), perceptionRange);
            _swarm.AddAgent(new Vector2(460,340), perceptionRange);
            _swarm.AddAgent(new Vector2(540,340), perceptionRange);
        }

        protected override void UpdateSimulation(object sender, EventArgs e)
        {
            var desiredInterAgentDistances = new float[5, 5]
            {
                { 0.0f, 28.28f, 28.28f, 56.57f, 56.57f },
                { 28.28f, 0.0f, 40.0f, 28.28f, 63.25f },
                { 28.28f, 40.0f, 0.0f, 63.25f, 28.28f },
                { 56.57f, 28.28f, 63.25f, 0.0f, 80.0f },
                { 56.57f, 63.25f, 28.28f, 80.0f, 0.0f },
            };
            
            var input = new FormationAlgorithmInput
            {
                DesiredInterAgentDistances = desiredInterAgentDistances
            };
            _leader.MoveToTarget(new Vector2(350,150), 15.0f);
            _swarm.MoveToArrowFormation(_arrowFormationAlgorithm, input);
            
            PictureBox.Invalidate();
        }
        
        protected override void OnPaint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.Black);
            SwarmRenderer.DrawAgents(_swarm, e.Graphics, (10, 10));
        }
    }
}