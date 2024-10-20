using System;
using System.Drawing;
using System.Numerics;
using System.Windows.Forms;
using SwarmSimulation.Core;
using SwarmSimulation.Core.Algorithms;
using SwarmSimulation.Core.Algorithms.Contracts;
using SwarmSimulation.Core.Algorithms.Implementation;
using SwarmSimulation.Core.Algorithms.Inputs;
using SwarmSimulation.Core.Algorithms.Settings;

namespace SwarmSimulation
{
    public sealed class LineFormationSimulation : BaseForm
    {
        private Swarm _swarm;
        private IAlgorithm<LineFormationAlgorithmSettings, LineFormationAlgorithmInput> _lineFormationAlgorithm;
        
        public LineFormationSimulation()
        {
            Text = @"Swarm line formation simulation";
            StartSimulation();
        }

        protected override void InitializeSimulation()
        {
            _lineFormationAlgorithm =
                AlgorithmFactory
                    .Get<LineFormationAlgorithm, LineFormationAlgorithmSettings, LineFormationAlgorithmInput>();
            
            var settings = new LineFormationAlgorithmSettings
            {
                GainParallel = 0.9f,
                GainPerpendicular = 0.6f,
            };
            _lineFormationAlgorithm.ConfigureSettings(settings);
            
            var perceptionRange = 120;
            _swarm = new Swarm();
            _swarm.AddAgent(new Vector2(380, 370), perceptionRange);
            _swarm.AddAgent(new Vector2(400, 310), perceptionRange);
            _swarm.AddAgent(new Vector2(480, 300), perceptionRange);
            _swarm.AddAgent(new Vector2(345, 320), perceptionRange);
            _swarm.AddAgent(new Vector2(520, 340), perceptionRange);
            _swarm.AddAgent(new Vector2(325, 380), perceptionRange);
            _swarm.AddAgent(new Vector2(430, 270), perceptionRange);
        }

        protected override void UpdateSimulation(object sender, EventArgs e)
        {
            var input = new LineFormationAlgorithmInput
            {
                DesiredDistance = 30,
                LineOrientationAngleInRadians = (float) Math.PI / 4
            };
            
            _swarm.MoveToLineFormation(_lineFormationAlgorithm, input);
            PictureBox.Invalidate();
        }
        
        protected override void OnPaint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.Black);
            SwarmRenderer.DrawAgents(_swarm, e.Graphics, (20, 20), drawId:true);
        }
    }
}