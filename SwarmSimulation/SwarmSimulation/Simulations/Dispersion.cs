using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using System.Windows.Forms;
using SwarmSimulation.Agents;
using SwarmSimulation.Agents.Basic;
using SwarmSimulation.Algorithms;
using SwarmSimulation.Algorithms.Proximity;
using SwarmSimulation.Visualization;

namespace SwarmSimulation.Simulations
{
    public sealed class Dispersion : BaseForm
    {
        private IAlgorithm<ProximityAlgorithmInput> _dispersionAlgorithm;
        private Swarm _swarm;
        
        public Dispersion()
        {
            Text = @"Swarm dispersion simulation";
            StartSimulation();
        }

        protected override void InitializeSimulation()
        {
            var algorithmSettings = new ProximityAlgorithmSettings
            {
                DampingCoefficient = 4.4f,
                InterAgentStiffnessCoefficient = 15.2f,
            };
            _dispersionAlgorithm = new ProximityAlgorithm(algorithmSettings);
            
            const float perceptionRange = 100;
            var positions = new List<Vector2>
            {
                new Vector2(490, 300),
                new Vector2(495, 295),
                new Vector2(505, 295),
                new Vector2(495, 305),
                new Vector2(505, 305),
                new Vector2(510, 300),
                new Vector2(490, 310),
                new Vector2(510, 310),
                new Vector2(495, 315),
                new Vector2(505, 315),
                new Vector2(500, 290),
                new Vector2(490, 320),
                new Vector2(510, 290),
                new Vector2(500, 300),
                new Vector2(500, 310)
            };
            _swarm = SwarmBuilder.CreateSwarm<BasicAgent>(positions, perceptionRange);
        }

        protected override void UpdateSimulation(object sender, EventArgs e)
        {
            var input = new ProximityAlgorithmInput
            {
                DesiredDistance = 50,
                NeighboursToCalculateFrom = 2
            };
            SwarmController.ExecuteAlgorithm(_swarm, _swarm.Agents, _dispersionAlgorithm, input);
            
            PictureBox.Invalidate();
        }
        
        protected override void OnPaint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.Black);
            SwarmRenderer.DrawAgents(_swarm, e.Graphics, (10, 10));
        }
    }
}