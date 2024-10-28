using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using System.Windows.Forms;
using SwarmSimulation.Agents;
using SwarmSimulation.Agents.Basic;
using SwarmSimulation.Algorithms;
using SwarmSimulation.Algorithms.LineAggregation;
using SwarmSimulation.Visualization;

namespace SwarmSimulation.Simulations
{
    public sealed class LineFormation : BaseForm
    {
        private Swarm _swarm;
        private IAlgorithm<LineAggregationAlgorithmInput> _lineFormationAlgorithm;
        
        public LineFormation()
        {
            Text = @"Swarm line formation simulation";
            StartSimulation();
        }

        protected override void InitializeSimulation()
        {
            var algorithmSettings = new LineAggregationAlgorithmSettings
            {
                GainParallel = 0.9f,
                GainPerpendicular = 0.6f,
            };
            _lineFormationAlgorithm = new LineAggregationAlgorithm(algorithmSettings);
            
            const float perceptionRange = 120;
            _swarm = new Swarm();
            var positions = new List<Vector2>
            {
                new Vector2(380, 370),
                new Vector2(400, 310),
                new Vector2(480, 300),
                new Vector2(345, 320),
                new Vector2(520, 340),
                new Vector2(325, 380),
                new Vector2(430, 270)
            };
            _swarm = SwarmBuilder.CreateSwarm<BasicAgent>(positions, perceptionRange);
        }

        protected override void UpdateSimulation(object sender, EventArgs e)
        {
            var input = new LineAggregationAlgorithmInput
            {
                DesiredDistance = 30,
                LineOrientationAngleInRadians = (float) Math.PI / 4
            };
            SwarmController.ExecuteAlgorithm(_swarm, _lineFormationAlgorithm, input);
            
            PictureBox.Invalidate();
        }
        
        protected override void OnPaint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.Black);
            SwarmRenderer.DrawAgents(_swarm, e.Graphics, 10);
        }
    }
}