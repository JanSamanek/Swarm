using System;
using System.Drawing;
using System.Numerics;
using System.Windows.Forms;
using SwarmSimulation.Core;
using SwarmSimulation.Settings;

namespace SwarmSimulation
{
    public sealed class DispersionSimulation : BaseForm
    {
        private Swarm _swarm;
        private DispersionAlgorithmSettings _dispersionAlgorithmSettings;

        public DispersionSimulation()
        {
            Text = @"Swarm dispersion simulation";
            StartSimulation();
        }

        protected override void InitializeSimulation()
        {
            _dispersionAlgorithmSettings = new DispersionAlgorithmSettings
            {
                DesiredDistance = 50,
                DampingCoefficient = 4,
                StiffnessCoefficient = 15,
                NeighboursToCalculateFrom = 2
            };
            
            var perceptionRange = 100;
            _swarm = new Swarm();
            _swarm.CreateAgent(new Vector2(495, 295), perceptionRange);
            _swarm.CreateAgent(new Vector2(505, 295), perceptionRange);
            _swarm.CreateAgent(new Vector2(495, 305), perceptionRange);
            _swarm.CreateAgent(new Vector2(505, 305), perceptionRange);
            _swarm.CreateAgent(new Vector2(490, 300), perceptionRange);
            _swarm.CreateAgent(new Vector2(510, 300), perceptionRange);
            _swarm.CreateAgent(new Vector2(490, 310), perceptionRange);
            _swarm.CreateAgent(new Vector2(510, 310), perceptionRange);
            _swarm.CreateAgent(new Vector2(495, 315), perceptionRange);
            _swarm.CreateAgent(new Vector2(505, 315), perceptionRange);
            _swarm.CreateAgent(new Vector2(500, 290), perceptionRange);
            _swarm.CreateAgent(new Vector2(490, 320), perceptionRange);
            _swarm.CreateAgent(new Vector2(510, 290), perceptionRange);
            _swarm.CreateAgent(new Vector2(500, 300), perceptionRange);
            _swarm.CreateAgent(new Vector2(500, 310), perceptionRange);
        }

        protected override void UpdateSimulation(object sender, EventArgs e)
        {
            _swarm.Disperse(_dispersionAlgorithmSettings);
            
            PictureBox.Invalidate();
        }
        
        protected override void OnPaint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.Black);
            SwarmRenderer.DrawAgents(_swarm, e.Graphics, (10, 10));
        }
    }
}