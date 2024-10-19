using System;
using System.Drawing;
using System.Numerics;
using System.Windows.Forms;
using SwarmSimulation.Core;
using SwarmSimulation.Settings;

namespace SwarmSimulation
{
    public sealed class LineFormationSimulation : BaseForm
    {
        private Swarm _swarm;
        private LineFormationAlgorithmSettings _lineFormationAlgorithmSettings;

        public LineFormationSimulation()
        {
            Text = @"Swarm line formation simulation";
            StartSimulation();
        }

        protected override void InitializeSimulation()
        {
            _lineFormationAlgorithmSettings = new LineFormationAlgorithmSettings
            {
                DesiredDistance = 30,
                GainParallel = 0.3f,
                GainPerpendicular = 0.2f,
                LineOrientationAngleInRadians = (float) Math.PI / 4
            };
            
            var perceptionRange = 120;
            _swarm = new Swarm();
            _swarm.CreateAgent(new Vector2(380, 370), perceptionRange);
            _swarm.CreateAgent(new Vector2(400, 310), perceptionRange);
            _swarm.CreateAgent(new Vector2(480, 300), perceptionRange);
            _swarm.CreateAgent(new Vector2(345, 320), perceptionRange);
            _swarm.CreateAgent(new Vector2(520, 340), perceptionRange);
            _swarm.CreateAgent(new Vector2(325, 380), perceptionRange);
            _swarm.CreateAgent(new Vector2(430, 270), perceptionRange);
        }

        protected override void UpdateSimulation(object sender, EventArgs e)
        {
            _swarm.MoveToLineFormation(_lineFormationAlgorithmSettings);
            PictureBox.Invalidate();
        }
        
        protected override void OnPaint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.Black);
            SwarmRenderer.DrawAgents(_swarm, e.Graphics, (20, 20), drawId:true);
        }
    }
}