using System;
using System.Drawing;
using System.Numerics;
using System.Windows.Forms;
using SwarmSimulation.Core;
using SwarmSimulation.Core.Algorithms;
using SwarmSimulation.Core.Algorithms.Implementation;
using SwarmSimulation.Core.Algorithms.Inputs;
using SwarmSimulation.Core.Algorithms.Settings;
using SwarmSimulation.Settings;

namespace SwarmSimulation
{
    public sealed class DispersionSimulation : BaseForm
    {
        private Swarm _swarm;
        private IAlgorithm<DispersionAlgorithmSettings, DispersionAlgorithmInput> _dispersionAlgorithm;
        
        public DispersionSimulation()
        {
            Text = @"Swarm dispersion simulation";
            StartSimulation();
        }

        protected override void InitializeSimulation()
        {
            _dispersionAlgorithm = 
                AlgorithmFactory.Get<DispersionAlgorithm, DispersionAlgorithmSettings, DispersionAlgorithmInput>();
            
            var algorithmSettings = new DispersionAlgorithmSettings
            {
                DampingCoefficient = 4,
                StiffnessCoefficient = 15,
            };
            _dispersionAlgorithm.ConfigureSettings(algorithmSettings);
            
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
            var input = new DispersionAlgorithmInput
            {
                DesiredDistance = 50,
                NeighboursToCalculateFrom = 2
            };
            _swarm.Disperse(_dispersionAlgorithm, input);
            
            PictureBox.Invalidate();
        }
        
        protected override void OnPaint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.Black);
            SwarmRenderer.DrawAgents(_swarm, e.Graphics, (10, 10));
        }
    }
}