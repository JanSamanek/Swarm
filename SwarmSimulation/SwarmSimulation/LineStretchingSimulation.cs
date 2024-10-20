using System;
using System.Drawing;
using System.Numerics;
using System.Windows.Forms;
using SwarmSimulation.Core;
using SwarmSimulation.Core.Agents.Implementation;
using SwarmSimulation.Core.Algorithms;
using SwarmSimulation.Core.Algorithms.Contracts;
using SwarmSimulation.Core.Algorithms.Implementation;
using SwarmSimulation.Core.Algorithms.Inputs;
using SwarmSimulation.Core.Algorithms.Settings;
using SwarmSimulation.Settings;

namespace SwarmSimulation
{
    public sealed class LineStretchingSimulation : BaseForm
    {
        private Swarm _swarm;
        private LeaderAgent _leader1;
        private LeaderAgent _leader2;

        private IAlgorithm<ProximityAlgorithmSettings, ProximityAlgorithmInput> _dispersionAlgorithm;
        
        public LineStretchingSimulation()
        {
            Text = @"Swarm line stretching simulation";
            StartSimulation();
        }

        protected override void InitializeSimulation()
        {
            _dispersionAlgorithm = 
                AlgorithmFactory.Get<ProximityAlgorithm, ProximityAlgorithmSettings, ProximityAlgorithmInput>();
            
            var algorithmSettings = new ProximityAlgorithmSettings
            {
                DampingCoefficient = 4,
                StiffnessCoefficient = 15,
            };
            _dispersionAlgorithm.ConfigureSettings(algorithmSettings);
            
            const int perceptionRange = 100;
            _swarm = new Swarm();

            _leader1 = _swarm.AddLeader(new Vector2(485, 300), perceptionRange);
            _leader2 = _swarm.AddLeader(new Vector2(515, 300), perceptionRange);
            
            _swarm.AddAgent(new Vector2(490, 300), perceptionRange);
            _swarm.AddAgent(new Vector2(495, 295), perceptionRange);
            _swarm.AddAgent(new Vector2(505, 295), perceptionRange);
            _swarm.AddAgent(new Vector2(495, 305), perceptionRange);
            _swarm.AddAgent(new Vector2(505, 305), perceptionRange);
            _swarm.AddAgent(new Vector2(510, 300), perceptionRange);
            _swarm.AddAgent(new Vector2(490, 310), perceptionRange);
            _swarm.AddAgent(new Vector2(510, 310), perceptionRange);
            _swarm.AddAgent(new Vector2(495, 315), perceptionRange);
            _swarm.AddAgent(new Vector2(505, 315), perceptionRange);
            _swarm.AddAgent(new Vector2(500, 290), perceptionRange);
            _swarm.AddAgent(new Vector2(490, 320), perceptionRange);
            _swarm.AddAgent(new Vector2(510, 290), perceptionRange);
            _swarm.AddAgent(new Vector2(500, 300), perceptionRange);
            _swarm.AddAgent(new Vector2(500, 310), perceptionRange);
        }

        protected override void UpdateSimulation(object sender, EventArgs e)
        {
            var input = new ProximityAlgorithmInput
            {
                DesiredDistance = 50,
                NeighboursToCalculateFrom = 2
            };
            _leader1.MoveToTarget(new Vector2(300, 300), 15.0f);
            _leader2.MoveToTarget(new Vector2(750, 300), 15.0f);
            _swarm.Disperse(_dispersionAlgorithm, input); // TODO: create LineStretching
            
            PictureBox.Invalidate();
        }
        
        protected override void OnPaint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.Black);
            SwarmRenderer.DrawAgents(_swarm, e.Graphics, (10, 10));
        }
    }
}